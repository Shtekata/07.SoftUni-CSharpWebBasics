using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Test3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var httpClient = new HttpClient();
                var httpResponse = await httpClient.GetAsync("https://softuni.bg");
                var result = await httpResponse.Content.ReadAsStringAsync();
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            Console.WriteLine("gjhgkjgkghghkjgjhk");
        }
    }
}
