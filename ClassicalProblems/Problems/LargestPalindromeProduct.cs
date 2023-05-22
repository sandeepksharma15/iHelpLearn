using BenchmarkDotNet.Attributes;

namespace ClassicalProblems.Problems;

[MemoryDiagnoser]
[RankColumn]
public class LargestPalindromeProduct
{
    [Benchmark]
    [Arguments(123_321)]
    public bool IsPalindrome_1(long n)
    {
        if (n < 0 || (n % 10 == 0 && n != 0))
        {
            return false;
        }

        long numberCopy = n;
        long reverseCopy = 0;

        while (numberCopy != 0)
        {
            reverseCopy = (reverseCopy * 10) + (numberCopy % 10);
            numberCopy /= 10;
        }

        return n == reverseCopy;
    }

    [Benchmark]
    [Arguments(123_321)]
    public bool IsPalindrome_2(long n)
    {
        if (n < 0 || (n % 10 == 0 && n != 0))
        {
            return false;
        }

        string strNumber = n.ToString();
        char[] strArray = strNumber.ToCharArray();
        Array.Reverse(strArray);
        string strReverse = new(strArray);

        return strNumber == strReverse;
    }

    [Benchmark]
    [Arguments(123_321)]
    public bool IsPalindrome_3(long n)
    {
        if (n < 0 || (n % 10 == 0 && n != 0))
        {
            return false;
        }

        long numberCopy = n;
        long reverseCopy = 0;

        while (numberCopy > reverseCopy)
        {
            reverseCopy = (reverseCopy * 10) + (numberCopy % 10);
            numberCopy /= 10;
        }

        return numberCopy == reverseCopy || numberCopy == reverseCopy / 10;
    }

    [Benchmark]
    [Arguments(123_321)]
    public bool IsPalindrome_4(long n)
    {
        if (n < 0 || (n % 10 == 0 && n != 0))
        {
            return false;
        }

        long numberCopy = n;
        long divisor = 1;

        while (numberCopy / divisor >= 10)
            divisor *= 10;

        while (numberCopy != 0)
        {
            if (numberCopy % divisor != numberCopy % 10)
                return false;

            // Remove Leading and Trailing Digit
            numberCopy = (numberCopy % divisor) / 10;

            // Update Divisor
            divisor /= 100;
        }

        return true;
    }

    [Benchmark]
    public void Largest_Palindrome_Product_1()
    {
        int largestPalindromeProduct = 0;

        for (int i = 100; i < 1000; i++)
            for (int j = 100; j < 1000; j++)
            {
                int candidate = i * j;

                if (IsPalindrome_3(candidate))
                    if (candidate > largestPalindromeProduct)
                        largestPalindromeProduct = candidate;
            }

        Console.WriteLine($" Largest Palindrome Is {largestPalindromeProduct}");
    }
}
