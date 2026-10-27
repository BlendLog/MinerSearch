using System;

namespace MSearch.Core
{
    /// <summary>
    /// Канал «текущий сканируемый объект». Сканеры сообщают сюда путь объекта,
    /// подписчик (движок) пересылает его в UI. Логирование не затрагивается.
    /// </summary>
    public static class ScanActivity
    {
        public static event Action<string> ObjectChanged;

        /// <summary>Есть ли подписчики: позволяет не собирать строки впустую.</summary>
        public static bool HasSubscribers
        {
            get { return ObjectChanged != null; }
        }

        public static void Report(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            Action<string> handler = ObjectChanged;
            if (handler != null)
                handler(value);
        }
    }
}
