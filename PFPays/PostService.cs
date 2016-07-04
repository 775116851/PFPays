using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

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
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.Headers.Add("Accept-Language", "zh-cn");
            wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            byte[] bytReturn = wc.UploadValues(Url, "post", Inputs);
            wc.Dispose();
            return System.Text.Encoding.GetEncoding("utf-8").GetString(bytReturn);
        }
    }
}