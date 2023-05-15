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

       // public string cookieString { get; set; }
        public string Body { get; set; }

        public string MakeRequest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = Method;
            request.ContentType = ContentType;
            //request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.64";


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

                string result;
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    result = reader.ReadToEnd();
                }
                return result;
            }
            catch (Exception e1)
            {
                Console.WriteLine("出现一个网络错误。");
                return "";
            }
            
        }

    }
}