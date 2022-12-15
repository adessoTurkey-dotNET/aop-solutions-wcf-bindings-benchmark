namespace AOP.Services.Interfaces;

public interface IFibonacciService
{
    ulong CalculateWithoutCache(int n);
    ulong CalculateWithCache(int n);
}
