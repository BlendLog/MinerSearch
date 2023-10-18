using System;
using System.Threading;

namespace rk_test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("nothing to see here");
            if (args.Length > 0)
            {
                Thread.Sleep(Convert.ToInt32(args[0]) * 1000);
            }
        }
    }
}
