using System.ServiceModel;
using System.ServiceModel.Channels;
using WCF.Service;

namespace WCF.Client
{
    public class FibonacciServiceClient : ClientBase<IFibonacciService>, IFibonacciService
    {
        public FibonacciServiceClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
        {
        }

        public ulong Calculate(int n, bool optimized)
        {
            return Channel.Calculate(n, optimized);
        }
    }
}
