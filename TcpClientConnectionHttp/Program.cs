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
            Console.WriteLine(client.HttpPostRequestAsync(car).Result);
            Console.WriteLine(client.HttpGetAllRequestAsync().Result);
            Console.ReadKey();
        }
    }
}
