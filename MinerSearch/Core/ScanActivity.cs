using System;

namespace MSearch.Core
{
    /// <summary>
    /// Current scanned object channel. Scanners report the object path here;
    /// the subscriber (engine) forwards it to the UI. Logging is unaffected.
    /// </summary>
    public static class ScanActivity
    {
        public static event Action<string> ObjectChanged;

        /// <summary>Whether there are subscribers; avoids building strings in vain.</summary>
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
