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
                // Retry 2 attempts with 5000ms timeout for unstable networks
                var result = pingQuery.Send(host, 5000);
                if (result.Status == IPStatus.Success)
                    return true;
                
                result = pingQuery.Send(host, 5000);
                return result.Status == IPStatus.Success;
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
