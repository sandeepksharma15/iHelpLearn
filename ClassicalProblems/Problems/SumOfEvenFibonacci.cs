using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace ClassicalProblems.Problems;

[RankColumn]
[SimpleJob(RunStrategy.ColdStart, iterationCount: 10000)]
public class SumOfEvenFibonacci
{
    const long UpperLimit = 3_000_000;

    [Benchmark]
    public void GenerateFibonacci_1()
    {
        int f1 = 1;
        int f2 = 1;
        const int Terms = 10;

        for (int i = 0; i < Terms; i++)
        {
            Console.Write($"{f1} ");

            int f3 = f1 + f2;

            f1 = f2;
            f2 = f3;
        }
    }

    [Benchmark]
    public void GenerateFibonacci_2()
    {
        const int Limit = 10;

        // Returns The nth Fibonacci Term
        static int Fibo(int n)
        {
            if ((n == 0) || (n == 1))
                return 1;

            return Fibo(n - 1) + Fibo(n - 2);
        }

        for (int i = 0; i < Limit; i++)
            Console.Write($"{Fibo(i)} ");
    }

    [Benchmark]
    public void GenerateFibonacci_3()
    {
        // We keep hold of earlier Fibonacci Terms
        static int Fibo(int n, List<int> fiboList)
        {
            if (n < fiboList.Count)
                return fiboList[n];

            fiboList.Add(fiboList[n - 1] + fiboList[n - 2]);

            return fiboList[n];
        }

        List<int> fiboList = new() { 1, 1 };
        const int Limit = 10;

        for (int i = 0; i < Limit; i++)
            Console.Write($"{Fibo(i, fiboList)} ");
    }

    [Benchmark]
    public void Sum_Of_Even_Fibonacci_1()
    {
        int f1 = 1;
        int f2 = 2;
        int sum = 0;
        int f3 = f1 + f2;

        while (f1 < UpperLimit)
        {
            if (f1 % 2 == 0)
                sum += f1;

            f1 = f2;
            f2 = f3;

            f3 = f1 + f2;
        }

        Console.WriteLine($"The sum of Even Fibonacci of up to {UpperLimit} is: {sum}");
    }

    [Benchmark]
    public void Sum_Of_Even_Fibonacci_2()
    {
        static IEnumerable<int> Fibonacci(long limit)
        {
            int f1 = 1;
            int f2 = 2;
            int f3 = f1 + f2;

            while (f1 < limit)
            {
                yield return f1;

                f1 = f2;
                f2 = f3;
                f3 = f1 + f2;
            }
        }

        int sum = 0;
        foreach (int f in Fibonacci(UpperLimit))
            if (f % 2 == 0)
                sum += f;

        //_ = Fibonacci(UpperLimit)
        //    .Where(x => x % 2 == 0)
        //    .Sum(x => sum += x);

        Console.WriteLine($"The sum of Even Fibonacci of up to {UpperLimit} is: {sum}");
    }

    [Benchmark]
    public void Sum_Of_Even_Fibonacci_3()
    {
        int f1 = 1;
        int f2 = 1;
        int f3 = f1 + f2;
        int sum = 0;
        int terms = 3;

        while (f3 < UpperLimit)
        {
            if (terms++ % 3 == 0)
                sum += f3;
            f1 = f2;
            f2 = f3;
            f3 = f1 + f2;
        }

        Console.WriteLine($"The sum of Even Fibonacci of up to {UpperLimit} is: {sum}");
    }

    [Benchmark]
    public void Sum_Of_Even_Fibonacci_4()
    {
        static int Fibonacci(int n)
        {
            if ((n == 1) || (n == 2))
                return 1;

            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        static int SumEvenFibonacci(long limit)
        {
            int sum = 0;
            int n = 1;
            int next;

            while ((next = Fibonacci(n)) < limit)
            {
                if (next % 2 == 0)
                    sum += next;

                n++;
            }

            return sum;
        }

        Console.WriteLine($"The sum of Even Fibonacci of up to {UpperLimit} is: {SumEvenFibonacci(UpperLimit)}");
    }

    [Benchmark]
    public void Sum_Of_Even_Fibonacci_5()
    {
        var fiboList = new List<int>();
        int nextFibo;

        fiboList.Add(1);
        fiboList.Add(1);

        while ((nextFibo = fiboList[^1] + fiboList[^2]) < UpperLimit)
            fiboList.Add(nextFibo);

        int sum = fiboList.Where(fibo => fibo % 2 == 0)
            .Sum();

        Console.WriteLine($"The sum up to {UpperLimit} is: {sum}");
    }

    [Benchmark]
    public void Sum_Of_Even_Fibonacci_6()
    {
        int f1 = 1;
        int f2 = 2;
        int f3 = f1 + f2;
        int sum = 0;

        while (f1 < UpperLimit)
        {
            sum += f1;
            f1 = f2;
            f2 = f3;
            f3 = f1 + f2;
        }

        Console.WriteLine($"The sum of Even Fibonacci of up to {UpperLimit} is: {(sum / 2) + 1}");
    }

    [Benchmark]
    public void Sum_Of_Even_Fibonacci_7()
    {
        static int EvenFibonacci(int n)
        {
            if (n < 1)
                return n;
            if (n == 1)
                return 2;

            return (4 * EvenFibonacci(n - 1)) + EvenFibonacci(n - 2);
        }

        static int SumEvenFibonacci(long limit)
        {
            int sum = 0;
            int n = 1;
            int next;

            while ((next = EvenFibonacci(n)) < limit)
            {
                sum += next;

                n++;
            }

            return sum;
        }

        Console.WriteLine($"The sum of Even Fibonacci of up to {UpperLimit} is: {SumEvenFibonacci(UpperLimit)}");
    }

    [Benchmark]
    public void Sum_Of_Even_Fibonacci_8()
    {
        var fiboList = new List<int>();
        int nextEvenFibo;

        fiboList.Add(0);
        fiboList.Add(2);

        while ((nextEvenFibo = (4 * fiboList[^1]) + fiboList[^2]) < UpperLimit)
            fiboList.Add(nextEvenFibo);

        Console.WriteLine($"The sum up to {UpperLimit} is: {fiboList.Sum()}");
    }
}
