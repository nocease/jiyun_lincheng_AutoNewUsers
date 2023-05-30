using System.IO;
using System.Net;
using System.Text;
using System;

namespace ConsoleApp1
{
    public class Ajax
    {
        public string Url { get; set; }
        public string Method { get; set; } = "GET";
        public string ContentType { get; set; } = "application/x-www-form-urlencoded";
        public string Body { get; set; }

        public string MakeRequest()
        {
            int retryCount = 10;
            string result = "";

            while (retryCount > 0)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = Method;
                request.ContentType = ContentType;
                //request.UserAgent = "okhttp/3.12.12";

                if (!string.IsNullOrEmpty(Body))
                {
                    byte[] data = Encoding.UTF8.GetBytes(Body);
                    request.ContentLength = data.Length;
                    Stream stream = request.GetRequestStream();
                    stream.Write(data, 0, data.Length);
                }

                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                        result = reader.ReadToEnd();
                    }

                    if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        //Console.WriteLine("服务器返回了500错误。正在尝试重新发送请求。");
                        retryCount--;
                    }
                    else
                    {
                        return result;
                    }
                }
                catch (Exception e1)
                {
                   Console.WriteLine("出现一个请求错误。");
                    retryCount--;
                }
            }

            //Console.WriteLine("已达到重试次数上限。");
            return result;
        }
    }
}