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
            //var sessionStore = new Dictionary<string, int>();
            var sid = Regex.Match(request, @"sid=[^\n ]*;").Value?.Replace("sid=", string.Empty).Replace(";", string.Empty);
            var newSid = Guid.NewGuid().ToString();
            var count = 0;
            if (SessionStore.ContainsKey(sid))
            {
                SessionStore[sid]++;
                count = SessionStore[sid];
            }
            else
            {
                sid = null;
                SessionStore[newSid] = 1;
                count = 1;
            }
            var cookie = Regex.Match(request, @"Cookie:[^\n]*\n").Value;
            //string responseText = "<h1>HELLO</h1>";
            string responseText = "<h1>" + count + "</h1>" +
                "<h1>" + sid + "</h1>" +
@"<h1>HELLO</h1>
<form action='/Account/Login' method='post'>
<input type=date name='date' />
<input type=text name='username' />
<input type=password name='password' />
<input type=submit value='Login' /></form>" +
"<h2>" + cookie + "</h2><h1>" + DateTime.UtcNow + "</h1>";
            string response = "HTTP/1.0 200 Asen" + NewLine +
                              "Server: SoftUniServer/116.0" + NewLine +
                              "Content-Type: text/html" + NewLine +
                              (string.IsNullOrWhiteSpace(sid) ? ("Set-Cookie: sid=" + newSid + NewLine) : string.Empty) +
                              //"Set-Cookie: user=Asen; Expires=" + new DateTime(2021, 1, 1).ToString("R") + NewLine +
                              "Set-Cookie: user=Asen; Max-Age=3600; HttpOnly" + NewLine +
                              "Set-Cookie: lang=bg; Path=/lang" + NewLine +
                              //"Location: https://google.com" + NewLine +
                              //"Content-Disposition: attachment; filename=Asen.html" + NewLine +
                              "Content-Lenght: " + responseText.Length + NewLine +
                              NewLine +
                              responseText;
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
            Console.WriteLine(request);
            Console.WriteLine(response);
            Console.WriteLine(new string('=', 60));
        }

        //public static async Task HttpRequest()
        //{
        //    var client = new HttpClient();
        //    HttpResponseMessage response = await client.GetAsync("https://softuni.bg/");
        //    string result = await response.Content.ReadAsStringAsync();
        //    //Console.WriteLine(result);
        //    File.WriteAllText("index.html", result);
        //}
    }
}
