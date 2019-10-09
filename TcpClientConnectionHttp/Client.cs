using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TcpClientConnectionHttp
{
    public class Client
    {
        

        public string HttpPostRequestAsync(Car car)
        {
            string response;

            string body = JsonConvert.SerializeObject(car);

            using (TcpClient tcpClient = new TcpClient())
            {
                tcpClient.Connect("lgwebservice.azurewebsites.net", 80);
                using (NetworkStream networkStream = tcpClient.GetStream())
                {
                    using (StreamWriter streamWriter = new StreamWriter(networkStream))
                    using (StreamReader sr = new StreamReader(networkStream))
                    {
                        // request line
                        streamWriter.Write("POST /api/cars/ HTTP/1.1\r\n");

                        // headers
                        streamWriter.Write("Host: lgwebservice.azurewebsites.net\r\n");
                        streamWriter.Write("Content-Type: application/json\r\n");
                        streamWriter.Write("Content-Length: " + body.Length + "\r\n");
                        streamWriter.Write("\r\n");

                        // body
                        streamWriter.Write(body + "\r\n");
                        streamWriter.Flush();

                        // HTTP Response ?
                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());







                        response = sb.ToString();
                    }
                }
            }

            return response;
        }

        public string HttpGetRequestAsync()
        {
            string response;

            using (TcpClient tcpClient = new TcpClient())
            {
                tcpClient.Connect("lgwebservice.azurewebsites.net", 80);
                using (NetworkStream networkStream = tcpClient.GetStream())
                {
                    using (StreamWriter streamWriter = new StreamWriter(networkStream))
                    using (StreamReader sr = new StreamReader(networkStream))
                    {
                        // request line
                        streamWriter.Write("Get /api/cars/ HTTP/1.1\r\n");

                        // headers
                        streamWriter.Write("Host: lgwebservice.azurewebsites.net\r\n");
                        streamWriter.Write("Content-Type: application/json\r\n");
                        streamWriter.Write("\r\n");

                        streamWriter.Flush();

                        // HTTP Response ?
                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());
                        sb.AppendLine(sr.ReadLine());

                        response = sb.ToString();
                    }
                }
            }

            return response;
        }
    }
}
