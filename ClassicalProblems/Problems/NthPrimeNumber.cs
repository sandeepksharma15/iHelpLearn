using BenchmarkDotNet.Attributes;

namespace ClassicalProblems.Problems;

[MemoryDiagnoser]
[RankColumn]
public class NthPrimeNumber
{
    [Benchmark]
    [Arguments(524_287)]
    public static bool IsPrime_3(long n)
    {
        for (int i = 2; i <= Math.Sqrt(n); i++)
            if (n % i == 0)
                return false;

        return true;
    }
}
