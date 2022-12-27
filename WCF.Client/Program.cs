using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using System;
using System.ServiceModel;
using WCF.Service;

namespace WCF.Client
{
    [SimpleJob(RunStrategy.Monitoring, iterationCount: 100)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn, IterationsColumn]
    public class Program
    {
        [Params(34)]
        public static int n;

        [Params(true, false)]
        public static bool optimized;

        public static FibonacciServiceClient httpClient = new FibonacciServiceClient(new WSHttpBinding(), new EndpointAddress(Constants.WSHttpBindingEndpoint));
        public static FibonacciServiceClient tcpClient = new FibonacciServiceClient(new NetTcpBinding(), new EndpointAddress(Constants.NetTcpBindingEndpoint));
        public static FibonacciServiceClient pipeClient = new FibonacciServiceClient(new NetNamedPipeBinding(), new EndpointAddress(Constants.NetNamedPipeBindingEndpoint));

        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Program>();

            httpClient.Close();
            tcpClient.Close();
            pipeClient.Close();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        [Benchmark]
        public void WSHttpBinding()
        {
            httpClient.Calculate(n, optimized);
        }

        [Benchmark]
        public void NetTcpBinding()
        {
            tcpClient.Calculate(n, optimized);
        }

        [Benchmark]
        public void NetNamedPipeBinding()
        {
            pipeClient.Calculate(n, optimized);
        }
    }
}
