using MSearch.Core;

namespace MSearch.Core.ThreatObjects
{
    /// <summary>
    /// Объект данных о обнаруженном рутките R77.
    /// IsRootkitPresent устанавливается сканером при обнаружении заражения текущего процесса.
    /// R77Processes и DialerPids заполняются анализатором.
    /// WasNeutralized и ConfigRemoved заполняются обработчиком.
    /// </summary>
    public sealed class RootkitThreatObject : ThreatObject
    {
        /// <summary>
        /// true, если текущий процесс заражён R77 (установлено сканером).
        /// </summary>
        public bool IsRootkitPresent { get; }

        /// <summary>
        /// Список процессов с руткит-подписями (R77/R77_SERVICE/R77_HELPER).
        /// Заполняется анализатором.
        /// </summary>
        public Win32Wrapper.Native.R77_PROCESS[] R77Processes { get; internal set; }

        /// <summary>
        /// PID процессов dialer, которые были приостановлены.
        /// Заполняется анализатором.
        /// </summary>
        public int[] DialerPids { get; internal set; }

        /// <summary>
        /// true, если все процессы R77 были успешно нейтрализованы.
        /// Заполняется обработчиком.
        /// </summary>
        public bool WasNeutralized { get; internal set; }

        /// <summary>
        /// true, если конфигурация руткита была удалена из реестра.
        /// Заполняется обработчиком.
        /// </summary>
        public bool ConfigRemoved { get; internal set; }

        public RootkitThreatObject(bool isRootkitPresent)
            : base(ThreatObjectKind.Other, "Rootkit")
        {
            IsRootkitPresent = isRootkitPresent;
        }
    }
}
