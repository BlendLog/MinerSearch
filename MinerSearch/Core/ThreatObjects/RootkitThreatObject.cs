using MSearch.Core;

namespace MSearch.Core.ThreatObjects
{
    /// <summary>
    /// Объект-данных о обнаруженном рутките R77.
    /// Содержит информацию о процессах с руткит-подписями и статусе нейтрализации.
    /// </summary>
    public sealed class RootkitThreatObject : ThreatObject
    {
        /// <summary>
        /// Список процессов с руткит-подписями (R77/R77_SERVICE/R77_HELPER).
        /// </summary>
        public int[] ProcessIds { get; }

        /// <summary>
        /// Сигнатуры найденных руткитов (R77_SIGNATURE, R77_SERVICE_SIGNATURE, R77_HELPER_SIGNATURE).
        /// </summary>
        public uint[] Signatures { get; }

        /// <summary>
        /// PID процессов dialer, которые были приостановлены.
        /// </summary>
        public int[] DialerPids { get; }

        /// <summary>
        /// true, если все процессы R77 были успешно нейтрализованы.
        /// </summary>
        public bool WasNeutralized { get; }

        /// <summary>
        /// true, если конфигурация руткита была удалена из реестра.
        /// </summary>
        public bool ConfigRemoved { get; }

        public RootkitThreatObject(
            int[] processIds,
            uint[] signatures,
            int[] dialerPids,
            bool wasNeutralized,
            bool configRemoved)
            : base(ThreatObjectKind.Other, "Rootkit")
        {
            ProcessIds = processIds;
            Signatures = signatures;
            DialerPids = dialerPids;
            WasNeutralized = wasNeutralized;
            ConfigRemoved = configRemoved;
        }
    }
}
