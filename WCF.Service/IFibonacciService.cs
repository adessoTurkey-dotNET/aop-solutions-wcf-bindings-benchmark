using System.ServiceModel;

namespace WCF.Service
{
    [ServiceContract]
    public interface IFibonacciService
    {
        [OperationContract]
        ulong Calculate(int n, bool optimized);
    }
}
