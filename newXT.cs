using System;
using System.IO;
using System.Net;
using System.Text;

namespace ConsoleApp1
{
    internal class newXT
    {
        //模拟<新邢台>信息登记
        public static void infoUpd()
        {
            // 请求URL
            string url = "https://appapi.hebei.com.cn/api/newxtappxxdj/api/receive.php";
            // 请求内容
            string message = "{\"appip\":\"127.0.0.1\",\"username\":\"王六\",\"ssdq\":\"邢台\",\"county\":\"临城县\",\"company\":\"赵庄乡\",\"nickname\":\"" + myUtil.get32str() + "\",\"phone\":\""+ myUtil.GenerateRandomPhoneNumber() + "\",\"imei\":\""+ myUtil.get32str() + " \"}";
            /* string openid = myUtil.get32str();
            string timesTamp= DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            string sign =myUtil.getMD5(openid+"5fc57e4d03c3ff0fd0bf19cd48eeb7de" + timesTamp);
            string userid= myUtil.get32str();*/

            // 自定义 User-Agent
            //string userAgent = "Mozilla/5.0 (Linux; Android 13; 2203121C Build/TKQ1.220829.002; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/113.0.5672.131 Mobile Safari/537.36 pdmiryun appId/46194da9303d3cc6a6a8ed33219cb0c8 userId/"+userid+" currentSiteId/f6d9609c96ed4e57aa3cffde7a26eef1 timesTamp/"+ timesTamp + " openId/"+openid+" sign/"+sign+"phone/MTU3MTMxOTYwNzc=";
            string userAgent = "";

           // 创建 WebRequest 对象
           HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            // 设置请求头
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = userAgent;

            // 将请求内容转换为字节数组
            byte[] postDataBytes = Encoding.UTF8.GetBytes("message=" + Uri.EscapeDataString(message));

            // 设置请求体长度
            request.ContentLength = postDataBytes.Length;

            // 获取请求流
            using (Stream requestStream = request.GetRequestStream())
            {
                // 将请求内容写入请求流
                requestStream.Write(postDataBytes, 0, postDataBytes.Length);
            }

            // 发送请求并获取响应
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                // 读取响应内容
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string responseText = reader.ReadToEnd();
                    Console.WriteLine(responseText);
                }
            }
        }
    }
}
