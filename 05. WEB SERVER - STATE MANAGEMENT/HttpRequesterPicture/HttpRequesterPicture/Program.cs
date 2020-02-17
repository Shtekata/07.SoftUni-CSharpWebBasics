using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HttpRequester
{
    class Program
    {
        static Dictionary<string, int> SessionStore = new Dictionary<string, int>();
        static async Task Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 80);
            tcpListener.Start();
            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                Task.Run(() => ProcessClientAsync(tcpClient));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
        }

        private static async Task ProcessClientAsync(TcpClient tcpClient)
        {
            const string NewLine = "\r\n";
            using NetworkStream networkStream = tcpClient.GetStream();
            byte[] requestBytes = new byte[1000000]; // TODO: Use buffer
            int bytesRead = await networkStream.ReadAsync(requestBytes, 0, requestBytes.Length);
            string request = Encoding.UTF8.GetString(requestBytes, 0, bytesRead);
            byte[] fileContent = File.ReadAllBytes("istock-1069317442.jpg");
            string headers = "HTTP/1.0 200 Asen" + NewLine +
                              "Server: SoftUniServer/116.0" + NewLine +
                              "Content-Type: image/jpeg" + NewLine +
                              "Set-Cookie: user=Asen; Max-Age=3600; HttpOnly" + NewLine +
                              "Set-Cookie: lang=bg; Path=/lang" + NewLine +
                              "Content-Lenght: " + fileContent.Length + NewLine +
                              NewLine;
            byte[] headersBytes = Encoding.UTF8.GetBytes(headers);
            await networkStream.WriteAsync(headersBytes);
            await networkStream.WriteAsync(fileContent);
            Console.WriteLine(request);
            Console.WriteLine(headers);
            Console.WriteLine(new string('=', 60));
        }
    }
}
