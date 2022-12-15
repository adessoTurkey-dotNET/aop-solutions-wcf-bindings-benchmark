namespace WCF.Service
{
    public static class Constants
    {
        public static string WSHttpBindingEndpoint = "http://localhost:10000/FibonacciHttpService";
        public static string NetTcpBindingEndpoint = "net.tcp://localhost:10001/FibonacciTcpService";
        public static string NetNamedPipeBindingEndpoint = "net.pipe://localhost/FibonacciMsmqService";
    }
}
