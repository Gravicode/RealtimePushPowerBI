using System;
using System.Net;
using System.IO;

using System.Linq;
using System.Threading;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace PushDataApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RunLoop();
            Console.ReadLine();
        }

       static  async void RunLoop()
        {
            var client = new HttpClient();
            var url = "https://api.powerbi.com/beta/e4a5cd36-e58f-4f98-8a1a-7a8e545fc65a/datasets/79c65ce1-591e-459e-a69a-94885df30939/rows?key=6LZcZM3WUKEbvqb7rbdV2zipAsFIEdinI6NgGLXucJsonS%2BtmlfJIyYoar2S1rtRMKzyyCagiiEGxW%2FOdz3uxg%3D%3D";
            var Rnd = new Random(Environment.TickCount);
            while (true)
            {
                var data = new SensorData() { Temp = Rnd.Next(15, 35), Humid = Rnd.Next(40, 60), Light = Rnd.Next(1, 100), Time = DateTime.Now };
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var res = await client.PostAsync(url, content, CancellationToken.None);
                if (res.IsSuccessStatusCode)
                {
                    Console.WriteLine($"data sent to power bi - {DateTime.Now}");
                }
                Thread.Sleep(2000);
            }
        } 
     
    }
    
    public class SensorData
    {
        public float Temp { get; set; }
        public float Humid { get; set; }
        public float Light { get; set; }
        public DateTime Time { get; set; }
    }

}
