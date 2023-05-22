using BenchmarkDotNet.Attributes;

namespace ClassicalProblems.Problems;

[MemoryDiagnoser]
[RankColumn]
public class SmallestMultiple
{
    const int DivisorUpTo = 20;

    [Benchmark]
    public void Smallest_Multiple_1()
    {
        long smallestMultiple = DivisorUpTo;
        bool isFound = false;

        while (!isFound)
        {
            bool isMultiple = true;

            for (int i = 2; i <= DivisorUpTo; i++)
                if (smallestMultiple % i != 0)
                {
                    isMultiple = false;
                    break;
                }

            if (isMultiple)
                isFound = true;
            else
                smallestMultiple += DivisorUpTo;
        }

        Console.WriteLine($"The Largest common Multiple Of Numbers Up to {DivisorUpTo} Is {smallestMultiple}");
    }

    private static long GetGCD(long x, long y)
    {
        long r;

        while ((r = x % y) != 0)
        {
            x = y;
            y = r;
        }

        return y;
    }

    private static long GetLCM(long x, long y)
    {
        return (x * y) / GetGCD(x, y);
    }

    [Benchmark]
    public void Smallest_Multiple_2()
    {
        long smallestMultiple = 1;

        for (int i = 2; i <= DivisorUpTo; i++)
            smallestMultiple = GetLCM(smallestMultiple, i);

        Console.WriteLine($"The Largest common Multiple Of Numbers Up to {DivisorUpTo} Is {smallestMultiple}");
    }

    [Benchmark]
    public void Smallest_Multiple_3()
    {
        long smallestMultiple = 1;

        // LCM of a set of numbers is equal to the
        // product of the prime factors with their maximum powers
        List<int> primes = new();

        // First Get Prime Factors Up To N
        for (int i = 2; i <= DivisorUpTo; i++)
        {
            bool isPrime = true;
            for (int j = 2; (j <= Math.Sqrt(i)) && isPrime; j++)
                if (i % j == 0)
                    isPrime = false;

            if (isPrime)
                primes.Add(i);
        }

        // Find The Maximum Power Of Each Factor
        int[] powers = new int[primes.Count];

        for (int i = 1; i <= 20; i++)
        {
            int n = i;
            for (int j = 0; j < primes.Count; j++)
            {
                int count = 0;

                while (n % primes[j] == 0)
                {
                    count++;
                    n /= primes[j];
                }

                if (count > powers[j])
                {
                    powers[j] = count;
                }
            }
        }

        for (int i = 0; i < primes.Count; i++)
        {
            smallestMultiple *= (int)Math.Pow(primes[i], powers[i]);
        }

        Console.WriteLine($"The Largest common Multiple Of Numbers Up to {DivisorUpTo} Is {smallestMultiple}");
    }

    [Benchmark]
    public void Smallest_Multiple_4()
    {
        // First Get Prime Factors Up To N
        List<int> primes = new();
        for (int i = 2; i <= DivisorUpTo; i++)
        {
            bool isPrime = true;
            for (int j = 2; (j <= Math.Sqrt(i)) && isPrime; j++)
                if (i % j == 0)
                    isPrime = false;

            if (isPrime)
                primes.Add(i);
        }

        // Put All Numbers In The array
        int[] divisors = new int[DivisorUpTo + 1];
        for (int i = 2; i < divisors.Length; i++)
            divisors[i] = i;

        long smallestMultiple = 1;

        for (int i = 0; i < primes.Count; i++)
        {
            bool isDivided = true;

            while (isDivided)
            {
                isDivided = false;

                for (int j = 2; j < divisors.Length; j++)
                {
                    if ((divisors[j] > 1) && (divisors[j] % primes[i] == 0))
                    {
                        isDivided = true;
                        divisors[j] /= primes[i];
                    }
                }

                if (isDivided)
                    smallestMultiple *= primes[i];
            }
        }

        Console.WriteLine($"The Largest common Multiple Of Numbers Up to {DivisorUpTo} Is {smallestMultiple}");
    }
}
