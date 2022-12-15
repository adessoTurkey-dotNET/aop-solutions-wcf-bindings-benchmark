using System;
using System.ServiceModel;
using WCF.Service;

namespace WCF.Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(FibonacciService));
            try
            {
                host.AddServiceEndpoint(typeof(IFibonacciService), new WSHttpBinding(), Constants.WSHttpBindingEndpoint);
                host.AddServiceEndpoint(typeof(IFibonacciService), new NetTcpBinding(), Constants.NetTcpBindingEndpoint);
                host.AddServiceEndpoint(typeof(IFibonacciService), new NetNamedPipeBinding(), Constants.NetNamedPipeBindingEndpoint);
                host.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine(e.InnerException.Message);
                }
            }
            finally
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                host.Close();
            }
        }
    }
}
