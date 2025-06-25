using System.Net;

namespace netlib
{
    public static class Internet
    {
        public static bool IsOK()
        {
            try
            {
                Dns.GetHostEntry("msch3295connect.ru");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
