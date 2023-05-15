
using Newtonsoft.Json;
using System;

namespace ConsoleApp1
{
    internal class Register
    {
        public static string appid = "46194da9303d3cc6a6a8ed33219cb0c8";
        //判断是否注册
        public static string isregister(string phone)
        {
            string currentTimeMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString(); ;
            string signature = "0cc8438f6b9f489eb8bdd1063c3577211.9.3"+ phone + currentTimeMillis+ "224e839335d1408a9a2c8d8e2cf2c4e8"+appid+"android";
            signature=myUtil.getMD5(signature);
            Ajax ajax1 = new Ajax();
            ajax1.Method = "post";
            ajax1.Url = "https://jiyunlincheng.hebyun.com.cn/memberapi/api/member/isRegister";
            ajax1.Body = "phone="+phone+"&currentTimeMillis="+ currentTimeMillis + "&appId="+appid+"&siteId=0cc8438f6b9f489eb8bdd1063c357721&versionName=1.9.3&platform=android&signature="+ signature;
            string result = ajax1.MakeRequest();
            //Console.WriteLine(result);
            return result;
        }
        //发送验证码
        public static string sendCode(string phone)
        {
            string currentTimeMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString(); ;
            string signature = "0cc8438f6b9f489eb8bdd1063c3577211.9.3"+ phone + currentTimeMillis +"224e839335d1408a9a2c8d8e2cf2c4e8"+appid+"android";
            signature = myUtil.getMD5(signature);
            Ajax ajax1 = new Ajax();
            ajax1.Method = "post";
            ajax1.Url = "https://jiyunlincheng.hebyun.com.cn/memberapi/api/member/sendAuthCode";
            ajax1.Body = "phone="+phone+"&currentTimeMillis="+ currentTimeMillis + "&appId="+appid+"&siteId=0cc8438f6b9f489eb8bdd1063c357721&versionName=1.9.3&platform=android&signature="+ signature;
            string result = ajax1.MakeRequest();
            dynamic json1 = JsonConvert.DeserializeObject(result);
            return json1.status;//验证码发送成功，请注意查收！
        }
        //注册
        public static string doMRegis(string phone,string nonce,int a)
        {
            string currentTimeMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            string signature = "";
            if (a==1)  signature = "0cc8438f6b9f489eb8bdd1063c3577211.9.3" + phone + currentTimeMillis + "224e839335d1408a9a2c8d8e2cf2c4e8" + nonce +appid+ "Androidb2e311cddac73bb6ab39ea42be49518a";
           else if (a == 2) signature = "0cc8438f6b9f489eb8bdd1063c3577211.9.3" + phone + currentTimeMillis + "224e839335d1408a9a2c8d8e2cf2c4e8" + appid+nonce + "Androidb2e311cddac73bb6ab39ea42be49518a";
            else if (a == 0) signature = "0cc8438f6b9f489eb8bdd1063c3577211.9.3" + nonce + phone + currentTimeMillis + "224e839335d1408a9a2c8d8e2cf2c4e8"+appid+"Androidb2e311cddac73bb6ab39ea42be49518a";
            else if (a == 3) signature = "0cc8438f6b9f489eb8bdd1063c3577211.9.3" + phone+ currentTimeMillis+ nonce+ "224e839335d1408a9a2c8d8e2cf2c4e8"+appid+"Androidb2e311cddac73bb6ab39ea42be49518a";

            signature = myUtil.getMD5(signature);
            Ajax ajax1 = new Ajax();
            ajax1.Method = "post";
            ajax1.Url = "https://jiyunlincheng.hebyun.com.cn/memberapi/api/member/doMRegis";
            ajax1.Body = "password=b2e311cddac73bb6ab39ea42be49518a&phone="+ phone + "&currentTimeMillis="+ currentTimeMillis + "&appId="+appid+"&siteId=0cc8438f6b9f489eb8bdd1063c357721&versionName=1.9.3&nonce="+ nonce + "&platform=Android&signature="+ signature;
            string result = ajax1.MakeRequest();
            return result;
            //dynamic json1 = JsonConvert.DeserializeObject(result);
            //return json1.status;//验证码结果
        }

        //登录
        public static string dologin(string phone)
        {
            string currentTimeMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString(); ;
            string signature = "0cc8438f6b9f489eb8bdd1063c3577211.9.3"+ phone+currentTimeMillis+"2218b94a5c65730d7871874a054ea498224e839335d1408a9a2c8d8e2cf2c4e8"+appid+"Androidb2e311cddac73bb6ab39ea42be49518a";
            signature = myUtil.getMD5(signature);
            Ajax ajax1 = new Ajax();
            ajax1.Method = "post";
            ajax1.Url = "https://jiyunlincheng.hebyun.com.cn/memberapi/api/member/doMLogin";
            ajax1.Body = "password=b2e311cddac73bb6ab39ea42be49518a&phone="+ phone + "&currentTimeMillis="+ currentTimeMillis + "&appId="+appid+"&siteId=0cc8438f6b9f489eb8bdd1063c357721&versionName=1.9.3&platform=Android&deviceToken=2218b94a5c65730d7871874a054ea498&signature="+ signature;
            string result = ajax1.MakeRequest();
           dynamic json1 = JsonConvert.DeserializeObject(result);
           return json1.id;//
        }

        //填邀请码
        public static string setInviteCode(string userId,int a)
        {
            string url = "https://jiyunlincheng.hebyun.com.cn/memberapi/api/member/setInviteCode";
            string currentTimeMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            string inviteCode = "KQUBCFLC";
            string siteId = "0cc8438f6b9f489eb8bdd1063c357721";
            string versionName = "1.9.3";
            string platform = "android";
            string signature = "7125cf06477c6efa0e8c994a17e48ac5";
            signature = siteId + versionName + currentTimeMillis + "224e839335d1408a9a2c8d8e2cf2c4e8" + appid + inviteCode + platform + userId;
            if (a == 1) signature = "0cc8438f6b9f489eb8bdd1063c3577211.9.3"+ currentTimeMillis+ "224e839335d1408a9a2c8d8e2cf2c4e8"+appid+userId+ "KQUBCFLCandroid";
            else if (a == 2) signature = "0cc8438f6b9f489eb8bdd1063c3577211.9.3" + currentTimeMillis + "224e839335d1408a9a2c8d8e2cf2c4e8" + appid + userId + "KQUBCFLCandroid";
            else if(a == 3) signature = "0cc8438f6b9f489eb8bdd1063c3577211.9.3" + currentTimeMillis + "224e839335d1408a9a2c8d8e2cf2c4e8" + userId  + appid + "KQUBCFLCandroid";
            else if(a == 4) signature = "0cc8438f6b9f489eb8bdd1063c3577211.9.3" + currentTimeMillis + "224e839335d1408a9a2c8d8e2cf2c4e8"+ appid  + "KQUBCFLC"  + userId + "android";
            else  if (a == 5) signature = "0cc8438f6b9f489eb8bdd1063c357721"+ userId+"1.9.3" + currentTimeMillis + "224e839335d1408a9a2c8d8e2cf2c4e8" + appid + "KQUBCFLC"  + "android";
            else if(a == 6) signature = "0cc8438f6b9f489eb8bdd1063c3577211.9.3" + currentTimeMillis + userId + "224e839335d1408a9a2c8d8e2cf2c4e8" + appid + "KQUBCFLCandroid";
            else if(a == 7) signature = "0cc8438f6b9f489eb8bdd1063c3577211.9.3" + userId  + currentTimeMillis + "224e839335d1408a9a2c8d8e2cf2c4e8" + appid + "KQUBCFLCandroid";
            else if (a == 8) signature = userId + "0cc8438f6b9f489eb8bdd1063c3577211.9.3" + currentTimeMillis + "224e839335d1408a9a2c8d8e2cf2c4e8" + appid + "KQUBCFLCandroid";
            signature = myUtil.getMD5(signature);
            Ajax ajax1 = new Ajax();
            ajax1.Url = url;
            ajax1.Method = "POST";
            ajax1.Body = "currentTimeMillis=" + currentTimeMillis + "&appId="+appid+"&inviteCode=" + inviteCode + "&siteId=0cc8438f6b9f489eb8bdd1063c357721&versionName=1.9.3&userId=" + userId + "&platform=android&signature=" + signature;
            string result= ajax1.MakeRequest();
            return result;
        }

    }
}
