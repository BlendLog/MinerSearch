using System.Net.NetworkInformation;

namespace netlib
{
    public static class Internet
    {
        public static bool IsOK(string host)
        {
            Ping pingQuery = new Ping();
            try
            {
                return (pingQuery.Send(host, 2000).Status == IPStatus.Success) == true;
            }
            catch
            {
                return false;
            }
            finally
            {
                pingQuery.Dispose();
            }
        }
    }
}
