using System;

namespace ConsoleApp1
{
    internal class NoExit
    {
       public static void exit1()
        {
            // 无限循环
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    // 如果有按键按下，则退出循环
                    Console.ReadKey(true);
                    break;
                }
            }
            // 等待用户按任意键继续
            Console.WriteLine("按任意键退出程序。。。");
             Console.ReadKey(true);
        }
    }
}
