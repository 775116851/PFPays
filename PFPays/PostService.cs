using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Net;
using Org.BouncyCastle.X509;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace PFPays
{
    public class PostService
    {
        private static readonly ILog _log = log4net.LogManager.GetLogger(typeof(PostService));
        private NameValueCollection Inputs = new NameValueCollection();
        public string Url = "";
        public string Method = "Post";
        public string FormName = "form1";

        /// <summary>
        /// 添加需要提交的名和值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Add(string name, string value)
        {
            Inputs.Add(name, value);
        }

        /// <summary>
        /// 以输出Html方式POST
        /// </summary>
        public void Post()
        {
            System.Web.HttpContext.Current.Response.Clear();
            string html = string.Empty;
            html += ("<html><head>");
            html += (string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
            html += (string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
            try
            {
                for (int i = 0; i < Inputs.Keys.Count; i++)
                {
                    html += (string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", Inputs.Keys[i], Inputs[Inputs.Keys[i]]));
                }
                html += ("</form>");
                html += ("</body></html>");

                _log.Info(html);
                System.Web.HttpContext.Current.Response.Write(html);
                System.Web.HttpContext.Current.Response.End();
            }
            catch (Exception ee)
            {
                //
                _log.Error(string.Format("PostService..Post以输出Html方式POST出错:{0} 错误详情:{1}", ee.Message, ee.ToString()));
            }
        }

        public string SendHttpPost()
        {
            //基础连接已经关闭: 未能为SSL/TLS 安全通道建立信任关系。
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;  
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.Headers.Add("Accept-Language", "zh-cn");
            wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            byte[] bytReturn = wc.UploadValues(Url, "post", Inputs);
            wc.Dispose();
            return System.Text.Encoding.GetEncoding("utf-8").GetString(bytReturn);
        }

        public string GetHtmlString()
        {
            string html = string.Empty;

            html += ("<html><head>");
            html += (string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
            html += (string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
            try
            {
                for (int i = 0; i < Inputs.Keys.Count; i++)
                {
                    html += (string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", Inputs.Keys[i], Inputs[Inputs.Keys[i]]));
                }
                html += ("</form>");
                html += ("</body></html>");

                _log.Info(html);
            }
            catch (Exception ee)
            {
                //
                _log.Error(string.Format("PostService..Post以输出Html方式POST出错:{0} 错误详情:{1}", ee.Message, ee.ToString()));
            }
            return html;
        }

        public string PFPost(string postData)
        {
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;  
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData.Trim());
            System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(Url);
            req.Method = "POST";
            //req.ContentType = "application/x-www-form-urlencoded";
            req.ContentType = "application/json";
            //req.ContentType = "text/html";

            req.ContentLength = data.Length;

            System.IO.Stream newStream = req.GetRequestStream();
            //发送数据   
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            System.Net.HttpWebResponse res = (System.Net.HttpWebResponse)req.GetResponse();
            StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
            return reader.ReadToEnd();
        }

        public string Po(string postData)
        {
            //基础连接已经关闭: 未能为SSL/TLS 安全通道建立信任关系。
            //ServicePointManager.CertificatePolicy = new AcceptAllCertificatePolicy();
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;  

            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);
            System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(Url);
            req.Method = "POST";
            req.ContentType = "text/plain";//application/Json text/html
            req.ContentLength = data.Length;
            System.IO.Stream newStream = req.GetRequestStream();
            //发送数据   
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            System.Net.HttpWebResponse res = (System.Net.HttpWebResponse)req.GetResponse();
            StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
            return reader.ReadToEnd();
        }

        private bool ValidateServerCertificate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        } 
    }

    //http://radiumwong.iteye.com/blog/684118
    internal class AcceptAllCertificatePolicy : ICertificatePolicy
    {
        public AcceptAllCertificatePolicy()
        {
        }

        public bool CheckValidationResult(ServicePoint sPoint,
           System.Security.Cryptography.X509Certificates.X509Certificate cert, WebRequest wRequest, int certProb)
        {
            // Always accept
            return true;
        }

        private bool ValidateServerCertificate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }

}