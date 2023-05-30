using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class RegService
    {
        private static int a = 1;
        private static int b = 1;
        private static int c = 1;
        private static int d = 1;
        private static int e = 1;
        private static int f= 1;
        private static int g = 1;
        private static int h = 1;

        //判断是否注册，同时尝试递归算法获取使用能解密的设备id
        public static string isregister(string phone)
        {
            string result = Register.isregister(phone, a);
            while (!result.Contains("200"))
            {
                a++;
                result = Register.isregister(phone, a);
                if (a >2)
                {
                    a = 1;
                    Register.appid = myUtil.get32str();//换个设备id重试
                    isregister(phone);//递归算法
                    break;
                } 
            }
            dynamic registerjson = JsonConvert.DeserializeObject(result);
            return registerjson.isRegister;
        }

        //发送验证码
        public static string sendCode(string phone)
        {
            string result = Register.sendCode(phone, b);
            while (result.Contains("409"))
            {
                b++;
                result = Register.sendCode(phone, b);
                if (b > 2)
                {
                    b = 1;
                    break;
                }
            }
            while (result.Contains("发送失败"))
            {
                c++;
                sendCode(phone);
                if (c > 30)
                {
                    c = 1;
                    break;
                }
            }
            Console.WriteLine(result);
            return result;//包含200表示成功，包含409表示解密失败，包含1分钟内重复表示需等待,包含发送失败表示500错误
        }

        //输入验证码，尝试注册
        public static string doMRegis(string phone, string nonce)
        {
            string result = Register.doMRegis(phone, nonce, d);
            while (result.Contains("409"))
            {
                d++;
                result = Register.doMRegis(phone, nonce, d);
                if (d > 8)
                {
                    d = 1;
                    Console.WriteLine("本程序所有解密方法都无法解密本次注册操作");
                    break;
                }
            }
            Console.WriteLine("尝试使用验证码"+ nonce + "："+result);
            return result;
        }

        //输入邀请码
        public static string setInviteCode(string userId)
        {
            string result = Register.setInviteCode(userId, e);
            while (!result.Contains("200"))
            {
                e++;
                result = Register.setInviteCode(userId, e);
                if (e > 9)
                {
                    e = 1;
                    Console.WriteLine("本程序所有解密方法都无法解密本次填写邀请码操作");
                    break;
                }
            }
            return result;
        }

    }
}
