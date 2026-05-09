using MSearch.Core.Managers;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.Diagnostics;
using Win32Wrapper;

namespace MSearch.Core.Handlers
{
    /// <summary>
    /// Используется для обработки процессов на этапе очистки
    /// </summary>
    internal sealed class ProcessThreatHandler : IThreatHandler
    {
        public ThreatObjectKind Kind => ThreatObjectKind.Process;

        public ApplyResult Apply(ThreatDecision decision, CleanupPhase phase)
        {
            var proc = decision.Target as ProcessThreatObject;
            if (proc == null) return ApplyResult.NotApplicable;
            if (decision.RiskLevel < 3) return ApplyResult.Skipped;

            if (phase == CleanupPhase.SuspendOnly)
            {
                if (!proc.ShouldSuspend) return ApplyResult.Skipped;
                if (proc.WasSuspended) return ApplyResult.NotApplicable;

                ProcessManager.SuspendProcess(proc.ProcessId);
                if (proc.FileProcess.ShouldDisableExecute)
                {
                    UnlockObjectClass.DisableExecute(proc.FileProcess.FilePath);
                }

                proc.WasSuspended = true;
                proc.ShouldTerminate = true;
                decision.ActionType = ScanActionType.Suspended;
                return ApplyResult.Success;
            }

            // Finalize
            if (!proc.ShouldTerminate) return ApplyResult.Skipped;
            if (!proc.WasSuspended) return ApplyResult.Skipped;

            try
            {
                using (var p = Process.GetProcessById(proc.ProcessId))
                {
                    string pname = p.ProcessName;

                    if (!LaunchOptions.GetInstance.ScanOnly)
                    {
                        UnProtect(p.Id);
                        p.Kill();

                        if (p.HasExited)
                        {
                            AppConfig.GetInstance.LL.LogSuccessMessage("_ProcessTerminated", $"{pname}, PID: {proc.ProcessId}");
                            proc.ShouldTerminate = false;
                            proc.WasSuspended = false;
                            decision.ActionType = ScanActionType.Terminated;
                            return ApplyResult.Success;
                        }
                        else
                        {
                            decision.ActionType = ScanActionType.Error;
                            return ApplyResult.Failed;
                        }
                    }
                    else
                    {
                        AppConfig.GetInstance.LL.LogCautionMessage("_Malici0usProcess", $"{pname} - PID: {proc.ProcessId}");
                        return ApplyResult.Skipped;
                    }
                }
            }
            catch (ArgumentException)
            {
                proc.WasSuspended = false;
                proc.ShouldTerminate = false;
                decision.ActionType = ScanActionType.Skipped;
                return ApplyResult.NotApplicable;
            }
            catch (Exception ex)
            {
                decision.ApplyErrorMessage = ex.Message;
                decision.ActionType = ScanActionType.Error;
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorTerminateProcess", ex);
                return ApplyResult.Error;
            }
        }

        void UnProtect(int pid)
        {
            int _pid = 0;
            int isCritical = 0;
            int BreakOnTermination = 0x1D;

            if (pid == 0 || pid == -1)
            {
                return;
            }

            try
            {
                _pid = pid;
                IntPtr handle = Native.OpenProcess(0x001F0FFF, false, pid);
                Native.NtSetInformationProcess(handle, BreakOnTermination, ref isCritical, sizeof(int));
                Native.CloseHandle(handle);
            }
            catch (InvalidOperationException ioe) when (ioe.HResult.Equals(unchecked((int)0x80070057)))
            {
                AppConfig.GetInstance.LL.LogWarnMessage("_ProcessNotRunning", $"PID: {_pid}");
            }
            catch (Exception e) when (e.HResult.Equals(unchecked((int)0x80131509)))
            {
                AppConfig.GetInstance.LL.LogWarnMessage("_ProcessNotRunning", _pid.ToString());
            }
            catch (System.ComponentModel.Win32Exception) { }
            catch (Exception e)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_ErrorTerminateProcess", e);
            }

        }
    }
}
