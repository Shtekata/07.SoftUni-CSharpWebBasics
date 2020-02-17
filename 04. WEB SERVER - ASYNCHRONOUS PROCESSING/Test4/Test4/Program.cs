using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Test4
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = Stopwatch.StartNew();
            var count = 0;
            var a = new object();

            //for (int i = 1; i <= 1000000; i++)
            Parallel.For(1, 1000001, (i) =>
            {
                bool isPrime = true;
                for (int div = 2; div <= Math.Sqrt(i); div++)
                {
                    if (i % div == 0)
                    {
                        isPrime = false;
                    }
                }
                if (isPrime)
                {
                    lock (a)
                    {
                        count++;
                    }
                }
            });
            Console.WriteLine(count);
            Console.WriteLine(sw.Elapsed);
        }
    }
}
