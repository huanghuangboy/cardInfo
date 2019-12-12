using Castle.Core.Logging;
using MyCompanyName.AbpZeroTemplate.HttpHelper.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MyCompanyName.AbpZeroTemplate.IDCard2Helpers
{

    public class IDCard2Helper
    {
        private const String host = "https://eid.shumaidata.com";
        private const String path = "/eid/check";
        private const String method = "POST";
        private const String appcode = "acab071df8d64d878e5cf5ed6eac20fc";
        public static string GetCardInfo(string idCard, string name)
        {
            String querys = "idcard=" + idCard + "&name=" + name;
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

        //省市区截取
        public static Area getArea(string str)
        {
            var zhixia = new List<string> { "北京市", "上海市", "天津市", "重庆市" };
            var area = new Area();
            try
            {
                var index11 = 0;
                var index1 = str.IndexOf("省");
                if (index1 == -1)
                {
                    index11 = str.IndexOf("自治区");//
                    if (index11 != -1)
                    {
                        area.Province = str.Substring(0, index11 + 3);
                    }
                    else
                    {

                        area.Province = str.Substring(0, str.IndexOf("市") + 1);
                    }
                }
                else
                {
                    area.Province = str.Substring(0, index1 + 1);
                }

                var index2 = str.IndexOf("市");
                if (index11 == -1)
                {
                    area.City = str.Substring(index11 + 1, index2 + 1);
                }
                else
                {
                    if (index11 == 0)
                    {
                        area.City = str.Substring(index1 + 1, index2 - index1);
                    }
                    else
                    {
                        area.City = str.Substring(index11 + 3, index2 - (index11 + 2));
                    }
                }

                var index3 = str.LastIndexOf("区");
                if (index3 == -1)
                {
                    index3 = str.IndexOf("县");
                    area.Country = str.Substring(index2 + 1, index3 - index2);
                }
                else
                {
                    area.Country = str.Substring(index2 + 1, index3 - index2);
                }
                if (zhixia.Contains(area.City))
                {
                    area.City = area.Country;
                }
                var sp = str.Length / 2;
                if (string.IsNullOrEmpty(area.City))
                {
                    area.City = str.Substring(0, sp);
                }
                if (string.IsNullOrEmpty(area.Country))
                {
                    area.Country = str.Substring(sp, str.Length - sp);
                }
                return area;
            }
            catch (Exception ex)
            {
                var sp = str.Length / 2;
                if (string.IsNullOrEmpty(area.City))
                {
                    area.City = str.Substring(0, sp);
                }
                if (string.IsNullOrEmpty(area.Country))
                {
                    area.Country = str.Substring(sp, str.Length - sp);
                }
                return area;
            }
        }

    }

    public class Area
    {
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
    }
}
