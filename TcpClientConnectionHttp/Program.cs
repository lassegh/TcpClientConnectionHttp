using System;
using System.Threading;

namespace TcpClientConnectionHttp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Client client = new Client();
            Car car = new Car()
            {
                Model = "A5",
                Vendor = "Audi",
                Price = 800000
            };
            client.HttpPostRequestAsync(car);
            Thread.Sleep(10000);
            Console.WriteLine(client.HttpGetAllRequestAsync().Result);
            Console.ReadKey();
        }
    }
}
