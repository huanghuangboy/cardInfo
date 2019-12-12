using Castle.Core.Logging;
using MyCompanyName.AbpZeroTemplate.HttpHelper.Dto;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MyCompanyName.AbpZeroTemplate.IDCardHelpers
{

    public class IDCardHelper
    {
        private const String host = "https://naidcard.market.alicloudapi.com";
        private const String path = "/nidCard";
        private const String method = "GET";
        private const String appcode = "acab071df8d64d878e5cf5ed6eac20fc";
        public static string GetCardInfo(string idCard, string name)
        {
            String querys = "idCard=" + idCard + "&name=" + name;
            String bodys = "";
            String url = host + path;
            HttpWebRequest httpRequest = null;
            HttpWebResponse httpResponse = null;
            if (0 < querys.Length)
            {
                url = url + "?" + querys;
            }

            if (host.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                httpRequest = (HttpWebRequest)WebRequest.Create(url);
            }
            httpRequest.Method = method;
            httpRequest.Headers.Add("Authorization", "APPCODE " + appcode);
            if (0 < bodys.Length)
            {
                byte[] data = Encoding.UTF8.GetBytes(bodys);
                using (Stream stream = httpRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }
            Stream st = httpResponse.GetResponseStream();
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            return reader.ReadToEnd();
         

        }
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public static string StatusDesc(string status)
        {
            if (status == "01")
            {
                return "实名认证通过！";
            }
            else if (status == "02")
            {
                return "实名认证不通过！";
            }
            else if (status == "202")
            {
                return "无法验证！【中心中无此身份证记录，军人转业，户口迁移等】";
            }
            else if (status == "203")
            {
                return "异常情况！";
            }
            else if (status == "204")
            {
                return "姓名格式不正确！";
            }
            else if (status == "205")
            {
                return "身份证格式不正确！";
            }
            else return "";
        }
    }
}
