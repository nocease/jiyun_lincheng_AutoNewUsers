using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;


//如需配置（邀请码、冀云平台），可打开Register.cs文件
namespace ConsoleApp1
{
    internal class Program
    {
        private static int n = 0;//记录成功数量
        static void Main(string[] args)
        {
            begJiYunLinCheng();//冀云临城注册邀请
            //for (int bb = 0; bb < 1000; bb++) newXT.infoUpd(); Console.Write("OK");//新邢台信息登记

            NoExit.exit1();
        }
        static void begJiYunLinCheng()
        {
            Console.WriteLine("**************************************************************");
            Console.WriteLine("*********************自动注册邀请冀云临城*********************");
            Console.WriteLine("*本程序仅供交流学习，下载后请在24小时内删除，不得用于非法用途*");
            Console.WriteLine("*************使用本程序造成的后果与开发者无关*****************");
            Console.WriteLine("**************************************************************");
            Console.Write("请输入手机号，多个请用英文逗号隔开：");
            Stream steam = Console.OpenStandardInput();
            Console.SetIn(new StreamReader(steam, Encoding.Default, false, 5000));
            string phones1 = Console.ReadLine();
            string[] phones2 = phones1.Split(',');
            //开始循环手机号
            foreach (string phone in phones2)
            {
                Register.appid = myUtil.get32str();//使用随机设备id
                do1(phone);
            }
            Console.WriteLine("**************************************************************");
            Console.WriteLine("程序运行结束。成功邀请了" + n + "个新用户。");
            Console.WriteLine("**************************************************************");
        }
        static void do1(string phone)
        {
            if (RegService.isregister(phone) == "0")//此时该手机号未被注册，可用
            {
                string sendCodeResult = RegService.sendCode(phone);
                if (sendCodeResult.Contains("200"))
                {
                    int[] codes = Enumerable.Range(4000, 9000).ToArray();//设置验证码的范围，4位数字，从4000开始按顺序尝试
                    foreach (int code in codes)
                    {
                        string doMRegisResult = RegService.doMRegis(phone, code.ToString());
                        if (doMRegisResult.Contains("失效"))
                        {
                            //重新发送验证码
                            Console.WriteLine("当前已成功邀请了" + n + "个新用户。");
                            do1(phone);
                        }
                        else if (doMRegisResult.Contains("409"))
                        {
                            //换下一个手机号
                            Console.WriteLine("当前已成功邀请了" + n + "个新用户。");
                            break;
                        }
                        else if (doMRegisResult.Contains("200"))
                        {
                            //注册成功
                            Console.WriteLine("已使用预设密码注册成功。");
                            string userid = Register.dologin(phone);//尝试登陆该账户
                            Console.WriteLine("已成功登录该账号！" + phone + ":" + userid);
                            //开始尝试填写邀请码
                            if (RegService.setInviteCode(userid).Contains("200"))
                            {
                                n++;
                            }
                            Console.WriteLine("当前已成功邀请了" + n + "个新用户。");
                            break;//结束循环
                        }

                    }
                } else return;
            }
            else //手机号已被注册
            {
                Console.WriteLine("该手机号已被注册。");
                Console.WriteLine("当前已成功邀请了" + n + "个新用户。");
            }
        }

    }
}
