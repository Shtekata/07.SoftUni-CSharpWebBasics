using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Test2
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = Task<string>.Run(() => { return "asdaasdss"; });
            Console.WriteLine(a.Result);

            var httpClient = new HttpClient();
            httpClient.GetAsync("https://softuni.bg")
                .ContinueWith((x) =>
                {
                    x.Result.Content.ReadAsStringAsync()
                    .ContinueWith((y) =>
                    {
                        if (!y.IsFaulted)
                        {
                            Console.WriteLine(y.Result);
                        }
                    });
                });
            Console.WriteLine("gjhghjghj");
            Console.ReadLine();

            Task.Run(() => { while (true) { } });
            Task.Run(() => { while (true) { } });
            Task.Run(() => { while (true) { } });

            for (int i = 0; i < 1000; i++)
            {
                Task.Run(() => Thread.Sleep(10000));
            }
            Console.WriteLine('a');

            //Task.Run(() =>
            //{
            //    for (int i = 0; i < 1000; i++)
            //    {
            //        Console.WriteLine(i);
            //    }
            //}).ContinueWith((x) =>
            //{
            //    for (int i = 2000; i < 3000; i++)
            //    {
            //        Console.WriteLine(i);
            //    }
            //});

            //Task.Run(() =>
            //{
            //    for (int i = 1000; i < 2000; i++)
            //    {
            //        Console.WriteLine(i);
            //    }
            //});

            while (true)
            {
                var line = Console.ReadLine();
                Console.WriteLine(line.ToUpper());
            }
        }
    }
}
