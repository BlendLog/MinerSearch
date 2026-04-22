using DBase;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MSearch.Core.ThreatAnalyzers
{
    public sealed class TaskThreatAnalyzer : IThreatAnalyzer
    {
        public ThreatObjectKind Kind => ThreatObjectKind.ScheduledTask;
        readonly IFileContentAnalyzer _fileAnalyzer;
        FileContentAnalysisResult _analysisResult = FileContentAnalysisResult.Error();

        readonly string[] checkDirectories =
        {
            Environment.SystemDirectory,
            new StringBuilder(AppConfig.GetInstance.drive_letter).Append(":\\W").Append("in").Append("do").Append("ws").Append("\\S").Append("ys").Append("WO").Append("W6").Append("4").ToString(),
            new StringBuilder(AppConfig.GetInstance.drive_letter).Append(":\\W").Append("in").Append("do").Append("ws").Append("\\S").Append("ys").Append("te").Append("m3").Append("2\\").Append("wb").Append("em").ToString(),
            MSData.GetInstance.queries["PowerShellPath"],
        };

        readonly Regex IfExistPathRegex = new Regex(@"if\s+exist\s+(?:""|\^"")(?<filepath>[A-Z]:\\.*?\.(?:dll|wsf))(?:""|\^"")", RegexOptions.Compiled | RegexOptions.IgnoreCase);


        public TaskThreatAnalyzer(IFileContentAnalyzer fileAnalyzer)
        {
            _fileAnalyzer = fileAnalyzer;
        }

        private static bool _headerLogged = false;
        private static readonly object _headerLock = new object();

        public IEnumerable<ThreatDecision> Analyze(IThreatObject threat)
        {
            if (!_headerLogged)
            {
                lock (_headerLock)
                {
                    if (!_headerLogged)
                    {
                        AppConfig.GetInstance.LL.LogHeadMessage("_ScanTasks");
                        _headerLogged = true;
                    }
                }
            }

            var taskObj = threat as TaskThreatObject;
            if (taskObj == null) yield break;

            var action = taskObj.Info.ExecActions.FirstOrDefault();
            if (action == null) yield break;

            int risk = 0;
            string args = action.Arguments ?? string.Empty;
            string filePathFromTask = taskObj.LinkedFile?.FilePath ?? ""; // Путь к исполняемому файлу

            AppConfig.GetInstance.LL.LogMessage("[#]", "_Scanning", $"{taskObj.Info.Name} | {taskObj.Info.Name}", ConsoleColor.White);
            // 1 Этап -----------------------------------------------------

            if (taskObj.Info.Name.StartsWith("d?ia?le?r".Replace("?", "")))
            {
                risk += 3;
                taskObj.ActionDeleteTask = true;
                taskObj.DetectionReasonRes = "_Malic1ousTask";
            }

            foreach (string a in MSData.GetInstance.badArgStrings)
            {
                if (args.IndexOf(a, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    risk += 3;
                    taskObj.ActionDeleteTask = true;
                    taskObj.DetectionReasonRes = "_Malic1ousTask";
                    break;
                }
            }

            if (args.IndexOf("/c reg add ", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                risk += 3;
                taskObj.ActionDeleteTask = true;
                taskObj.DetectionReasonRes = "_Malic1ousTask";
            }

            if (args.IndexOf("-jar ", StringComparison.OrdinalIgnoreCase) >= 0 && args.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            {
                risk += 3;
                taskObj.ActionDeleteTask = true;
                taskObj.DetectionReasonRes = "_Malic1ousTask";
            }

            if (taskObj.ActionDeleteTask) //Задача вредоносна независимо от файла
            {
                yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);
            }

            // 2 Этап -------------------------------------------


            if (taskObj.LinkedFile != null)
            {
                AppConfig.GetInstance.LL.LogMessage("\t[.]", "_Just_File", $"{filePathFromTask} {args}", ConsoleColor.Gray);


                if (!string.IsNullOrEmpty(args))
                {
                    if (filePathFromTask.IndexOf("rundll32", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        List<string> argsList = SplitCommandLineArguments(args);
                        string dllPathCandidate = null;

                        foreach (string arg in argsList)
                        {
                            if (!arg.StartsWith("/"))
                            {
                                dllPathCandidate = arg;
                                break;
                            }
                        }

                        if (!string.IsNullOrEmpty(dllPathCandidate))
                        {
                            string dllPath = dllPathCandidate.Split(',')[0];

                            string expandedDllPath = Environment.ExpandEnvironmentVariables(dllPath);

                            string resolvedDllPath = FileSystemManager.ResolveExecutablePath(expandedDllPath);

                            if (File.Exists(resolvedDllPath))
                            {
                                FileThreatObject dll = CreateFileObject(resolvedDllPath);
                                AppConfig.GetInstance.LL.LogMessage("[.]", "_Just_File", resolvedDllPath, ConsoleColor.Gray);
                                if (!dll.IsValidSignature)
                                {
                                    AppConfig.GetInstance.LL.LogWarnMediumMessage("_InvalidCertificateSignature", args);
                                    if (!LaunchOptions.GetInstance.ScanOnly)
                                    {
                                        taskObj.ActionDeleteTask = true;
                                        taskObj.ActionDeleteAdditionalFile = true;
                                    }

                                    taskObj.LinkedFileFromArgs = dll;
                                    MarkFileForAction(dll);
                                    taskObj.DetectionReasonRes = "_Rundll32Abuse";
                                    yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);

                                }
                                else
                                {
                                    Logger.WriteLog($"\t[OK]", Logger.success, false);
                                }
                            }
                            else
                            {
                                AppConfig.GetInstance.LL.LogWarnMessage("_FileIsNotFound", resolvedDllPath);
                            }

                        }
                    }

                    if (filePathFromTask.IndexOf("pcalua", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        Match matchRegex = Regex.Match(args, @"-a\s+(?:""(?<filepath>[^""]+)""|(?<filepath>\S+))", RegexOptions.IgnoreCase);
                        string fileFromArgs = "";


                        if (matchRegex.Success)
                        {
                            fileFromArgs = matchRegex.Groups["filepath"].Value;
                        }


                        if (!string.IsNullOrEmpty(fileFromArgs))
                        {
                            string finalPath = "";

                            if (Path.IsPathRooted(fileFromArgs))
                            {
                                finalPath = fileFromArgs;
                            }
                            else
                            {
                                foreach (string checkDir in checkDirectories)
                                {
                                    string combinedPath = Path.Combine(checkDir, fileFromArgs);
                                    if (File.Exists(combinedPath))
                                    {
                                        finalPath = combinedPath;
                                        break;
                                    }
                                }
                            }


                            if (!string.IsNullOrEmpty(finalPath) && string.IsNullOrEmpty(Path.GetExtension(finalPath)))
                            {
                                if (File.Exists(finalPath + ".exe"))
                                {
                                    finalPath += ".exe";
                                }
                            }

                            if (File.Exists(finalPath))
                            {
                                AppConfig.GetInstance.LL.LogMessage("[.]", "_Just_File", finalPath, ConsoleColor.Gray);
                                FileThreatObject dll = CreateFileObject(finalPath);
                                if (!dll.IsValidSignature)
                                {
                                    if (!LaunchOptions.GetInstance.ScanOnly)
                                    {
                                        taskObj.ActionDeleteTask = true;
                                        taskObj.ActionDeleteAdditionalFile = true;
                                    }
                                }

                                taskObj.LinkedFileFromArgs = dll;
                                MarkFileForAction(dll);
                                taskObj.DetectionReasonRes = "_PcaluaAbuse";
                                yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);

                            }
                            else
                            {
                                AppConfig.GetInstance.LL.LogWarnMessage("_FileIsNotFound", fileFromArgs);
                            }
                        }


                    }

                    if (filePathFromTask.IndexOf("regsvr32", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        List<string> argsList = SplitCommandLineArguments(args);

                        if (argsList.Count > 0)
                        {
                            string potentialPath = argsList.Last();

                            string normalizedPath = NormalizePath(potentialPath).Replace("\\\\", "\\");

                            if (normalizedPath.EndsWith(".pfx", StringComparison.OrdinalIgnoreCase) ||
                                normalizedPath.EndsWith(".p12", StringComparison.OrdinalIgnoreCase))
                            {
                                if (File.Exists(normalizedPath))
                                {
                                    FileThreatObject pfx = CreateFileObject(normalizedPath);
                                    if (!pfx.IsValidSignature)
                                    {
                                        if (!LaunchOptions.GetInstance.ScanOnly)
                                        {
                                            taskObj.ActionDeleteTask = true;
                                            taskObj.ActionDeleteAdditionalFile = true;
                                        }
                                    }

                                    taskObj.LinkedFileFromArgs = pfx;
                                    MarkFileForAction(pfx);
                                    taskObj.DetectionReasonRes = "_SuspiciousRegsvr32";
                                    yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);

                                }
                                else
                                {
                                    AppConfig.GetInstance.LL.LogWarnMessage("_FileIsNotFound", normalizedPath);
                                }
                            }

                            if (normalizedPath.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                            {
                                if (File.Exists(normalizedPath) && (normalizedPath.IndexOf("programdata", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                            normalizedPath.IndexOf("appdata", StringComparison.OrdinalIgnoreCase) >= 0))
                                {
                                    FileThreatObject dll = CreateFileObject(normalizedPath);
                                    if (!dll.IsValidSignature)
                                    {
                                        if (!LaunchOptions.GetInstance.ScanOnly)
                                        {
                                            taskObj.ActionDeleteTask = true;
                                            taskObj.ActionDeleteAdditionalFile = true;
                                        }
                                    }

                                    taskObj.LinkedFileFromArgs = dll;
                                    MarkFileForAction(dll);
                                    taskObj.DetectionReasonRes = "_SuspiciousRegsvr32";
                                    yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);
                                }
                            }

                        }

                    }

                    if (args.IndexOf("if exist", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        string expectedSuspiciousPath = ExtractFilePathFromIfExist(args);

                        if (File.Exists(expectedSuspiciousPath))
                        {
                            if (!LaunchOptions.GetInstance.ScanOnly)
                            {
                                taskObj.ActionDeleteTask = true;
                                taskObj.ActionDeleteAdditionalFile = true;
                                taskObj.LinkedFileFromArgs.ShouldDisableExecute = true;
                            }

                            taskObj.LinkedFileFromArgs = CreateFileObject(expectedSuspiciousPath);
                            MarkFileForAction(taskObj.LinkedFileFromArgs);
                            taskObj.DetectionReasonRes = "_Malic1ousTask";
                            yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);
                        }
                        else
                        {
                            AppConfig.GetInstance.LL.LogWarnMessage("_FileIsNotFound", expectedSuspiciousPath);
                        }
                    }
                }

                _analysisResult = _fileAnalyzer.Analyze(taskObj.LinkedFile, true);

                if (_analysisResult.IsMalicious)
                {
                    risk += 3;
                    taskObj.ActionDeleteTask = true;
                    taskObj.LinkedFile.ShouldDisableExecute = true;
                    taskObj.DetectionReasonRes = "_Malic1ousTask";
                    MarkFileForAction(taskObj.LinkedFile);

                    yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);
                }
            }
            else AppConfig.GetInstance.LL.LogWarnMessage("_FileIsNotFound", $"{action.Path} {action.Arguments}");

            if (LaunchOptions.GetInstance.RemoveEmptyTasks)
            {
                taskObj.ActionDeleteTask = true;
                taskObj.DetectionReasonRes = "_EmptyTask";
            }

            // 3 Этап -------------------------------------------

            if (filePathFromTask.IndexOf("powershell", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (args.IndexOf(MSData.GetInstance.queries["disableSmb1Script"], StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    int ps1Index = args.IndexOf(".ps1", StringComparison.OrdinalIgnoreCase);
                    if (ps1Index > 0)
                    {
                        int start = Math.Max(args.LastIndexOf('"', ps1Index), args.LastIndexOf(' ', ps1Index));
                        if (start >= 0)
                        {
                            string ps1Path = args.Substring(start + 1, ps1Index + 4 - start - 1).Trim('"');
                            ps1Path = Environment.ExpandEnvironmentVariables(ps1Path);

                            if (File.Exists(ps1Path))
                            {
                                WinVerifyTrustResult trustResult = WinTrust.GetInstance.VerifyEmbeddedSignature(ps1Path);

                                if (trustResult != WinVerifyTrustResult.Success && trustResult != WinVerifyTrustResult.Error)
                                {
                                    if (!LaunchOptions.GetInstance.ScanOnly)
                                    {
                                        taskObj.ActionDeleteTask = true;
                                        taskObj.ActionDeleteAdditionalFile = true;
                                    }

                                    taskObj.LinkedFileFromArgs = CreateFileObject(ps1Path);
                                    MarkFileForAction(taskObj.LinkedFileFromArgs);
                                    taskObj.DetectionReasonRes = "_Malic1ousTask";
                                    yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);
                                }

                            }
                            else
                            {
                                if (LaunchOptions.GetInstance.RemoveEmptyTasks)
                                {
                                    taskObj.ActionDeleteTask = true;
                                    taskObj.DetectionReasonRes = "_EmptyTask";
                                    yield return new ThreatDecision(taskObj, risk, ScanObjectType.Unknown);
                                }
                            }
                        }
                    }
                }

                string fileTarget = ExtractPwshStartProcessTarget(args);
                if (fileTarget != null)
                {
                    fileTarget = Environment.ExpandEnvironmentVariables(fileTarget);

                    if (File.Exists(fileTarget))
                    {
                        FileThreatObject additionalFile = CreateFileObject(fileTarget);

                        if ((fileTarget.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), StringComparison.OrdinalIgnoreCase) ||
                            fileTarget.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), StringComparison.OrdinalIgnoreCase)) &&
                            !additionalFile.TrustResult.Equals(WinVerifyTrustResult.Success))
                        {

                            if (!LaunchOptions.GetInstance.ScanOnly)
                            {
                                taskObj.ActionDeleteTask = true;
                                taskObj.ActionDeleteAdditionalFile = true;
                                taskObj.LinkedFileFromArgs.ShouldDisableExecute = true;
                            }

                            taskObj.LinkedFileFromArgs = additionalFile;
                            MarkFileForAction(taskObj.LinkedFileFromArgs);
                            taskObj.DetectionReasonRes = "_Malic1ousTask";
                            yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);
                        }
                    }
                    else
                    {
                        if (LaunchOptions.GetInstance.RemoveEmptyTasks)
                        {
                            taskObj.ActionDeleteTask = true;
                            taskObj.DetectionReasonRes = "_EmptyTask";
                            yield return new ThreatDecision(taskObj, risk, ScanObjectType.Unknown);
                        }
                    }
                }

                string normalizedArgs = args.Replace("'", "");
                if (normalizedArgs.Length >= 500 || normalizedArgs.IndexOf(" -e ", StringComparison.OrdinalIgnoreCase) >= 0 || normalizedArgs.IndexOf("-encodedcommand", StringComparison.OrdinalIgnoreCase) >= 0 || normalizedArgs.IndexOf("| iex", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (!LaunchOptions.GetInstance.ScanOnly)
                    {
                        taskObj.ActionDeleteTask = true;
                        MarkFileForAction(taskObj.LinkedFile);
                    }

                    taskObj.DetectionReasonRes = "_Malic1ousTask";
                    yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);

                }
            }

            if ((filePathFromTask.EndsWith(".bat", StringComparison.OrdinalIgnoreCase) || filePathFromTask.EndsWith(".cmd", StringComparison.OrdinalIgnoreCase)))
            {
                if (taskObj.LinkedFile.FileSize >= 1024 * 1024)
                {
                    if (!LaunchOptions.GetInstance.ScanOnly)
                    {
                        taskObj.ActionDeleteTask = true;
                        taskObj.ActionDeleteFile = true;
                    }

                    taskObj.LinkedFile.ShouldDisableExecute = true;
                    MarkFileForAction(taskObj.LinkedFile);
                    taskObj.DetectionReasonRes = "_Malic1ousTask";
                    yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);
                }

            }

            if (filePathFromTask.IndexOf("msiexec", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                foreach (string argsPart in args.Split(' '))
                {
                    if (argsPart.StartsWith("/", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    string msiFile = FileSystemManager.ResolveExecutablePath(argsPart);
                    if (msiFile != null && Path.IsPathRooted(msiFile))
                    {
                        if (!LaunchOptions.GetInstance.ScanOnly)
                        {
                            taskObj.ActionDeleteTask = true;
                            taskObj.ActionDeleteAdditionalFile = true;
                            taskObj.LinkedFileFromArgs.ShouldDisableExecute = true;

                        }

                        taskObj.LinkedFileFromArgs = CreateFileObject(msiFile);
                        MarkFileForAction(taskObj.LinkedFileFromArgs);
                        taskObj.DetectionReasonRes = "_Malic1ousTask";
                        yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);
                    }
                }

            }

            if (filePathFromTask.IndexOf(new StringBuilder("for").Append("files").ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (args.Count(c => c == '^') == 2)
                {
                    string wsfFile = args.Split('^')[1].Remove(0, 1);

                    if (wsfFile.Remove(0, 1).StartsWith(":\\"))
                    {
                        if (File.Exists(wsfFile))
                        {
                            if (!LaunchOptions.GetInstance.ScanOnly)
                            {
                                taskObj.ActionDeleteTask = true;
                                taskObj.ActionDeleteAdditionalFile = true;
                            }

                            taskObj.LinkedFileFromArgs = CreateFileObject(wsfFile);
                            MarkFileForAction(taskObj.LinkedFileFromArgs);
                            taskObj.DetectionReasonRes = "_Malic1ousTask";
                            yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);
                        }
                        else
                        {
                            AppConfig.GetInstance.LL.LogWarnMessage("_FileIsNotFound", wsfFile);
                        }
                    }

                }
            }

            if (filePathFromTask.IndexOf(new StringBuilder("ws").Append("cri").Append("pt").ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (!LaunchOptions.GetInstance.ScanOnly)
                {
                    taskObj.ActionDeleteTask = true;
                    MarkFileForAction(taskObj.LinkedFile);
                }

                taskObj.DetectionReasonRes = "_Malic1ousTask";
                yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);

            }

            if (filePathFromTask.IndexOf("regasm", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                string dllPath = FileSystemManager.ExtractDllPath(args);

                if (!LaunchOptions.GetInstance.ScanOnly)
                {
                    taskObj.ActionDeleteTask = true;
                    taskObj.ActionDeleteAdditionalFile = true;
                    taskObj.LinkedFileFromArgs.ShouldDisableExecute = true;
                }

                taskObj.LinkedFileFromArgs = CreateFileObject(dllPath);
                MarkFileForAction(taskObj.LinkedFileFromArgs);
                taskObj.DetectionReasonRes = "_Malic1ousTask";
                yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);

            }

            if (filePathFromTask.IndexOf("conhost", StringComparison.OrdinalIgnoreCase) >= 0 && args.IndexOf("--headless", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (args.IndexOf(MSData.GetInstance.conhostPatterns["convert-from"], StringComparison.OrdinalIgnoreCase) >= 0 ||
                    args.IndexOf(MSData.GetInstance.conhostPatterns["invoke-pattern"], StringComparison.OrdinalIgnoreCase) >= 0 ||
                    args.IndexOf(MSData.GetInstance.conhostPatterns["policy-bp"], StringComparison.OrdinalIgnoreCase) >= 0 ||
                    args.IndexOf(" -ec ", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    args.IndexOf(" -encodedcommand ", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    args.IndexOf("appdata\\local", StringComparison.OrdinalIgnoreCase) >= 0)
                {

                    if (!LaunchOptions.GetInstance.ScanOnly)
                    {
                        taskObj.ActionDeleteTask = true;
                        MarkFileForAction(taskObj.LinkedFile);
                    }

                    taskObj.DetectionReasonRes = "_Malic1ousTask";
                    yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);
                }
            }

            if (filePathFromTask.EndsWith(@"\node.exe", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(args))
            {
                Match m = Regex.Match(args, @"(?i)""?(?<script>[a-z]:\\[^""]+)""?");

                if (m.Success)
                {
                    string scriptPath = m.Groups["script"].Value.Trim('"');

                    string fullPathToJs = string.Empty;
                    bool invalidFullPath = false;
                    try
                    {
                        fullPathToJs = Path.GetFullPath(scriptPath);
                    }
                    catch
                    {
                        invalidFullPath = true;
                    }

                    if (invalidFullPath)
                    {
                        int score = 0;

                        if (!Path.HasExtension(fullPathToJs))
                        {
                            score += 2;
                        }

                        string fileName = Path.GetFileName(fullPathToJs);
                        if (Regex.IsMatch(fileName, @"^\{?[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}\}?$", RegexOptions.IgnoreCase))
                        {
                            score += 2;
                        }

                        string dirName = Path.GetDirectoryName(fullPathToJs) ?? "";
                        if (Regex.IsMatch(dirName, @"\{?[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}\}?", RegexOptions.IgnoreCase))
                        {
                            score += 2;
                        }


                        if (fullPathToJs.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.Windows), StringComparison.OrdinalIgnoreCase))
                        {
                            score += 3;
                        }

                        if (File.Exists(fullPathToJs) && FileChecker.IsJsUnreadable(fullPathToJs))
                        {
                            score += 2;
                        }
                        else if (!File.Exists(fullPathToJs))
                        {
                            if (LaunchOptions.GetInstance.RemoveEmptyTasks)
                            {
                                taskObj.ActionDeleteTask = true;
                            }
                        }

                        if (score >= 5)
                        {
                            if (!LaunchOptions.GetInstance.ScanOnly)
                            {
                                taskObj.ActionDeleteTask = true;
                                taskObj.ActionDeleteFile = true;
                                taskObj.LinkedFile.ShouldDisableExecute = true;
                                MarkFileForAction(taskObj.LinkedFile);
                            }

                            taskObj.LinkedFileFromArgs = CreateFileObject(fullPathToJs);
                            MarkFileForAction(taskObj.LinkedFileFromArgs);
                            taskObj.DetectionReasonRes = "_Malic1ousTask";
                            yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);
                        }
                    }
                }
            }

            if (filePathFromTask.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) &&
                ((filePathFromTask.IndexOf("\\pro\\", StringComparison.OrdinalIgnoreCase) >= 0) ||
                args.Equals("/LHS", StringComparison.OrdinalIgnoreCase) ||
                args.Equals("/T", StringComparison.OrdinalIgnoreCase)))
            {
                if (!LaunchOptions.GetInstance.ScanOnly)
                {
                    taskObj.ActionDeleteTask = true;
                    taskObj.ActionDeleteFile = true;
                    taskObj.LinkedFile.ShouldDisableExecute = true;
                    MarkFileForAction(taskObj.LinkedFile);
                }

                taskObj.DetectionReasonRes = "_Malic1ousTask";
                yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);
            }

            if (filePathFromTask.IndexOf("msbuild.exe", StringComparison.OrdinalIgnoreCase) >= 0 || args.IndexOf("msbuild.exe", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (!LaunchOptions.GetInstance.ScanOnly)
                {
                    taskObj.ActionDeleteTask = true;
                    MarkFileForAction(taskObj.LinkedFile);
                }

                taskObj.DetectionReasonRes = "_Malic1ousTask";
                yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);
            }

            if (filePathFromTask.StartsWith(Environment.GetEnvironmentVariable("AppData"), StringComparison.OrdinalIgnoreCase))
            {
                if (args.Equals(new StringBuilder("/v").Append("er").Append("ys").Append("il").Append("en").Append("t").ToString(), StringComparison.OrdinalIgnoreCase) || (FileChecker.IsExecutable(filePathFromTask) && string.IsNullOrEmpty(Path.GetExtension(filePathFromTask))))
                {
                    if (!LaunchOptions.GetInstance.ScanOnly)
                    {
                        taskObj.ActionDeleteTask = true;
                        taskObj.ActionDeleteFile = true;
                        taskObj.LinkedFile.ShouldDisableExecute = true;
                        MarkFileForAction(taskObj.LinkedFile);
                    }

                    taskObj.DetectionReasonRes = "_Malic1ousTask";
                    yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);

                }

            }

            if (IsInSuspiciousLocation(filePathFromTask) && (IsSuspiciousDirectoryNameWithClsid(filePathFromTask) || IsSystemFileName(filePathFromTask)))
            {
                if (!LaunchOptions.GetInstance.ScanOnly)
                {
                    taskObj.ActionDeleteTask = true;
                    taskObj.ActionDeleteFile = true;
                    taskObj.LinkedFile.ShouldDisableExecute = true;
                    MarkFileForAction(taskObj.LinkedFile);
                }

                taskObj.DetectionReasonRes = "_Malic1ousTask";
                yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);
            }

            if (!string.IsNullOrEmpty(filePathFromTask) && (taskObj.LinkedFile.FileSize >= taskObj.LinkedFile.MAX_FILE_SIZE || FileChecker.IsJarFile(filePathFromTask) || (FileChecker.IsDotNetAssembly(filePathFromTask) && FileSystemManager.HasHiddenAttribute(filePathFromTask))))
            {
                if (!LaunchOptions.GetInstance.ScanOnly)
                {
                    taskObj.ActionDeleteTask = true;
                    taskObj.ActionDeleteFile = true;
                    taskObj.LinkedFile.ShouldDisableExecute = true;
                    MarkFileForAction(taskObj.LinkedFile);
                }

                taskObj.DetectionReasonRes = "_Malic1ousTask";
                yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);
            }

            bool hasMultiExe = FileSystemManager.HasMultipleExeExtensions(filePathFromTask);
            bool isUpx = FileChecker.IsUpxPacked(filePathFromTask);

            if (isUpx && (FileSystemManager.HasHiddenAttribute(filePathFromTask) || HasGuidIdentifier(filePathFromTask) || hasMultiExe))
            {
                if (!LaunchOptions.GetInstance.ScanOnly)
                {
                    taskObj.ActionDeleteTask = true;
                    taskObj.ActionDeleteFile = true;
                    taskObj.LinkedFile.ShouldDisableExecute = true;
                    MarkFileForAction(taskObj.LinkedFile);
                }

                taskObj.DetectionReasonRes = "_Malic1ousTask";
                yield return new ThreatDecision(taskObj, risk, ScanObjectType.Malware);
            }

            if (!string.IsNullOrEmpty(filePathFromTask) && _analysisResult.IsSuspicious) /* is sfx archive*/
            {
                if (!LaunchOptions.GetInstance.ScanOnly)
                {
                    taskObj.ActionDeleteTask = true;
                    taskObj.ActionDeleteFile = true;
                    taskObj.LinkedFile.ShouldDisableExecute = true;
                    MarkFileForAction(taskObj.LinkedFile);
                }

                taskObj.DetectionReasonRes = "_sfxArchive";
                yield return new ThreatDecision(taskObj, risk, ScanObjectType.Unsafe);
            }

            int filesCount = 0;
            if (!string.IsNullOrEmpty(filePathFromTask))
            {
                foreach (string file in Directory.EnumerateFiles(Path.GetDirectoryName(filePathFromTask), "*.*", SearchOption.TopDirectoryOnly))
                {
                    filesCount++;
                }
            }

            if (filesCount.Equals(1) && FileSystemManager.HasHiddenAttribute(filePathFromTask) && taskObj.LinkedFile.FileSize > 1024 * 1024)
            {
                if (!LaunchOptions.GetInstance.ScanOnly)
                {
                    taskObj.ActionDeleteTask = true;
                    taskObj.ActionDeleteFile = true;
                    taskObj.LinkedFile.ShouldDisableExecute = true;
                    MarkFileForAction(taskObj.LinkedFile);
                }

                taskObj.DetectionReasonRes = "_FileSize";
                yield return new ThreatDecision(taskObj, risk, ScanObjectType.Unsafe);
            }

            // Решения для связанных файлов (если есть флаги действия)
            if (taskObj.LinkedFile != null &&
                (taskObj.LinkedFile.ShouldDeleteFile ||
                 taskObj.LinkedFile.ShouldMoveFileToQuarantine ||
                 taskObj.LinkedFile.ShouldDisableExecute))
            {
                yield return new ThreatDecision(taskObj.LinkedFile, risk, ScanObjectType.Malware);
            }

            if (taskObj.LinkedFileFromArgs != null &&
                (taskObj.LinkedFileFromArgs.ShouldDeleteFile ||
                 taskObj.LinkedFileFromArgs.ShouldMoveFileToQuarantine ||
                 taskObj.LinkedFileFromArgs.ShouldDisableExecute))
            {
                yield return new ThreatDecision(taskObj.LinkedFileFromArgs, risk, ScanObjectType.Malware);
            }
        }

        // Вспомогательные методы внутри Анализатора ------------------------------

        FileThreatObject CreateFileObject(string path)
        {
            if (!File.Exists(path)) return null;
            var trust = WinTrust.GetInstance.VerifyEmbeddedSignature(path, true);
            var fileInfo = new FileInfo(path);
            var versionInfo = FileVersionInfo.GetVersionInfo(path);
            string originalName = versionInfo.OriginalFilename ?? string.Empty;
            string description = versionInfo.FileDescription ?? string.Empty;

            return new FileThreatObject(path, Path.GetFileName(path), fileInfo.Length, originalName, description, FileChecker.CalculateSHA1(path), trust);
        }

        string ExtractFilePathFromIfExist(string arguments)
        {
            Match match = IfExistPathRegex.Match(arguments);

            if (match.Success)
            {
                return match.Groups["filepath"].Value;
            }

            return null;
        }

        string ExtractPwshStartProcessTarget(string arguments)
        {
            int idx = arguments.IndexOf("start-process", StringComparison.OrdinalIgnoreCase);
            if (idx < 0)
                return null;

            string tail = arguments.Substring(idx + "start-process".Length);

            int argListIdx = tail.IndexOf("-argumentlist", StringComparison.OrdinalIgnoreCase);
            if (argListIdx > 0)
                tail = tail.Substring(0, argListIdx);

            tail = Regex.Replace(tail,
                @"-(filepath|windowstyle|verb|workingdirectory)\s+",
                "",
                RegexOptions.IgnoreCase);

            var m = Regex.Match(tail, @"(""[^""]+""|\S+)");
            if (!m.Success)
                return null;

            return m.Value.Trim('"');
        }

        bool IsInSuspiciousLocation(string filePath)
        {
            return filePath.IndexOf(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), StringComparison.OrdinalIgnoreCase) >= 0 ||
                              filePath.IndexOf(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), StringComparison.OrdinalIgnoreCase) >= 0 ||
                              filePath.IndexOf(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), StringComparison.OrdinalIgnoreCase) >= 0 ||
                              filePath.IndexOf(Environment.GetEnvironmentVariable("PUBLIC"), StringComparison.OrdinalIgnoreCase) >= 0;
        }

        bool IsSystemFileName(string filePath)
        {
            string fileName = Path.GetFileName(filePath);

            HashSet<string> sysFileNamesHashset = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            sysFileNamesHashset.Clear();

            foreach (string _fileName in MSData.GetInstance.SysFileName)
            {
                if (!string.IsNullOrEmpty(_fileName))
                {
                    sysFileNamesHashset.Add(_fileName.Trim() + ".exe");
                }
            }

            return sysFileNamesHashset.Contains(fileName.ToLowerInvariant());

        }


        bool IsSuspiciousDirectoryNameWithClsid(string filePath)
        {
            string directory = Path.GetDirectoryName(filePath) ?? string.Empty;
            string directoryName = Path.GetFileName(directory) ?? string.Empty;

            if (!string.IsNullOrEmpty(directoryName) && directoryName.Contains(".{") && directoryName.EndsWith("}"))
            {
                if (Regex.IsMatch(directoryName, @"^.*\.{[0-9A-F]{8}(-[0-9A-F]{4}){3}-[0-9A-F]{12}\}$", RegexOptions.IgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        bool HasGuidIdentifier(string filePath)
        {
            string dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir) && Directory.Exists(dir))
            {
                return Directory.EnumerateFiles(dir, "*", SearchOption.TopDirectoryOnly)
                    .Select(Path.GetFileNameWithoutExtension)
                    .Any(n => n != null && Guid.TryParse(n, out _));
            }
            return false;
        }

        List<string> SplitCommandLineArguments(string commandLine)
        {
            var args = new List<string>();
            var matches = Regex.Matches(commandLine, @"""[^""]+""|\S+");
            foreach (Match match in matches)
            {
                args.Add(match.Value.Trim('"'));
            }
            return args;
        }

        string NormalizePath(string rawPath)
        {
            if (string.IsNullOrEmpty(rawPath))
            {
                return rawPath;
            }

            int driveLetterPos = rawPath.IndexOf(":\\");
            if (driveLetterPos > 0)
            {
                return rawPath.Substring(driveLetterPos - 1);
            }

            return rawPath;
        }

        void MarkFileForAction(FileThreatObject file)
        {
            if (file == null) return;

            if (IsKnownMaliciousFile(file.FilePath))
            {
                file.ShouldDeleteFile = true;
            }
            else if (!file.IsValidSignature)
            {
                file.ShouldMoveFileToQuarantine = true;
            }
            // Если файл подписан — не трогаем его (это может быть системный файл)
        }

        bool IsKnownMaliciousFile(string filePath)
        {
            return MSData.GetInstance.obfStr2.Any(s =>
                FileSystemManager.NormalizeExtendedPath(s).Equals(filePath, StringComparison.OrdinalIgnoreCase));
        }
    }
}
