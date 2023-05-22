using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace ClassicalProblems.Problems;

[MemoryDiagnoser]
[RankColumn]
[SimpleJob(RunStrategy.ColdStart, iterationCount: 1000)]
public class LargestPrimeFactor
{
    const long Number = 500_743_867_642;

    [Benchmark]
    [Arguments(524_287)]
    public bool IsPrime_1(long n)
    {
        for (int i = 2; i < n; i++)
            if (n % i == 0)
                return false;

        return true;
    }

    [Benchmark]
    [Arguments(524_287)]
    public bool IsPrime_2(long n)
    {
        for (int i = 2; i <= n / 2; i++)
            if (n % i == 0)
                return false;

        return true;
    }

    [Benchmark]
    [Arguments(524_287)]
    public bool IsPrime_3(long n)
    {
        for (int i = 2; i <= Math.Sqrt(n); i++)
            if (n % i == 0)
                return false;

        return true;
    }

    [Benchmark]
    [Arguments(524_287)]
    public bool IsPrime_4(long n)
    {
        int[] inc = new int[] { 4, 2, 4, 2, 4, 6, 2, 6 };

        if (n % 2 == 0) return false;
        if (n % 3 == 0) return false;
        if (n % 5 == 0) return false;

        int i = 0;

        for (int k = 7; k * k <= n; k += inc[i])
        {
            if (n % k == 0)
                return false;

            if (i < 7)
                i++;
            else
                i = 0;
        }

        return true;
    }

    [Benchmark]
    [Arguments(123_456_789)]
    public void Factors_1(long n)
    {
        for (long i = 2; i <= (n / 2); i++)
        {
            if (n % i == 0)
                Console.WriteLine($"{i} is a factor of {n}");
        }
    }

    [Benchmark]
    [Arguments(123_456_789)]
    public void Factors_2(long n)
    {
        long limit = (long)Math.Sqrt(n);

        for (long i = 2; i < limit; i++)
        {
            if (n % i == 0)
            {
                Console.WriteLine($"{i} is a factor of {n}");
                Console.WriteLine($"{n / i} is a factor of {n}");
            }
        }

        if (limit * limit == n)
            Console.WriteLine($"{limit} is a factor of {n}");
    }

    [Benchmark]
    [Arguments(123_456)]
    public void Prime_Factors_1(long n)
    {
        for (int i = 2; i <= (n / 2); i++)
        {
            if ((n % i == 0) && IsPrime_1(i))
                Console.WriteLine($"{i} is a Prime factor of {n}");
        }
    }

    [Benchmark]
    [Arguments(123_456)]
    public void Prime_Factors_2(long n)
    {
        if (n % 2 == 0)
            Console.WriteLine($"2 is a Prime factor of {n}");

        if (n % 3 == 0)
            Console.WriteLine($"3 is a Prime factor of {n}");

        for (int i = 1; (i * 6) <= (n / 2); i++)
        {
            // Potential Primes
            int p = (6 * i) - 1;
            int q = (6 * i) + 1;

            if ((n % p == 0) && (IsPrime_3(p)))
                Console.WriteLine($"{p} is a Prime factor of {n}");

            if ((n % q == 0) && (IsPrime_3(q)))
                Console.WriteLine($"{q} is a Prime factor of {n}");
        }
    }

    [Benchmark]
    [Arguments(123_456)]
    public void Prime_Factors_3(long n)
    {
        long factor = 2;
        long number = n;

        while (number > 1)
        {
            if (number % factor == 0)
            {
                Console.WriteLine($"{factor} is a Prime factor of {n}");

                while (number % factor == 0)
                    number /= factor;
            }

            factor++;
        }
    }

    [Benchmark]
    public void Largest_Prime_Factor_1()
    {
        static bool IsPrime(long n)
        {
            for (int i = 2; i <= Math.Sqrt(n); i++)
                if (n % i == 0)
                    return false;

            return true;
        }

        long largest = 1;

        for (long factor = 2; factor <= Math.Sqrt(Number); factor++)
            if ((Number % factor == 0) && IsPrime(factor))
                largest = factor;

        Console.WriteLine($"Largest Prime Factor Is {largest}");
    }

    [Benchmark]
    public void Largest_Prime_Factor_2()
    {
        long number = Number;
        long factor = 2;
        long largest = 1;

        while (number > 1)
        {
            if (number % factor == 0)
            {
                largest = factor;

                while (number % factor == 0)
                    number /= factor;
            }

            factor++;
        }

        Console.WriteLine($"Largest Prime Factor Is {largest}");
    }

    [Benchmark]
    public void Largest_Prime_Factor_3()
    {
        long number = Number;
        long factor = 2;
        long largest = 1;

        while (number > 1)
        {
            while (number % factor == 0)
            {
                largest = factor;
                number /= factor;
            }

            factor++;
        }

        Console.WriteLine($"Largest Prime Factor Is {largest}");
    }
}
