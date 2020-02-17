using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Test
{
    class Program
    {
        static void Exception()
        {
            try
            {
                throw new Exception("Exception handled!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void Main(string[] args)
        {
            var thread5 = new Thread(Exception);
            thread5.Start();

            var lockObj1 = new object();
            var lockObj2 = new object();

            var thread3 = new Thread(() =>
            {
                lock (lockObj1)
                {
                    Thread.Sleep(1000);
                    lock (lockObj2)
                    {

                    }
                }
            });
            var thread4 = new Thread(() =>
            {
                lock (lockObj2)
                {
                    Thread.Sleep(1000);
                    lock (lockObj1)
                    {

                    }
                }
            });

            thread3.Start();
            thread4.Start();
            //thread3.Join();
            //thread4.Join();

            var numbers = new ConcurrentQueue<int>(Enumerable.Range(0, 10000).ToList());
            for (int i = 0; i < 4; i++)
            {
                new Thread(() => { while (numbers.Count > 0) numbers.TryDequeue(out _); }).Start();
            }

            object lockObj = new object();
            decimal money = 0;
            ThreadStart incrementMyMoney = () =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    lock (lockObj)
                    {
                        money++;
                    }
                }
            };
            //var thread1 = new Thread(() =>
            //{
            //    for (int i = 0; i < 10000; i++)
            //    {
            //        lock (lockObj)
            //        {
            //            money++;
            //        }
            //    }
            //});
            var thread1 = new Thread(incrementMyMoney);
            thread1.Start();
            //var thread2 = new Thread(() =>
            //{
            //    for (int i = 0; i < 10000; i++)
            //    {
            //        lock (lockObj)
            //        {
            //            money++;
            //        }
            //    }
            //});
            var thread2 = new Thread(incrementMyMoney);
            thread2.Start();
            thread1.Join();
            thread2.Join();
            Console.WriteLine(money);
            //new Thread(() => { while (true) Console.WriteLine(1); }).Start();
            //new Thread(() => { while (true) Console.WriteLine(2); }).Start();

            Thread thread = new Thread(MyThreadMainMethod);
            thread.Start();

            while (true)
            {
                var line = Console.ReadLine();
                Console.WriteLine(line.ToUpper());
            }
        }

        private static void MyThreadMainMethod()
        {
            var sw = Stopwatch.StartNew();
            Console.WriteLine(CountPrimeNumbers(1, 1000000));
            Console.WriteLine(sw.Elapsed);
        }

        private static int CountPrimeNumbers(int from, int to)
        {
            int count = 0;
            for (int i = from; i <= to; i++)
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
                    count++;
                }
            }
            return count;
        }
    }
}
