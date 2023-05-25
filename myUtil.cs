using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1
{
    internal class myUtil
    {
        //随机获取手机号
        public static string GenerateRandomPhoneNumber()
        {
            Random random = new Random();
            // 中国手机号的前三位数字
            string[] prefixes = { "134", "135", "136", "137", "138", "139", "150", "151", "152", "157", "158", "159", "182", "183", "184", "187", "188", "147", "178", "170", "171", "173", "175", "176", "198", "199" };
            // 随机选择一个前缀
            string prefix = prefixes[random.Next(prefixes.Length)];
            // 生成剩下的 8 位数字
            string suffix = random.Next(10000000, 99999999).ToString();
            // 拼接手机号
            string phoneNumber = prefix + suffix;
            return phoneNumber;
        }
        //MD5
        public static string getMD5(string  str)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(str);
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        //随机长度32字符串
       public static string get32str()
        {
            byte[] randomBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            string randomString = BitConverter.ToString(randomBytes).Replace("-", "").ToLower();
            //Console.WriteLine(randomString);
            return randomString;
        }

        //四位数字,验证码库
        public static string[] AllCode()
        {
            List<string> codes = new List<string>();
            GenerateCodes("", "0123456789", 4, codes);
            return codes.ToArray();
        }

        private static void GenerateCodes(string prefix, string suffix, int length, List<string> codes)
        {
            if (prefix.Length == length)
            {
                codes.Add(prefix);
            }
            else
            {
                for (int i = 0; i < suffix.Length; i++)
                {
                    char c = suffix[i];
                    string newPrefix = prefix + c;
                    string newSuffix = suffix.Remove(i, 1);
                    GenerateCodes(newPrefix, newSuffix, length, codes);
                }
            }
        }


    }
}
