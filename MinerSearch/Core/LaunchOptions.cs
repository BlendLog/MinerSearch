using System;
using System.Collections.Generic;
using MSearch.Infrastructure;

namespace MSearch.Core
{
    /// <summary>
    /// Application launch parameters (singleton)
    /// </summary>
    public class LaunchOptions
    {
        #region Singleton

        static volatile LaunchOptions _instance;
        static readonly object _lock = new object();

        private LaunchOptions() { }

        public static LaunchOptions GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new LaunchOptions();
                        }
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Parsing state

        /// <summary>Parse error flag</summary>
        public bool hasErrors { get; internal set; }

        /// <summary>List of parse errors</summary>
        public List<string> errors { get; internal set; } = new List<string>();

        /// <summary>Parse success flag</summary>
        public bool parsed { get; internal set; }

        #endregion

        #region Flags

        /// <summary>--help / -h — show help</summary>
        public bool help { get; internal set; }

        /// <summary>--force / -f — suppress confirmations</summary>
        public bool force { get; internal set; }

        /// <summary>--silent / -si — silent mode</summary>
        public bool silent { get; internal set; }

        /// <summary>--no-scantime / -nstm — scan only processes</summary>
        public bool no_scantime { get; internal set; }

        /// <summary>--no-scan-wmi / -nwmi — skip WMI scanning</summary>
        public bool no_scan_wmi { get; internal set; }

        /// <summary>--no-signature-scan / -nss — skip signature check</summary>
        public bool nosignaturescan { get; internal set; }

        /// <summary>--no-runtime / -nr — do not scan processes</summary>
        public bool no_runtime { get; internal set; }

        /// <summary>--no-scan-registry / -nsr — skip registry scanning</summary>
        public bool no_scan_registry { get; internal set; }

        /// <summary>--no-scan-tasks / -nst — skip task scanning</summary>
        public bool no_scan_tasks { get; internal set; }

        /// <summary>--no-scan-users / -nsu — skip users profiles scanning</summary>
        public bool no_scan_users { get; internal set; }

        /// <summary>--pause / -p — pause before cleanup</summary>
        public bool pause { get; internal set; }

        /// <summary>--remove-empty-tasks / -ret — remove empty tasks</summary>
        public bool RemoveEmptyTasks { get; internal set; }

        /// <summary>--no-rootkit-check / -nrc — disable rootkit check</summary>
        public bool no_rootkit_check { get; internal set; }

        /// <summary>--no-services / -nse — skip service scanning</summary>
        public bool no_services { get; internal set; }

        /// <summary>--scan-only / -so — scan only, no cleanup</summary>
        public bool ScanOnly { get; internal set; }

        /// <summary>--full-scan / -fs — full disk scan</summary>
        public bool fullScan { get; internal set; }

        /// <summary>--open-quarantine / -q — open quarantine manager</summary>
        public bool QuarantineMode { get; internal set; }

        /// <summary>--winpemode / -w — WinPE mode</summary>
        public bool winpemode { get; internal set; }

        /// <summary>-R — WMI restored</summary>
        public bool RestoredWMI { get; internal set; }

        /// <summary>--verbose / -v — verbose output</summary>
        public bool verbose { get; internal set; }

        /// <summary>--select / -s (without value) — request directory selection</summary>
        public bool demandSelection { get; internal set; }

        /// <summary>--no-check-hosts / -nch — skip hosts check</summary>
        public bool no_check_hosts { get; internal set; }

        /// <summary>--no-firewall / -nfw — skip firewall scanning</summary>
        public bool no_firewall { get; internal set; }

        /// <summary>--no-logs / -nl — do not write logs to file</summary>
        public bool no_logs { get; internal set; }

        /// <summary>--accept-eula / -a — accept license agreement</summary>
        public bool accept_eula { get; internal set; }

        /// <summary>--console-mode / -cm — console mode</summary>
        public bool console_mode { get; internal set; }

        /// <summary>--force / -f</summary>
        public bool Force { get; internal set; }
        #endregion

        #region Values with arguments

        /// <summary>--depth= / -d= — maximum scan depth</summary>
        public int maxSubfolders { get; internal set; }

        /// <summary>--select= / -s= — path to scan</summary>
        public string selectedPath { get; internal set; }

        /// <summary>--restore= / -res= or --delete= / -del= — quarantine item list</summary>
        public string quarantineListEnum { get; internal set; }

        /// <summary>Flag for quarantine restore</summary>
        public bool QuarantineRestoreOption { get; internal set; }

        /// <summary>Flag for quarantine delete</summary>
        public bool QuarantineDeleteOption { get; internal set; }

        #endregion

        #region Reset and parsing

        /// <summary>
        /// Resets all fields to default values
        /// </summary>
        internal void _reset()
        {
            hasErrors = false;
            errors.Clear();
            parsed = false;

            help = force = silent = no_scantime = no_scan_wmi = nosignaturescan = false;
            no_runtime = no_scan_registry = no_scan_tasks = pause = false;
            RemoveEmptyTasks = no_rootkit_check = no_services = false;
            ScanOnly = fullScan = QuarantineMode = winpemode = false;
            RestoredWMI = verbose = demandSelection = false;
            no_check_hosts = no_firewall = no_logs = accept_eula = false;
            console_mode = false;

            maxSubfolders = 8;
            selectedPath = null;
            quarantineListEnum = null;
            QuarantineRestoreOption = false;
            QuarantineDeleteOption = false;
        }

        /// <summary>
        /// Parses command line arguments
        /// </summary>
        public static void ParseArgs(string[] args)
        {
            var inst = GetInstance;
            inst._reset();

            if (args == null || args.Length == 0)
            {
                inst.parsed = true;
                return;
            }

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];

                // --help / -h
                if (arg.Equals("--help", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-h", StringComparison.OrdinalIgnoreCase))
                {
                    inst.help = true;
                    continue;
                }

                if (arg.Equals("--no-logs", StringComparison.OrdinalIgnoreCase) ||
                     arg.Equals("-nl", StringComparison.OrdinalIgnoreCase))
                {
                    inst.no_logs = true;
                    continue;
                }

                // --force / -f
                if (arg.Equals("--force", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-f", StringComparison.OrdinalIgnoreCase))
                {
                    inst.force = true;
                    continue;
                }

                // --silent / -si
                if (arg.Equals("--silent", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-si", StringComparison.OrdinalIgnoreCase))
                {
                    inst.silent = true;
                    continue;
                }

                // --no-scantime / -nstm
                if (arg.Equals("--no-scantime", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-nstm", StringComparison.OrdinalIgnoreCase))
                {
                    inst.no_scantime = true;
                    continue;
                }

                // --no-scan-wmi / -nwmi
                if (arg.Equals("--no-scan-wmi", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-nwmi", StringComparison.OrdinalIgnoreCase))
                {
                    inst.no_scan_wmi = true;
                    continue;
                }

                // --no-signature-scan / -nss
                if (arg.Equals("--no-signature-scan", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-nss", StringComparison.OrdinalIgnoreCase))
                {
                    inst.nosignaturescan = true;
                    continue;
                }

                // --no-runtime / -nr
                if (arg.Equals("--no-runtime", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-nr", StringComparison.OrdinalIgnoreCase))
                {
                    inst.no_runtime = true;
                    continue;
                }

                // --no-scan-registry / -nsr
                if (arg.Equals("--no-scan-registry", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-nsr", StringComparison.OrdinalIgnoreCase))
                {
                    inst.no_scan_registry = true;
                    continue;
                }

                // --no-scan-tasks / -nst
                if (arg.Equals("--no-scan-tasks", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-nst", StringComparison.OrdinalIgnoreCase))
                {
                    inst.no_scan_tasks = true;
                    continue;
                }

                // --no-scan-users / -nsu
                if (arg.Equals("--no-scan-users", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-nsu", StringComparison.OrdinalIgnoreCase))
                {
                    inst.no_scan_users = true;
                    continue;
                }

                // --pause / -p
                if (arg.Equals("--pause", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-p", StringComparison.OrdinalIgnoreCase))
                {
                    inst.pause = true;
                    continue;
                }

                // --remove-empty-tasks / -ret
                if (arg.Equals("--remove-empty-tasks", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-ret", StringComparison.OrdinalIgnoreCase))
                {
                    inst.RemoveEmptyTasks = true;
                    continue;
                }

                // --depth= / -d=
                if (arg.StartsWith("--depth=", StringComparison.OrdinalIgnoreCase) ||
                    arg.StartsWith("-d=", StringComparison.OrdinalIgnoreCase))
                {
                    string depthValue = arg.Substring(arg.IndexOf('=') + 1);

                    if (!int.TryParse(depthValue, out int depth) || depth <= 0 || depth > 16)
                    {
                        inst.hasErrors = true;
                        inst.errors.Add(AppConfig.GetInstance.LL.GetLocalizedString("_DepthInvalidValue").Replace("#ARG#", arg));
                        return;
                    }

                    inst.maxSubfolders = depth;
                    continue;
                }

                // --no-rootkit-check / -nrc
                if (arg.Equals("--no-rootkit-check", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-nrc", StringComparison.OrdinalIgnoreCase))
                {
                    inst.no_rootkit_check = true;
                    continue;
                }

                // --no-services / -nse
                if (arg.Equals("--no-services", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-nse", StringComparison.OrdinalIgnoreCase))
                {
                    inst.no_services = true;
                    continue;
                }

                // --scan-only / -so
                if (arg.Equals("--scan-only", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-so", StringComparison.OrdinalIgnoreCase))
                {
                    inst.ScanOnly = true;
                    inst.no_rootkit_check = true;
                    inst.RemoveEmptyTasks = false;
                    continue;
                }

                // --full-scan / -fs
                if (arg.Equals("--full-scan", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-fs", StringComparison.OrdinalIgnoreCase))
                {
                    inst.fullScan = true;
                    continue;
                }

                // --open-quarantine / -q
                if (arg.Equals("--open-quarantine", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-q", StringComparison.OrdinalIgnoreCase))
                {
                    inst.QuarantineMode = true;
                    continue;
                }

                // --winpemode / -w
                if (arg.Equals("--winpemode", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-w", StringComparison.OrdinalIgnoreCase))
                {
                    inst.winpemode = true;
                    continue;
                }

                // -R
                if (arg.Equals("-R", StringComparison.Ordinal))
                {
                    inst.RestoredWMI = true;
                    continue;
                }

                // --verbose / -v
                if (arg.Equals("--verbose", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-v", StringComparison.OrdinalIgnoreCase))
                {
                    inst.verbose = true;
                    continue;
                }

                // --select / -s (без значения)
                if (arg.Equals("--select", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-s", StringComparison.OrdinalIgnoreCase))
                {
                    inst.demandSelection = true;
                    continue;
                }

                // --select= / -s= (с путём)
                if (arg.Equals("--select=", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-s=", StringComparison.OrdinalIgnoreCase))
                {
                    inst.demandSelection = true;
                    if (i + 1 >= args.Length)
                    {
                        inst.hasErrors = true;
                        inst.errors.Add(AppConfig.GetInstance.LL.GetLocalizedString("_ErrorSpecifiedPathIsNull").Replace("#ARG#", arg));
                        return;
                    }

                    string expectedPath = args[++i];
                    if (!System.IO.Directory.Exists(expectedPath))
                    {
                        inst.hasErrors = true;
                        inst.errors.Add(AppConfig.GetInstance.LL.GetLocalizedString("_ErrorSpecifiedPathNotFound").Replace("#PATH#", expectedPath));
                        return;
                    }

                    inst.selectedPath = expectedPath;
                    continue;
                }

                // --restore= / -res=
                if (arg.Equals("--restore=", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-res=", StringComparison.OrdinalIgnoreCase))
                {
                    inst.QuarantineRestoreOption = true;
                    if (i + 1 >= args.Length)
                    {
                        inst.hasErrors = true;
                        inst.errors.Add(AppConfig.GetInstance.LL.GetLocalizedString("_ErrorQuarantineListEnumIsNull"));
                        return;
                    }

                    string expectedList = args[++i];
                    if (expectedList.IndexOf(',') >= 0 || int.TryParse(expectedList, out _))
                    {
                        inst.quarantineListEnum = expectedList;
                    }
                    else
                    {
                        inst.hasErrors = true;
                        inst.errors.Add(AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_RestoreCommandSyntax"));
                        return;
                    }
                    continue;
                }

                // --delete= / -del=
                if (arg.Equals("--delete=", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-del=", StringComparison.OrdinalIgnoreCase))
                {
                    inst.QuarantineDeleteOption = true;
                    if (i + 1 >= args.Length)
                    {
                        inst.hasErrors = true;
                        inst.errors.Add(AppConfig.GetInstance.LL.GetLocalizedString("_ErrorQuarantineListEnumIsNull"));
                        return;
                    }

                    string expectedList = args[++i];
                    if (expectedList.IndexOf(',') >= 0 || int.TryParse(expectedList, out _))
                    {
                        inst.quarantineListEnum = expectedList;
                    }
                    else
                    {
                        inst.hasErrors = true;
                        inst.errors.Add(AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_RestoreCommandSyntax"));
                        return;
                    }
                    continue;
                }

                // --no-check-hosts / -nch
                if (arg.Equals("--no-check-hosts", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-nch", StringComparison.OrdinalIgnoreCase))
                {
                    inst.no_check_hosts = true;
                    continue;
                }

                // --no-firewall / -nfw
                if (arg.Equals("--no-firewall", StringComparison.OrdinalIgnoreCase) ||
                    arg.Equals("-nfw", StringComparison.OrdinalIgnoreCase))
                {
                    inst.no_firewall = true;
                    continue;
                }

                // Пассивные аргументы
                if (PassiveArgs.Contains(arg))
                    continue;

                // Неизвестный аргумент
                LocalizedLogger.LogUnknownCommand(arg);
                inst.hasErrors = true;
                return;
            }

            inst.parsed = true;
        }

        #endregion

        #region Passive arguments

        private static readonly HashSet<string> PassiveArgs = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "--no-logs", "-nl",
            "--accept-eula", "-a",
            "--console-mode", "-cm",
            "--silent", "-si"
        };

        #endregion
    }
}
