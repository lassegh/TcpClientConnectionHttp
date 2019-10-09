using System;
using System.Threading;

namespace TcpClientConnectionHttp
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            Car car = new Car()
            {
                Model = "A5",
                Vendor = "Audi",
                Price = 800000
            };

            // Post
            Console.WriteLine(client.HttpPostRequestAsync(car));
            Console.ReadKey();

            // GetAll
            Console.WriteLine(client.HttpGetRequestAsync());
            Console.ReadKey();
        }
    }
}
