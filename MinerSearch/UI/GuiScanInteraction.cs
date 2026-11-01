using MSearch.Core;
using MSearch.Core.ThreatDecisions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MSearch.UI
{
    /// <summary>
    /// IScanInteraction implementation for the GUI: all dialogs run on the UI thread.
    /// </summary>
    internal sealed class GuiScanInteraction : IScanInteraction
    {
        private readonly FormMain _form;

        public GuiScanInteraction(FormMain form)
        {
            _form = form;
        }

        public bool ConfirmAutoAcceptUnknown()
        {
            return (bool)_form.Invoke(new Func<bool>(_form.ConfirmAutoAcceptUnknown));
        }

        public void ShowThreatReview(List<ThreatDecision> decisions)
        {
            // Показываем страницу в UI-потоке и ждём решения пользователя.
            ManualResetEventSlim gate = new ManualResetEventSlim(false);
            try
            {
                _form.BeginInvoke(new Action(delegate { _form.BeginThreatReview(decisions, gate); }));
                gate.Wait();
            }
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException) { }
            finally
            {
                gate.Dispose();
            }
        }

        public void WaitBeforeCleanup()
        {
            _form.Invoke(new Action(_form.WaitBeforeCleanup));
        }
    }
}
