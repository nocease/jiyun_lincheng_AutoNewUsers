using Newtonsoft.Json;
using System;

namespace ConsoleApp1
{
    internal class Register
    {
        //以下变量可自定义
        static string inviteCode = "KQUBCFLC";//使用的邀请码，可自定义
        static string siteId = "0cc8438f6b9f489eb8bdd1063c357721";//表示冀云平台，这个表示【冀云临城】，可自定义。软件下载页链接包含该id
        static string apiSign = "224e839335d1408a9a2c8d8e2cf2c4e8";//似乎和siteid同理
        static string password = "8da01aebe8fc44e081f2f4cde855a28e";//默认密码
        //设备id，使用随机设备id会导致加密的方式过多、解密时间过长
        static string appid = "46194da9303d3cc6a6a8ed33219cb0c8";//设备id
        static string deviceToken = "a275859425adce681dc5a504062c23c4";//设备id
        



        //判断是否注册
        public static string isregister(string phone,int a)
        {
            string currentTimeMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            string signature =  siteId + "1.9.3" + currentTimeMillis +  phone + apiSign + appid + "android";
            if (a == 2)
            {
                signature = siteId + "1.9.3" + phone +  currentTimeMillis + apiSign + appid + "android";
            }
            signature = myUtil.getMD5(signature);
            Ajax ajax1 = new Ajax();
            ajax1.Method = "post";
            ajax1.Url = "https://jiyunlincheng.hebyun.com.cn/memberapi/api/member/isRegister";
            ajax1.Body = "phone=" + phone + "&currentTimeMillis=" + currentTimeMillis + "&appId=" + appid + "&siteId=" + siteId + "&versionName=1.9.3&platform=android&signature=" + signature;
            string result = ajax1.MakeRequest();
            return result;
        }

        //发送验证码
        public static string sendCode(string phone,int a)
        {
            string currentTimeMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString(); 
            string signature =  siteId + "1.9.3" + currentTimeMillis +  phone + apiSign + appid + "android";
            if (a == 2)
            {
                signature = siteId + "1.9.3" + phone +  currentTimeMillis + apiSign + appid + "android";
            }
            signature = myUtil.getMD5(signature);
            Ajax ajax1 = new Ajax();
            ajax1.Method = "post";
            ajax1.Url = "https://jiyunlincheng.hebyun.com.cn/memberapi/api/member/sendAuthCode";
            ajax1.Body = "phone=" + phone + "&currentTimeMillis=" + currentTimeMillis + "&appId=" + appid + "&siteId=" + siteId + "&versionName=1.9.3&platform=android&signature=" + signature;
            string result = ajax1.MakeRequest();
            return result;
            //dynamic json1 = JsonConvert.DeserializeObject(result);
            //return json1.status;
        }

        //注册
        public static string doMRegis(string phone, string nonce, int a)
        {
            string currentTimeMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            string signature = "";
            if (a == 1) signature =  siteId + "1.9.3" + currentTimeMillis + phone  + apiSign + nonce + appid + "Android" + password;
            else if (a == 2) signature = siteId + "1.9.3" + currentTimeMillis + phone + apiSign + appid + nonce + "Android" + password;
            else if (a == 3) signature =  siteId + "1.9.3" + nonce + phone + currentTimeMillis + apiSign + appid + "Android" + password;
            else if (a == 4) signature =  siteId + "1.9.3" + currentTimeMillis + phone + nonce + apiSign + appid + "Android" + password;
            else if (a == 5) signature = siteId + "1.9.3" + phone +  currentTimeMillis + apiSign + nonce + appid + "Android"+password;
            else if (a == 6) signature = siteId + "1.9.3" + phone +  currentTimeMillis + apiSign + appid + nonce + "Android"+password;
            else if (a == 7) signature = siteId + "1.9.3" + nonce + currentTimeMillis +  phone + apiSign + appid + "Android"+password;
            else if (a == 8) signature = siteId + "1.9.3" +  phone + currentTimeMillis + nonce + apiSign + appid + "Android"+password;
            signature = myUtil.getMD5(signature);
            Ajax ajax1 = new Ajax();
            ajax1.Method = "post";
            ajax1.Url = "https://jiyunlincheng.hebyun.com.cn/memberapi/api/member/doMRegis";
            ajax1.Body = "password="+password+"&phone=" + phone + "&currentTimeMillis=" + currentTimeMillis + "&appId=" + appid + "&siteId=" + siteId + "&versionName=1.9.3&nonce=" + nonce + "&platform=Android&signature=" + signature;
            string result = ajax1.MakeRequest();
            return result;
        }

        //登录
        public static string dologin(string phone,int a)
        {
            string currentTimeMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            string signature = "";
            if (a == 1) signature = siteId + "1.9.3" + phone + currentTimeMillis + apiSign + appid + password + "Android" + deviceToken;
            else if (a == 2) signature = siteId + "1.9.3" + currentTimeMillis + phone + apiSign + appid + password + "Android" + deviceToken;
            else if (a == 3) signature = siteId + "1.9.3" + currentTimeMillis + phone + apiSign + appid + "Android" + deviceToken + password;
            //还有其他解密方法没找齐

            signature = myUtil.getMD5(signature);
            Ajax ajax1 = new Ajax();
            ajax1.Method = "post";
            ajax1.Url = "https://jiyunlincheng.hebyun.com.cn/memberapi/api/member/doMLogin";
            ajax1.Body = "password="+password+"&phone=" + phone + "&currentTimeMillis=" + currentTimeMillis + "&appId=" + appid + "&siteId=" + siteId + "&versionName=1.9.3&platform=Android&deviceToken="+ deviceToken + "&signature=" + signature;
            string result = ajax1.MakeRequest();
            return result;
        }

        //填邀请码
        public static string setInviteCode(string userId, int a)
        {
            string url = "https://jiyunlincheng.hebyun.com.cn/memberapi/api/member/setInviteCode";
            string currentTimeMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            string signature = "";
            if (a == 1) signature = siteId + "1.9.3" + currentTimeMillis + apiSign + appid + userId +  inviteCode + "android";
            else if (a == 2) signature =  siteId + "1.9.3" + currentTimeMillis + apiSign + appid + userId + inviteCode + "android";
            else if (a == 3) signature = siteId + "1.9.3" + currentTimeMillis + apiSign + userId + appid +  inviteCode + "android";
            else if (a == 4) signature =  siteId + "1.9.3" + currentTimeMillis + apiSign + appid + inviteCode +  userId + "android";
            else if (a == 5) signature = siteId + "" + userId + "1.9.3" + currentTimeMillis + apiSign + appid +  inviteCode  + "android";
            else if (a == 6) signature = siteId + "1.9.3" + currentTimeMillis + userId + apiSign + appid +  inviteCode + "android";
            else if (a == 7) signature = siteId + "1.9.3" + userId + currentTimeMillis + apiSign + appid + inviteCode + "android";
            else if (a == 8) signature = userId + siteId + "1.9.3" + currentTimeMillis + apiSign + appid +  inviteCode + "android";
            else if (a == 9) signature = siteId + "1.9.3" + currentTimeMillis + apiSign + appid + inviteCode + "android" + userId;
            signature = myUtil.getMD5(signature);
            Ajax ajax1 = new Ajax();
            ajax1.Url = url;
            ajax1.Method = "POST";
            ajax1.Body = "currentTimeMillis=" + currentTimeMillis + "&appId=" + appid + "&inviteCode=" + inviteCode + "&siteId=" + siteId + "&versionName=1.9.3&userId=" + userId + "&platform=android&signature=" + signature;
            string result = ajax1.MakeRequest();
            return result;
        }

        //修改密码。没什么用了
        public static string dochangePass(string userid,int a)
        {
            string url = "https://jiyunlincheng.hebyun.com.cn/memberapi/api/member/doChangePass";
            string oldpassword = "b2e311cddac73bb6ab39ea42be49518a";//旧密码
            string newpassword = "8da01aebe8fc44e081f2f4cde855a28e";//新密码
            string currentTimeMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            string signature = "";
            if (a == 1) signature = siteId + "1.9.3" + currentTimeMillis + apiSign + appid + userid + newpassword + "android" + oldpassword;
            else if (a == 2) signature = siteId + "1.9.3" + currentTimeMillis + apiSign + appid + newpassword + "android" + oldpassword + userid;
            else if (a == 3) signature = userid + siteId + "1.9.3" + currentTimeMillis + apiSign + appid + newpassword + "android" + oldpassword;
            else if (a == 4) signature =  siteId + "1.9.3" + currentTimeMillis + apiSign +userid+ appid + newpassword + "android" + oldpassword;
            else if (a == 5) signature = siteId + "1.9.3" + currentTimeMillis + apiSign + appid   + newpassword +userid+ "android" + oldpassword;
            else if (a == 6) signature = siteId + "1.9.3" +userid+ currentTimeMillis + apiSign + appid + newpassword  + "android" + oldpassword;
            else if (a == 7) signature = siteId + userid + "1.9.3"  + currentTimeMillis + apiSign + appid + newpassword + "android" + oldpassword;
            else if (a == 8) signature = siteId  + "1.9.3" + currentTimeMillis +userid+ apiSign + appid + newpassword + "android" + oldpassword;
            else if (a == 9) signature = siteId + "1.9.3" + currentTimeMillis  + apiSign + appid + newpassword + "android"+userid + oldpassword;
            signature = myUtil.getMD5(signature);
            Ajax ajax1 = new Ajax();
            ajax1.Url = url;
            ajax1.Method = "POST";
            ajax1.Body = "currentTimeMillis="+ currentTimeMillis + "&appId="+appid+"&newPass="+ newpassword + "&siteId="+siteId+"&versionName=1.9.3&userId="+userid+"&platform=android&oldPass="+oldpassword+"&signature="+signature;
            string result = ajax1.MakeRequest();
            dynamic json1 = JsonConvert.DeserializeObject(result);
            return json1.msg;
        }

    }
}
