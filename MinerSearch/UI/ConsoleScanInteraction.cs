using MSearch.Core;
using MSearch.Core.ThreatDecisions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MSearch.UI
{
    /// <summary>
    /// Консольная реализация IScanInteraction: сохраняет текущее поведение консольных режимов.
    /// </summary>
    internal sealed class ConsoleScanInteraction : IScanInteraction
    {
        public bool ConfirmAutoAcceptUnknown()
        {
            DialogResult result = DialogDispatcher.Show(
                AppConfig.GetInstance.LL.GetLocalizedString("_ReviewAutoAccept_Question"),
                AppConfig.GetInstance.LL.GetLocalizedString("_ReviewAutoAccept_Title"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            return result == DialogResult.Yes;
        }

        public void ShowThreatReview(List<ThreatDecision> decisions)
        {
            // Консольный режим: обзор угроз отложен, применяются рекомендованные действия.
        }

        public void WaitBeforeCleanup()
        {
            LocalizedLogger.LogPAUSE();
            if (AppConfig.GetInstance.IsGuiAvailable)
            {
                ConsoleBridge.SafeReadKey(true);
            }
        }
    }
}
