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
        public async Task<string> HttpGetAllRequestAsync()
        {
            string result = string.Empty;

            using (var tcp = new TcpClient("lgwebservice.azurewebsites.net", 80))
            using (var stream = tcp.GetStream())
            {
                tcp.SendTimeout = 500;
                tcp.ReceiveTimeout = 1000;
                // Send request headers
                var builder = new StringBuilder();
                builder.AppendLine("GET /api/cars/ HTTP/1.1");
                builder.AppendLine("Host: lgwebservice.azurewebsites.net");
                //builder.AppendLine("Content-Length: " + data.Length);   // only for POST request
                builder.AppendLine("Connection: close");
                builder.AppendLine();
                var header = Encoding.ASCII.GetBytes(builder.ToString());
                await stream.WriteAsync(header, 0, header.Length);

                // receive data
                using (var memory = new MemoryStream())
                {
                    await stream.CopyToAsync(memory);
                    memory.Position = 0;
                    var data = memory.ToArray();

                    var index = BinaryMatch(data, Encoding.ASCII.GetBytes("\r\n\r\n")) + 4;
                    var headers = Encoding.ASCII.GetString(data, 0, index);
                    memory.Position = index;

                    if (headers.IndexOf("Content-Encoding: gzip") > 0)
                    {
                        using (GZipStream decompressionStream = new GZipStream(memory, CompressionMode.Decompress))
                        using (var decompressedMemory = new MemoryStream())
                        {
                            decompressionStream.CopyTo(decompressedMemory);
                            decompressedMemory.Position = 0;
                            result = Encoding.UTF8.GetString(decompressedMemory.ToArray());
                        }
                    }
                    else
                    {
                        result = Encoding.UTF8.GetString(data, index, data.Length - index);
                        //result = Encoding.GetEncoding("gbk").GetString(data, index, data.Length - index);
                    }
                }

                //Debug.WriteLine(result);
                return result;
            }
        }

        private static int BinaryMatch(byte[] input, byte[] pattern)
        {
            int sLen = input.Length - pattern.Length + 1;
            for (int i = 0; i < sLen; ++i)
            {
                bool match = true;
                for (int j = 0; j < pattern.Length; ++j)
                {
                    if (input[i + j] != pattern[j])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    return i;
                }
            }
            return -1;
        }

        public async Task<string> HttpPostRequestAsync(Car car)
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
                        response = sr.ReadLine();
                    }
                }
            }

            return response;
        }
    }
}
