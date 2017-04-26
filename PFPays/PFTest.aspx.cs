using log4net;
using System.Xml;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Runtime.Serialization.Json;
using System.IO;
using LitJson;
using System.Management;
using System.Runtime.InteropServices; 

namespace PFPays
{
    public partial class PFTest : System.Web.UI.Page
    {
        private static readonly ILog _log = log4net.LogManager.GetLogger(typeof(PFTest));
        protected void Page_Load(object sender, EventArgs e)
        {


            StringBuilder sSL = new StringBuilder();
            DateTime dtTime = DateTime.Now;
            string rOrderNo = "R" + "20000000123456789" + "_" + dtTime.ToString("yyyyMMddHHmmss");

            //System.Collections.Specialized.NameObjectCollectionBase.KeysCollection p = Request.ServerVariables.Keys;
            //foreach (string key in p)
            //{
            //    sSL.Append(key).Append("=").Append(Request.ServerVariables[key]).Append("&") ;
            //}
            //_log.Info("获取到的Request对象列表：" + sSL.ToString());
            //string mac = null;
            ////[DllImport("System.Management.dll")]  
            //ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
            //ManagementObjectCollection queryCollection = query.Get();
            //foreach (ManagementObject mo in queryCollection)
            //{
            //    if (mo["IPEnabled"].ToString() == "True")
            //        mac = mo["MacAddress"].ToString();
            //} 

            //try
            //{
            //    //获取网卡硬件地址 
            //    string mac = "";
            //    ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            //    ManagementObjectCollection moc = mc.GetInstances();
            //    foreach (ManagementObject mo in moc)
            //    {
            //        if ((bool)mo["IPEnabled"] == true)
            //        {
            //            mac = mo["MacAddress"].ToString();
            //            break;
            //        }
            //    }
            //}
            //catch
            //{
            //}
            //finally
            //{
            //}

            //try
            //{
            //    //获取IP地址 
            //    string st = "";
            //    ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            //    ManagementObjectCollection moc = mc.GetInstances();
            //    foreach (ManagementObject mo in moc)
            //    {
            //        if ((bool)mo["IPEnabled"] == true)
            //        {
            //            //st=mo["IpAddress"].ToString(); 
            //            System.Array ar;
            //            ar = (System.Array)(mo.Properties["IpAddress"].Value);
            //            st = ar.GetValue(0).ToString();
            //            break;
            //        }
            //    }
            //}
            //catch
            //{
            //}
            //finally
            //{
            //}

            string jsonContent = "测试1数V5据X";
            //string pu = "MIHNMA0GCSqGSIb3DQEBAQUAA4G7ADCBtwKBsQClbZvCusVUJulyMlScQ3zkaiEF/4neapRIkpmQLHvirNa23iVhoTk4dYHZIbeKv4GpnqRwG/424APHmhUdJiF5oAV6kFJ6QwgPQrXKx1tCd0poIC6uFD9BXhf+or2/0YwUJUjLE7MjiYoVTarcT41cw99lmLNbLv012SyCfdxmNqFTd4dxwL4BddZFW5QGVizdCZaH7w+5g6RmMvcAvfsKVaJB1IVe20wpMJq+WJuciwIBAw==";
            //string pr = "MIIDTQIBADANBgkqhkiG9w0BAQEFAASCAzcwggMzAgEAAoGxAKVtm8K6xVQm6XIyVJxDfORqIQX/id5qlEiSmZAse+Ks1rbeJWGhOTh1gdkht4q/gamepHAb/jbgA8eaFR0mIXmgBXqQUnpDCA9CtcrHW0J3SmggLq4UP0FeF/6ivb/RjBQlSMsTsyOJihVNqtxPjVzD32WYs1su/TXZLIJ93GY2oVN3h3HAvgF11kVblAZWLN0JlofvD7mDpGYy9wC9+wpVokHUhV7bTCkwmr5Ym5yLAgEDAoGwbkkSgdHY4sSboXbjEteomEbArqpb6Zxi2wxmYB2n7HM5zz7Dlmt7evkBO2vPsdUBG78YSr1UJJVX2ma4vhlrpmquUbWMUYIFX4HOhy+SLE+G8BV0dA1/f9aBUR6JdKziNUifVlKVywO1JdBGhIXVWGruUwAnDgzA8GjZNvCjCSvs8gUy1at33BZd6IUj274JVBBvb0VbXslWJOAiCZal0HmJJBTZXAQDshLHg3Kh9PsCWQDBsUcU91YqcOk1ThWNxUWSmLDjI8z5Bi3mxaXNTQRFaIQKYQOAVGZg9g7FltHxHGrxTYtKWABay2+T8VqCDXbA4hLS57oeETtO+7yn585dOLrLG59zyiMlAlkA2qS93/g6o8fbAwu0Cg0tcWGrsh1IjccqPLRDSyvB1nNJMYWslJNyE8fZqiRfTm3MYvvdSYY8XlNvXP4GhPk0lMEcD9w4PfEWUFjPDVeGeA3jSVPZuN6J7wJZAIEg2g36OXGgm3jeuQkuLmG7IJdtM1CuyUSDw94zWC5FrVxArQA4RECkCdkPNqC9nKDeXNw6qucySmKg5wFeTytBYeHv0Wlg0jSn0xqaiZN7JzISak0xbMMCWQCRwyk/+tHChTyssngGs3Og68fME4Wz2hwoeCzcx9aO94YhA8hjDPa32pEcGD+JnohB/T4xBCg+4ko9/q8DUM24gL1f6CV+oLmK5d9eOlmlXpeGN+Z7PwafAlhFM/qD4dZBOCWbwmF9vd9GTU1Xt2AAwYlAN8c3L4YZvED9QaZEmnaTfGJNK63yOW1fNPV7CZvp+9nH6c/sf/tA2BTd/Dv3faQ5GZbqEGIxloFWZnGz/YbV";
            //string lpu = "A56D9BC2BAC55426E97232549C437CE46A2105FF89DE6A94489299902C7BE2ACD6B6DE2561A139387581D921B78ABF81A99EA4701BFE36E003C79A151D262179A0057A90527A43080F42B5CAC75B42774A68202EAE143F415E17FEA2BDBFD18C142548CB13B323898A154DAADC4F8D5CC3DF6598B35B2EFD35D92C827DDC6636A153778771C0BE0175D6455B9406562CDD099687EF0FB983A46632F700BDFB0A55A241D4855EDB4C29309ABE589B9C8B";
            //string pfLpu = "EEE81E52267060718526F25F035175E924434A465DA4E9558E5A67C1168137193D9087FC83E21F39CEE713D3E3BF994529B962486E815203575C46802CAD7D7F2B2838A754DE4CCF4E49683AC521FDBC03CA333C9DB4B0336355F0423D871DA337F1F5784BF2AB51F59E4DB0733FB653FF02AD65A590D070AA7D1AC3EF7E516D8114537C87887236DC1F821B7469C330E373C331655EF245524CAB4B3F798A7E9671AE91B35746FA5377EEFFEF341C93";

            string pu = "MIHNMA0GCSqGSIb3DQEBAQUAA4G7ADCBtwKBsQCzcOreN6lOdOUiI3zy8a9ZMOODPg4eb3p4EUOYXFCqLIqESQAErHxgi8/hRjbLMaKbJit7/y+oGSwfLgpqLlwweLVioCpLEhxPRICsty5C/jnwJ5LWGpKGtP/dCE63SIx6rBN6q35f/zaJdB4TluzNRPfls0cLTXkyPryqvnj0tqy8NAS4xjLgHxvjoLnvcqlhWJlD3EGlA3BkQeYRCPiIe9zWkhPRU2y7Q+Jy3OaTCQIBAw==";
            string pr = "MIIDTQIBADANBgkqhkiG9w0BAQEFAASCAzcwggMzAgEAAoGxALNw6t43qU505SIjfPLxr1kw44M+Dh5vengRQ5hcUKosioRJAASsfGCLz+FGNssxopsmK3v/L6gZLB8uCmouXDB4tWKgKksSHE9EgKy3LkL+OfAnktYakoa0/90ITrdIjHqsE3qrfl//Nol0HhOW7M1E9+WzRwtNeTI+vKq+ePS2rLw0BLjGMuAfG+Ogue9yqWFYmUPcQaUDcGRB5hEI+Ih73NaSE9FTbLtD4nLc5pMJAgEDAoGwd6CclCUbiaNDbBeoofZ05iCXrNQJaZ+m+rYtED2LHB2xrYYAAx2oQF01QNl53MvBvMQc/VTKcBDIFMlcRsmSyvsjlxVxh2FoNNhVyHoe11QmoBph5BG3A1pbEJOs+S87Z+mBQyd3sAyn4IODTYGDbO8k6DndgzyEUdJurjUTJ6gYeH4wr8UtA0mY13kKtySt0tuF4pfNc4PXD7myS/7xiMyiC9sfusmpL/bjekJDH4MCWQDj5eO0cKzwOGKMMYWrY5Gf8NInzxmKe8Nnoiv4kwB0FgKEt8pq6DrW57yE8yY6RWgtxaZlwGw5XT4mQMPo+tg7aD0s1nw31gb/zzWcihujqj1ZMERNolKrAlkAyZFgdlqUkXr8QaAQROdGTEnmhwoFyivmdp5dY+fF/py0/l7bA/P+Y6BK8ciK5CnzAvD6BWlwgkdm6Q+sD5KfgkWPfoQi0rg0SRqPLNodgUS1+Fz3K9+RGwJZAJful82gc0rQQbLLrnJCYRVLNsU0u7Gn15psHVBiAE1kAa3P3EdFfI9FKFiiGXwuRXPZGZkq8tDo1BmAgptR5XzwKMiO/XqOr1U0zmhcEm0cKOYgLYkW4ccCWQCGYOr5kbhg/KgrwArYmi7dhpmvXAPcHURPFD5CmoP/EyNUPzytTVRCatyhMFyYG/dXS1FY8PWsL5nwtR1ftxUBg7T/AsHh0CLbZwod5r5WLc6lk09ylQtnAlgxadKbufjRV0JTEwPKazsEXP6IW414U++UEgpxmea6l37uVAKywVTDqt6q8vjJy9DoL008GIbPCzyxBe2EQmh44o9g8Esfp2T2QMtCW9QZSO7TWtiVeqeW";
            string lpu = "B370EADE37A94E74E522237CF2F1AF5930E3833E0E1E6F7A781143985C50AA2C8A84490004AC7C608BCFE14636CB31A29B262B7BFF2FA8192C1F2E0A6A2E5C3078B562A02A4B121C4F4480ACB72E42FE39F02792D61A9286B4FFDD084EB7488C7AAC137AAB7E5FFF3689741E1396ECCD44F7E5B3470B4D79323EBCAABE78F4B6ACBC3404B8C632E01F1BE3A0B9EF72A961589943DC41A503706441E61108F8887BDCD69213D1536CBB43E272DCE69309";
            string pfLpu = "EEE81E52267060718526F25F035175E924434A465DA4E9558E5A67C1168137193D9087FC83E21F39CEE713D3E3BF994529B962486E815203575C46802CAD7D7F2B2838A754DE4CCF4E49683AC521FDBC03CA333C9DB4B0336355F0423D871DA337F1F5784BF2AB51F59E4DB0733FB653FF02AD65A590D070AA7D1AC3EF7E516D8114537C87887236DC1F821B7469C330E373C331655EF245524CAB4B3F798A7E9671AE91B35746FA5377EEFFEF341C93";

            pr = "M1IIDTQIBADANBgkqhkiG9w0BAQEFAASCAzcwggMzAgEAAoGxAJl7xSSvTEMJ6EnyD55hKnrYuFTWGdBW7LRJUt37YqF3NsiiaYDRBLGWL5gfFrkqU81KJ6Q+tMxBWfGYnPW7zNUBIAqL2/hFcXhfmfF6uQPYTEsmi2urYrovCsLJbciYWLTK4nKhYfAT7th3om9n1M7MdQoOrZzt9g0t6gc9Hl+C8xfn6t4V8O78dp0w9ix+mBpA4Tw29V5vWQkl6sm+yoeNV562sYZVJcOj22XQZ9BnAgEDAoGwZlKDbcoy11vwMUwKaZYcUeXQOI674DnzItuMk/zsa6TPMGxGVeCty7l1EBS50MbiiNwabX8jMtY79mW9+Sfd41YVXF09UC5LpZURS6cmApAy3MRc8nJB0GrF2G+llgZf95RM7zTunKIz2UcY0Lc8gE1x8ppwBI4jjU88wLv7LOGL0xdvRa+PIagguncr/DeBKeFQiBd16M+C3Y9Qxj5F1s6nsdUe78fbHVajzGCZG6sCWQDdd2KudrZSjnnLHsobY+mh3doiB3lVP1gXSqe8xVzK++EtSVJNiqMtnLqzNe1yxW0ZYG6o9H5J8A2pFNFETSwQYJadP/khiJVz6uaiePLwJHc3qtg4aCanAlkAsWqbc36xPDpHoVBBtpgbfsM4avW8/7q2QP92akA5TcTYCcWT1Zr5AwSgkY4IG9TPZuUW1T+z4WZNxdOez3dVJ7QmMbGnP9kv43EtVIoruTigajrbBxoAQQJZAJOk7HRPJDcJpodp3BJCm8E+kWwE+44qOroxxSiDkzH9QMjbjDOxwh5ofHd5SPcuSLuVnxtNqYagCRtjNi2IyArrDxN/+2uwY6KcmcGl90rC+iUckCWaxG8CWHZHEkz/INLRhRY1gSRlZ6nXevH5KKp8eYCqTvGAJjPYkAaDt+O8pgIDFbZesBKN35nuDzjVIpZEM9k3vzT6OMUixCEhGiqQypegyOMGx9DQavF8kgS8ACsCWQDVNNkzKclkOsrpI4Pbul32sULTP0aDc2/AuynwkN+YtKtGHL8g0CzpZ6Sfdn29PyWLz6v/G/EnRchRUSzbyZItS5CRecAK6/IzQ8VkyCk4fScjHQP8giKe";
            pfLpu = "D128C9670D6B19989BEEAB5D17FF3971EC25BA13E0E90DE29FADB633C567D431A6F1C798143DAD534C1BCE1D697AC88705EFEA6F488B97ED239BA98ADA942D1C06CE03FECD7B9B438CCA0B0681DA080294A718B733A6F345569F229AFBAE092DAF264BF00B82890A51DCDB6EB56F0E8E9CBFE95E17924E4E0E848AB63CDAAB92B3AE829E49F8A12FAE1C319CF4BE59C9002C90662C1A3B577309B21C6C31E6B15A568BD7FCD67EE0A33F6FE1A7B281123";

            //公钥 私钥
            RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(pr));
            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(pu));

            //裸公钥(自己)
            BigInteger mod1 = new BigInteger(lpu, 16);
            BigInteger pub1 = new BigInteger("03", 16);
            RsaKeyParameters pulickKeyL = new RsaKeyParameters(false, mod1, pub1);

            //裸公钥(浦发)
            BigInteger mod2 = new BigInteger(pfLpu, 16);
            BigInteger pub2 = new BigInteger("03", 16);
            RsaKeyParameters pulickKeyLP = new RsaKeyParameters(false, mod2, pub2);

            //string r = "35C118B60DB38B20BA8A93CE5C2DEC61782C2855E6BBCF4C42DEE960BE9A7328AD1E8E66D6E35C8E38482DFE707792B6EFA73AA9E69FEFD7BCC4B08EB83E7CFF6BFE4B61B92267645C53F68E5C54DAAD88DD3820337BBFB2EFA356A6169BADED9C14CB1D41D7C0070164190BF5FE5C52BD645E332C62704BF881D3B33F5C9C4122BC204181B453C5D14A6B1EAEB5AE90F4AF54AD91F4C1EC3BE9ED5C002CC972E5927061207A9E2FA1AB3406A5133445";
            //string a = "{\"bigOrderDate\":\"20170415\",\"bigOrderNo\":\"1170415034103463\",\"bigReqNo\":\"1170415034103463\",\"orderStatus\":\"B\",\"bigOrderAmt\":\"20000\",\"bigPayAmt\":\"20000\",\"innerTranSeq\":\"20170415034106371829\",\"respCode\":\"0000\",\"respMsg\":\"成功\"}";
            //bool isPassA = ValitedSignX(r, a, pulickKeyLP);

            string rk = "{\"bigOrderDate\":\"20170425\",\"bigOrderNo\":\"1170425023124230\",\"bigReqNo\":\"1170425023124230\",\"orderStatus\":\"B\",\"bigOrderAmt\":\"617800\",\"bigPayAmt\":\"617800\",\"innerTranSeq\":\"20170425143140416947\",\"respCode\":\"0000\",\"respMsg\":\"成功\"}&sign=8F8B7EA600D1C8BDA17FC713AB3AC97FA549580A3198446A598ABB62C89DA951BA650A5C9F3516C2989E197FD7E37F45BB3879331F5D5C28E5798BBD76419136F9E7ECCF65EDEB9A182B9FB0EEFDD995E2058BC817715962FB7151F587DB661AE5F946B6AAEE2EE00EF0505EE8F8D52D8EA1673BD4D10FB0167817BD88968A62B3EB7A695CCF7287FD40FFDFB9A71C3E250B80F1191A952654F91EF086ED204565BC4A0BAC1F920079D00676359F2E81";
            string[] resultR = rk.Trim().Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
            if (resultR.Length == 2)
            {
                string RResult = resultR[0];
                string RSignParam = resultR[1];
                JsonData jdP = JsonMapper.ToObject(RResult.Trim());
                if (jdP.Count > 0)
                {
                    string RrespCode = Convert.ToString(jdP["respCode"]);//响应码
                    string RrespMsg = Convert.ToString(jdP["respMsg"]);//响应描述
                    string RbigOrderDate = string.Empty;
                    string RbigOrderNo = string.Empty;
                    string RbigReqNo = string.Empty;
                    string RorderStatus = string.Empty;
                    string RbigOrderAmt = string.Empty;
                    string RbigPayAmt = string.Empty;
                    string RinnerTranSeq = string.Empty;
                    if (RrespCode == "0000")
                    {
                        RbigOrderDate = Convert.ToString(jdP["bigOrderDate"]);//总订单日期 格式：YYYYMMDD
                        RbigOrderNo = Convert.ToString(jdP["bigOrderNo"]);//总订单号唯一标识
                        RbigReqNo = Convert.ToString(jdP["bigReqNo"]);//总订单请求流水号唯一标识
                        RorderStatus = Convert.ToString(jdP["orderStatus"]);//订单状态 A请求 B成功 C请求 D待支付
                        RbigOrderAmt = Convert.ToString(jdP["bigOrderAmt"]);//总订单金额 单位：分
                        RbigPayAmt = Convert.ToString(jdP["bigPayAmt"]);//总支付金额 单位：分
                        RinnerTranSeq = Convert.ToString(jdP["innerTranSeq"]);//支付平台内部流水号
                    }
                    //解密
                    bool isCheck = ValitedSignX(RSignParam.Trim().Split('=')[1], RResult, pulickKeyLP);//pulickKeyL
                    //log.Info("调用浦发快捷查询接口解密结果：" + isCheck);
                }
            }

            //string mJson = "0440012.0.00310010021070612017-03-29T16:44:3601{\"respCode\":\"0000\",\"respMsg\":\"成功\",\"bigOrderDate\":\"2017-03-29\",\"bigOrderStatus\":\"B\",\"bigOrderNo\":\"888820170316103500666675\",\"bigOrderReqNo\":\"888820170316103500666675\",\"bigOrderAmt\":\"100\",\"marketAmt\":\"\",\"bigOrderDealDateTime\":\"2017-03-29T16:44:36\",\"innerTransNo\":\"20170329163959510635\"}";
            //string mSign = "5FE5CD1739BDBA51C90612A7C3F8D956";
            ////解密
            //bool isCK = ValitedSignX(mSign, mJson, pulickKeyLP);//pulickKeyL
            //_log.Info("支付回调接口浦发解密结果：" + isCK);
            //OrderCreate(pulickKeyL, privateKeyParam, pulickKeyLP);
            //OrderPayBakUrl(pulickKeyLP);
            //return;

            PostService ps = new PostService();
            //ps.Url = "http://localhost:57393/Pay/Test";
            //string jsonData = "{\"data\":{\"bigOrderAmt\":\"100\",\"bigOrderDate\":\"2017-03-27\",\"bigOrderDealDateTime\":\"2017-03-27T11:33:57\",\"bigOrderNo\":\"888820170316103500666668\",\"bigOrderReqNo\":\"888820170316103500666668\",\"bigOrderStatus\":\"B\",\"innerTransNo\":\"20170327113003510405\",\"marketAmt\":\"\",\"respCode\":\"0000\",\"respMsg\":\"成功\"},\"interfaceId\":\"044001\",\"reqDateTime\":\"2017-03-27T11:33:57\",\"serviceCode\":\"031001002107061\",\"sign\":\"3E84B348C082B0436A403BC8DD153C53413EB0024F3FC6DC9E4D2065FBED3590B720E7EB7239600A3E261DB8EBA7EE610170B942B4B26FF8F342E4BE95D7C702F85AE5D5BF017916275E0D49DD53DB23AEAECDB26B08FA3CD6AB001B2F17D2FB9DD7415F27604A0FDA250EA02CD5003A293AC480BB3DE44D157D32DA4F9B2571A2E6CE0A3845B1BD5FB4CD682AD8DAB9E1AA9DB966D3A4B81D73CB0BAE3634AC1B8BF7574E69CAD6DFF2ED0F13CD56BC\",\"signType\":\"01\",\"version\":\"2.0.0\"}";

            ps.Url = "http://localhost:28850/Order/QueryPFPayInfo";
            string jsonData = "data=" + HttpUtility.UrlEncode("a4opgHTcQeeuSTOmfh3QHLOdkKOzh3Z5t44f6lpbimWlMQfA4JVsLTlmt26+Wa2zAXPU+IKvvxDhxpGPZlr5P6UxB8DglWwtn0JmOIEhmSEdkl0UwUxCRFX2l4S1jnC36jlbEkkPteo=");
            //string jsonData = "张654weewr三";
            //UTF8Encoding utf8 = new UTF8Encoding();
            //Byte[] encodedBytes = utf8.GetBytes(jsonData);
            //jsonData = utf8.GetString(encodedBytes); ;
            string TT = ps.PFPost(jsonData);
            //
            //UTF8Encoding utf8 = new UTF8Encoding();
            //Byte[] encodedBytes = utf8.GetBytes(jsonData);
            //jsonData = utf8.GetString(encodedBytes);
            //string TT = System.Web.HttpUtility.UrlDecode(jsonData, Encoding.GetEncoding("UTF-8"));
            return;

            //OrderPayBakUrl(pulickKeyLP);
            //return;
            //string result = ps.SendHttpPost();
            //string result = ps.Po("");

            //string resultA = "{\"bigOrderAmt\":\"100\",\"bigOrderDate\":\"2017-03-27\",\"bigOrderDealDateTime\":\"2017-03-27T11:33:57\",\"bigOrderNo\":\"888820170316103500666668\",\"bigOrderReqNo\":\"888820170316103500666668\",\"bigOrderStatus\":\"B\",\"innerTransNo\":\"20170327113003510405\",\"marketAmt\":\"\",\"respCode\":\"0000\",\"respMsg\":\"成功\"}";
            //UTF8Encoding utf8 = new UTF8Encoding();
            //Byte[] encodedBytes = utf8.GetBytes(resultA);
            //resultA = utf8.GetString(encodedBytes); ;
            //string RsignA = "42CD627D96782DF8B8822AF352386BDC4B751E47885896A3E8C12BE381ACE1B0C8B8555F58059C010ED60722E07C47D89CCE4C711B956EE9BD09430062833EEF5A8D380ADA59B246690E5438247520C235544913D0FDCDBA4A48D75817B925B5A8BC334E1F584D27D2179CCC05D1DB5F100AEBCF52EA5C6658CDBFAD0793DB9ECA537C5A00D74747C05D6452A2F6F43A65A1451FFE4F7BBE50723D3FD793649132E3AF46C7E71B4211D7CC5AC6DF4A15";
            //bool isPassA = ValitedSignY(RsignA, resultA, pulickKeyLP);//pulickKeyL
            //_log.Info("支付回调接口浦发解密结果：" + isPassA);
            //return;
            string result = "{\"respCode\":\"0000\",\"respMsg\":\"成功\",\"interfaceId\":\"121002\",\"version\":\"2.0.0\",\"serviceCode\":\"031001002107061\",\"resDateTime\":\"2017-03-27T11:34:22\",\"signType\":\"01\",\"sign\":\"5963579C5B75BEEF9057F44A7997299C0A684059F00C9EEC92574F646EAC017A35B5AB648D16B605F5192A470CD73E6A83341139DE7E98552167C2FCDE7FD0537C437F175F41E41311882A6AAFD4E6C3363E9B4104CC4AA6B58B223D3AD59AB7976FEBCBD1B5273496799E334A4AE64B389B6C97AA6C185F061ECAC6806601C9AF5B66FC7419C9E85887F77D6530B9AA4FD4CA19837DFF94A7B298712B58B1CF305621AE9C05C9A0A0F38083C895A08F\",\"data\":{\"oldBigOrderNo\":\"888820170316103500666668\",\"oldBigOrderReqNo\":\"888820170316103500666668\",\"refundDate\":\"2017-03-27\",\"refundAmt\":\"100\",\"mchntCode\":\"031001002107061\",\"currencyCode\":\"156\",\"innerTransNo\":\"20170327114118510406\",\"refundReqNo\":\"R888820170316103500666668\",\"refundStatus\":\"B\"}}";
            //UTF8Encoding utf8 = new UTF8Encoding();
            //Byte[] encodedBytes = utf8.GetBytes(result);
            //result = utf8.GetString(encodedBytes); ;
            //解密
            if (!string.IsNullOrEmpty(result.Trim()))
            {
                JsonData jdP = JsonMapper.ToObject(result.Trim());
                if (jdP.Count > 0)
                {
                    string RrespCode = Convert.ToString(jdP["respCode"]);//响应码
                    string RrespMsg = Convert.ToString(jdP["respMsg"]);//响应描述
                    if (RrespCode == "0000")
                    {
                        string RinterfaceId = Convert.ToString(jdP["interfaceId"]);//接口编号
                        string Rversion = Convert.ToString(jdP["version"]);//接口版本
                        string RserviceCode = Convert.ToString(jdP["serviceCode"]);//一级商户代码
                        string RresDateTime = Convert.ToString(jdP["resDateTime"]);//响应日期时间
                        string RsignType = Convert.ToString(jdP["signType"]);//签名类型
                        string Rsign = Convert.ToString(jdP["sign"]);//签名
                        JsonData jdBankFields = jdP["data"];//银行响应消息
                        string RoldBigOrderNo = string.Empty;
                        string RoldBigOrderReqNo = string.Empty;
                        string RrefundDate = string.Empty;
                        string RrefundAmt = string.Empty;
                        string RmchntCode = string.Empty;
                        string RcurrencyCode = string.Empty;
                        string RinnerTransNo = string.Empty;
                        string RrefundReqNo = string.Empty;
                        string RrefundStatus = string.Empty;
                        if (jdBankFields.Count > 0)
                        {
                            RoldBigOrderNo = Convert.ToString(jdBankFields["oldBigOrderNo"]);
                            RoldBigOrderReqNo = Convert.ToString(jdBankFields["oldBigOrderReqNo"]);
                            RrefundDate = Convert.ToString(jdBankFields["refundDate"]);
                            RrefundAmt = Convert.ToString(jdBankFields["refundAmt"]);
                            RmchntCode = Convert.ToString(jdBankFields["mchntCode"]);
                            RcurrencyCode = Convert.ToString(jdBankFields["currencyCode"]);
                            RinnerTransNo = Convert.ToString(jdBankFields["innerTransNo"]);
                            RrefundReqNo = Convert.ToString(jdBankFields["refundReqNo"]);
                            RrefundStatus = Convert.ToString(jdBankFields["refundStatus"]);
                        }

                        StringBuilder sbRSign = new StringBuilder();
                        sbRSign.Clear();
                        sbRSign.Append(RrespCode).Append(RrespMsg).Append(RinterfaceId).Append(Rversion).Append(RserviceCode).Append(RresDateTime).Append(RsignType).Append(jdBankFields.ToJson());
                        _log.Info("创建订单接口浦发解密前报文：" + sbRSign.ToString());

                        //解密
                        bool isPass = ValitedSignX(Rsign, sbRSign.ToString(), pulickKeyLP);//pulickKeyL
                        _log.Info("创建订单接口浦发解密结果：" + isPass);
                    }
                }
            }

            //string result = "{\"respCode\":\"030019\",\"respMsg\":\"商户未开通该支付方式\",\"interfaceId\":\"121001\",\"version\":\"2.0.0\",\"serviceCode\":\"031001002107061\",\"resDateTime\":\"2017-03-23T15:25:48\",\"signType\":\"01\",\"sign\":\"D391F00E364E7E677B372E1E84228FFB1DCAD3E920597D5392642947768D0A307D79AB4618A83BE836DF201D4BA76FF5BB5511016AA6B76A6029B5E17286EE0CAD4B4769FDECF709EB103AE39E2D8AEA9BD2B738DF427E46DD79A5F43963A5B37BC5931ADA89B0827F2BCCA60BB1751F5690DC6D68AF51E8D2D82C8B3C8B7C2FEA3D0D75784BF18715F3CACE57E53F34EAAAED16CEA25B11B1FB46E6173A6EB0076B5683F75A5F16CBCC3380D8AA731A\",\"data\":{\"innerTransNo\":\"\"}}";
            //if (!string.IsNullOrEmpty(result.Trim()))
            //{
            //    JsonData jdP = JsonMapper.ToObject(result.Trim());
            //    if (jdP.Count > 0)
            //    {
            //        string RrespCode = Convert.ToString(jdP["respCode"]);//响应码
            //        string RrespMsg = Convert.ToString(jdP["respMsg"]);//响应描述
            //        if (RrespCode == "0000")
            //        {
            //            string RinterfaceId = Convert.ToString(jdP["interfaceId"]);//接口编号
            //            string Rversion = Convert.ToString(jdP["version"]);//接口版本
            //            string RserviceCode = Convert.ToString(jdP["serviceCode"]);//一级商户代码
            //            string RresDateTime = Convert.ToString(jdP["resDateTime"]);//响应日期时间
            //            string RsignType = Convert.ToString(jdP["signType"]);//签名类型
            //            string Rsign = Convert.ToString(jdP["sign"]);//签名
            //            JsonData jdBankFields = jdP["data"];//银行响应消息
            //            string RinnerTransNo = Convert.ToString(jdBankFields["innerTransNo"]);

            //            StringBuilder sbSign = new StringBuilder();
            //            sbSign.Clear();
            //            sbSign.Append(RrespCode).Append(RrespMsg).Append(RinterfaceId).Append(Rversion).Append(RserviceCode).Append(RresDateTime).Append(RsignType).Append(jdBankFields.ToJson());
            //            _log.Info("创建订单接口加密前报文：" + sbSign.ToString());

            //            //解密
            //            bool isPass = ValitedSignX(Rsign, sbSign.ToString(), pulickKeyLP);//pulickKeyL
            //            _log.Info("创建订单接口浦发解密结果：" + isPass);
            //        }
            //    }
            //}


            ////string m = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            //PostService ps = new PostService();
            //ps.Url = "https://222.44.42.5/entry_payfront/memberOrder/memberOrder.do";
            //string jsonData = "{\"interfaceId\":\"121001\",\"version\":\"2.0.0\",\"serviceCode\":\"1400100029\",\"reqDateTime\":\"2017-03-16T15:49:00\",\"signType\":\"01\",\"sign\":\"123456789\",\"data\":{\"bigOrderNo\":\"888820170316103500666666\"}}";

            //string result = ps.PFPost(jsonData);
            ////string result = ps.SendHttpPost();
            ////string result = ps.Po("");

            //OrderCreate(pulickKeyL, privateKeyParam, pulickKeyLP);
            //OrderRefund(pulickKeyL, privateKeyParam, pulickKeyLP);
            OrderPayBakUrl(pulickKeyLP);
            //OrderQuery(pulickKeyL, privateKeyParam, pulickKeyLP);

            //jsonContent = sbData.ToString().Trim();
            ////加密
            //string mSign = GetSign(jsonContent, privateKeyParam);
            //ps.Add("data", jsonContent);
            //ps.Add("sign", mSign);
            //ps.Url = "https://222.44.42.5/paycashier/account/queryOrderStatus.do";
            ////ps.Post();
            //string result = ps.SendHttpPost();
            ////string pData = ps.GetHtmlString();
            ////string pResult = ps.Po(pData);
            ////解密
            //bool isPass = ValitedSign(mSign, jsonContent, publicKeyParam);//pulickKeyL
        }

        //浦发广州 支付回调B 测试
        public static void OrderR()
        {
            PostService psN = new PostService();
            psN.Url = "http://localhost:28850/Order/WSTLPayBackBakUrl";
            string jsonDataN = "childmerid=000000000000001&completetime=20170412105005&errorCode=0000&merchantId=2017041123&orderAmount=1&orderDatetime=20170412103608&orderNo=1170412103608774&payAmount=1&payResult=成功&payType=0&returnDatetime=201704102105005&signType=1&transnumber=9001170412260732&version=v1.0&key=123456";
            psN.Add("childmerid", "000000000000001");
            psN.Add("completetime", "20170412105005");
            psN.Add("errorCode", "0000");
            psN.Add("merchantId", "2017041123");
            psN.Add("orderAmount", "1");
            psN.Add("orderDatetime", "20170412103608");
            psN.Add("orderNo", "1170412103608774");
            psN.Add("payAmount", "1");
            psN.Add("payResult", "成功");
            psN.Add("payType", "0");
            psN.Add("returnDatetime", "201704102105005");
            psN.Add("signType", "1");
            psN.Add("transnumber", "9001170412260732");
            psN.Add("version", "v1.0");
            psN.Add("key", "123456");
            psN.Add("signMsg", "18368e31b43043444f9fa5c4921d247e");
            string TTN = "";
            psN.Post();
            int mN = 1;
        }

        //浦发收银台 创建订单
        public static void OrderCreate(RsaKeyParameters publicKeyParam, RsaPrivateCrtKeyParameters privateKeyParam, RsaKeyParameters pulickKeyLP)
        {
            Hashtable htR = new Hashtable();
            htR.Add("interfaceId", "121001");//接口编号121001
            htR.Add("version", "2.0.0");//接口版本2.0.0
            htR.Add("serviceCode", "031001002107061");//一级商户代码
            htR.Add("reqDateTime", Convert.ToDateTime("2017-03-30 13:48:00").ToString("yyyy-MM-ddTHH:mm:ss"));//请求日期时间 yyyy-MM-ddTHH:mm:ss
            htR.Add("signType", "01");//签名类型 01
            Hashtable htD = new Hashtable();
            htD.Add("bigOrderNo", "888820170316103500666677");//总订单号
            htD.Add("bigOrderReqNo", "888820170316103500666677");//总订单流水号
            htD.Add("bigOrderDate", "2017-03-30");//总订单日期 yyyy-MM-dd
            htD.Add("bigOrderAmt", "100");//总订单金额 分
            htD.Add("backUrl", "http://115.29.143.43/xpmall/Order/PFPayBackBakUrl");//后台通知地址   http://www.soonwill.com/swapp/Order/WXBakUrl
            htD.Add("clientIp", "127.0.0.1");//终端IP地址
            htD.Add("clientMac", "6A-14-01-9C-39-55");//终端MAC地址 XX-XX-XX-XX-XX-XX
            htD.Add("termType", "08");//终端类型 08手机设备
            htD.Add("userId", "00000092909865");//会员编号   00000089581277
            Hashtable htOF = new Hashtable();
            htOF.Add("mchntCode", "031001002107061");//二级商户代码
            htOF.Add("orderNo", "888820170316103500666677");//订单号
            htOF.Add("orderReqNo", "888820170316103500666677");//订单流水号
            htOF.Add("orderDate", "2017-03-30");//订单日期 yyyy-MM-dd
            htOF.Add("orderAmt", "100");//订单金额 分
            htOF.Add("validateFlag", "0");//订单有效期标识 0：永久 1：指定
            //htOF.Add("validate", "");//订单有效期 有效期标识为1时 yyyy-MM-ddTHH:mm:ss
            StringBuilder sbData = new StringBuilder();
            sbData.Clear();
            sbData.Append("{\"bigOrderNo\":\"").Append(htD["bigOrderNo"]).Append("\"");
            sbData.Append(",\"bigOrderReqNo\":\"").Append(htD["bigOrderReqNo"]).Append("\"");
            sbData.Append(",\"bigOrderDate\":\"").Append(htD["bigOrderDate"]).Append("\"");
            sbData.Append(",\"bigOrderAmt\":\"").Append(htD["bigOrderAmt"]).Append("\"");
            sbData.Append(",\"backUrl\":\"").Append(htD["backUrl"]).Append("\"");
            sbData.Append(",\"clientIp\":\"").Append(htD["clientIp"]).Append("\"");
            sbData.Append(",\"clientMac\":\"").Append(htD["clientMac"]).Append("\"");
            sbData.Append(",\"termType\":\"").Append(htD["termType"]).Append("\"");
            sbData.Append(",\"userId\":\"").Append(htD["userId"]).Append("\"");
            sbData.Append(",\"orderFields\":[{");//订单信息域
            sbData.Append("\"mchntCode\":\"").Append(htOF["mchntCode"]).Append("\"");
            sbData.Append(",\"orderNo\":\"").Append(htOF["orderNo"]).Append("\"");
            sbData.Append(",\"orderReqNo\":\"").Append(htOF["orderReqNo"]).Append("\"");
            sbData.Append(",\"orderDate\":\"").Append(htOF["orderDate"]).Append("\"");
            sbData.Append(",\"orderAmt\":\"").Append(htOF["orderAmt"]).Append("\"");
            sbData.Append(",\"validateFlag\":\"").Append(htOF["validateFlag"]).Append("\"");
            sbData.Append("}]");
            sbData.Append("}");
            htR.Add("data", sbData.ToString());//业务数据 JSON格式

            StringBuilder sbSign = new StringBuilder();
            sbSign.Clear();
            sbSign.Append(htR["interfaceId"]).Append(htR["version"]).Append(htR["serviceCode"]).Append(htR["reqDateTime"]).Append(htR["signType"]).Append(htR["data"]);

            _log.Info("创建订单接口加密前报文：" + sbSign.ToString());
            //加密
            string mSign = GetSignX(sbSign.ToString(), privateKeyParam);
            htR.Add("sign", mSign);//签名

            //请求数据Json
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.Clear();
            sbPostData.Append("{\"interfaceId\":\"").Append(htR["interfaceId"]).Append("\"");
            sbPostData.Append(",\"version\":\"").Append(htR["version"]).Append("\"");
            sbPostData.Append(",\"serviceCode\":\"").Append(htR["serviceCode"]).Append("\"");
            sbPostData.Append(",\"reqDateTime\":\"").Append(htR["reqDateTime"]).Append("\"");
            sbPostData.Append(",\"signType\":\"").Append(htR["signType"]).Append("\"");
            sbPostData.Append(",\"sign\":\"").Append(htR["sign"]).Append("\"");
            sbPostData.Append(",\"data\":").Append(htR["data"]).Append("");
            sbPostData.Append("}");

            _log.Info("创建订单接口请求报文：" + sbPostData.ToString());
            PostService ps = new PostService();
            ps.Url = "https://222.44.42.5/entry_payfront/memberOrder/memberOrder.do";
            string result = ps.PFPost(sbPostData.ToString());
            _log.Info("创建订单接口返回结果：" + result);

            //解密
            if (!string.IsNullOrEmpty(result.Trim()))
            {
                JsonData jdP = JsonMapper.ToObject(result.Trim());
                if (jdP.Count > 0)
                {
                    string RrespCode = Convert.ToString(jdP["respCode"]);//响应码
                    string RrespMsg = Convert.ToString(jdP["respMsg"]);//响应描述
                    if (RrespCode == "0000")
                    {
                        string RinterfaceId = Convert.ToString(jdP["interfaceId"]);//接口编号
                        string Rversion = Convert.ToString(jdP["version"]);//接口版本
                        string RserviceCode = Convert.ToString(jdP["serviceCode"]);//一级商户代码
                        string RresDateTime = Convert.ToString(jdP["resDateTime"]);//响应日期时间
                        string RsignType = Convert.ToString(jdP["signType"]);//签名类型
                        string Rsign = Convert.ToString(jdP["sign"]);//签名
                        JsonData jdBankFields = jdP["data"];//银行响应消息
                        string RinnerTransNo = string.Empty;
                        if (jdBankFields.Count > 0)
                        {
                            RinnerTransNo = Convert.ToString(jdBankFields["innerTransNo"]);
                        }

                        StringBuilder sbRSign = new StringBuilder();
                        sbRSign.Clear();
                        sbRSign.Append(RrespCode).Append(RrespMsg).Append(RinterfaceId).Append(Rversion).Append(RserviceCode).Append(RresDateTime).Append(RsignType).Append(jdBankFields.ToJson());
                        _log.Info("创建订单接口浦发解密前报文：" + sbRSign.ToString());

                        //解密
                        bool isPass = ValitedSignX(Rsign, sbRSign.ToString(), pulickKeyLP);//pulickKeyL
                        _log.Info("创建订单接口浦发解密结果：" + isPass);
                    }
                }
            }
            ////解密
            //bool isPass = ValitedSignX(mSign, sbSign.ToString(), publicKeyParam);//pulickKeyL
            //_log.Info("创建订单接口自我解密结果：" + isPass);
        }

        //浦发收银台 退款订单
        public static void OrderRefund(RsaKeyParameters publicKeyParam, RsaPrivateCrtKeyParameters privateKeyParam, RsaKeyParameters pulickKeyLP)
        {
            Hashtable htR = new Hashtable();
            htR.Add("interfaceId", "121002");//接口编号121002
            htR.Add("version", "2.0.0");//接口版本2.0.0
            htR.Add("serviceCode", "031001002107061");//一级商户代码
            htR.Add("reqDateTime", Convert.ToDateTime("2017-03-27 11:35:00").ToString("yyyy-MM-ddTHH:mm:ss"));//请求日期时间 yyyy-MM-ddTHH:mm:ss
            htR.Add("signType", "01");//签名类型 01

            Hashtable htD = new Hashtable();
            htD.Add("oldBigOrderNo", "888820170316103500666668");//原总订单号
            htD.Add("oldBigOrderReqNo", "888820170316103500666668");//原总订单流水号
            htD.Add("refundDate", "2017-03-27");//退款日期 yyyy-MM-dd
            htD.Add("refundAmt", "100");//退款金额 分
            htD.Add("mchntCode", "031001002107061");//二级商户代码
            htD.Add("currencyCode", "156");//交易货币代码 156人民币
            htD.Add("refundReqNo", "R888820170316103500666668");//退款流水号

            StringBuilder sbData = new StringBuilder();
            sbData.Clear();
            sbData.Append("{\"oldBigOrderNo\":\"").Append(htD["oldBigOrderNo"]).Append("\"");
            sbData.Append(",\"oldBigOrderReqNo\":\"").Append(htD["oldBigOrderReqNo"]).Append("\"");
            sbData.Append(",\"refundDate\":\"").Append(htD["refundDate"]).Append("\"");
            sbData.Append(",\"refundAmt\":\"").Append(htD["refundAmt"]).Append("\"");
            sbData.Append(",\"mchntCode\":\"").Append(htD["mchntCode"]).Append("\"");
            sbData.Append(",\"currencyCode\":\"").Append(htD["currencyCode"]).Append("\"");
            sbData.Append(",\"refundReqNo\":\"").Append(htD["refundReqNo"]).Append("\"");
            sbData.Append("}");
            htR.Add("data", sbData.ToString());//业务数据 JSON格式

            StringBuilder sbSign = new StringBuilder();
            sbSign.Clear();
            sbSign.Append(htR["interfaceId"]).Append(htR["version"]).Append(htR["serviceCode"]).Append(htR["reqDateTime"]).Append(htR["signType"]).Append(htR["data"]);

            _log.Info("退款订单接口加密前报文：" + sbSign.ToString());
            //加密
            string mSign = GetSignX(sbSign.ToString(), privateKeyParam);
            htR.Add("sign", mSign);//签名

            //请求数据Json
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.Clear();
            sbPostData.Append("{\"interfaceId\":\"").Append(htR["interfaceId"]).Append("\"");
            sbPostData.Append(",\"version\":\"").Append(htR["version"]).Append("\"");
            sbPostData.Append(",\"serviceCode\":\"").Append(htR["serviceCode"]).Append("\"");
            sbPostData.Append(",\"reqDateTime\":\"").Append(htR["reqDateTime"]).Append("\"");
            sbPostData.Append(",\"signType\":\"").Append(htR["signType"]).Append("\"");
            sbPostData.Append(",\"sign\":\"").Append(htR["sign"]).Append("\"");
            sbPostData.Append(",\"data\":").Append(htR["data"]).Append("");
            sbPostData.Append("}");

            _log.Info("退款订单接口请求报文：" + sbPostData.ToString());
            PostService ps = new PostService();
            ps.Url = "https://222.44.42.5/entry_payfront/mchntOrder/refundMchntOrder.do";
            string result = ps.PFPost(sbPostData.ToString());
            _log.Info("退款订单接口返回结果：" + result);

            //解密
            if (!string.IsNullOrEmpty(result.Trim()))
            {
                JsonData jdP = JsonMapper.ToObject(result.Trim());
                if (jdP.Count > 0)
                {
                    string RrespCode = Convert.ToString(jdP["respCode"]);//响应码
                    string RrespMsg = Convert.ToString(jdP["respMsg"]);//响应描述
                    if (RrespCode == "0000")
                    {
                        string RinterfaceId = Convert.ToString(jdP["interfaceId"]);//接口编号
                        string Rversion = Convert.ToString(jdP["version"]);//接口版本
                        string RserviceCode = Convert.ToString(jdP["serviceCode"]);//一级商户代码
                        string RresDateTime = Convert.ToString(jdP["resDateTime"]);//响应日期时间
                        string RsignType = Convert.ToString(jdP["signType"]);//签名类型
                        string Rsign = Convert.ToString(jdP["sign"]);//签名
                        JsonData jdBankFields = jdP["data"];//银行响应消息
                        string RoldBigOrderNo = string.Empty;
                        string RoldBigOrderReqNo = string.Empty;
                        string RrefundDate = string.Empty;
                        string RrefundAmt = string.Empty;
                        string RmchntCode = string.Empty;
                        string RcurrencyCode = string.Empty;
                        string RinnerTransNo = string.Empty;
                        string RrefundReqNo = string.Empty;
                        string RrefundStatus = string.Empty;
                        if(jdBankFields.Count > 0)
                        {
                            RoldBigOrderNo = Convert.ToString(jdBankFields["oldBigOrderNo"]);
                            RoldBigOrderReqNo = Convert.ToString(jdBankFields["oldBigOrderReqNo"]);
                            RrefundDate = Convert.ToString(jdBankFields["refundDate"]);
                            RrefundAmt = Convert.ToString(jdBankFields["refundAmt"]);
                            RmchntCode = Convert.ToString(jdBankFields["mchntCode"]);
                            RcurrencyCode = Convert.ToString(jdBankFields["currencyCode"]);
                            RinnerTransNo = Convert.ToString(jdBankFields["innerTransNo"]);
                            RrefundReqNo = Convert.ToString(jdBankFields["refundReqNo"]);
                            RrefundStatus = Convert.ToString(jdBankFields["refundStatus"]);
                        }

                        StringBuilder sbRSign = new StringBuilder();
                        sbRSign.Clear();
                        sbRSign.Append(RrespCode).Append(RrespMsg).Append(RinterfaceId).Append(Rversion).Append(RserviceCode).Append(RresDateTime).Append(RsignType).Append(jdBankFields.ToJson());
                        _log.Info("创建订单接口浦发解密前报文：" + sbRSign.ToString());

                        //解密
                        bool isPass = ValitedSignX(Rsign, sbRSign.ToString(), pulickKeyLP);//pulickKeyL
                        _log.Info("创建订单接口浦发解密结果：" + isPass);
                    }
                }
            }
            ////解密
            //bool isPass = ValitedSignX(mSign, sbSign.ToString(), publicKeyParam);//pulickKeyL
        }

        //浦发收银台 支付通知
        public static void OrderPayBakUrl(RsaKeyParameters pulickKeyLP)
        {
            //string result = "{\"interfaceId\":\"044001\",\"version\":\"2.0.0\",\"serviceCode\":\"031001002107061\",\"reqDateTime\":\"2017-03-23T17:20:45\",\"signType\":\"01\",\"sign\":\"112233AASSDD\",\"data\":{\"respCode\":\"0000\",\"respMsg\":\"Success\",\"bigOrderDate\":\"2017-03-23\",\"bigOrderStatus\":\"A\",\"bigOrderNo\":\"123\",\"bigOrderReqNo\":\"234\",\"bigOrderAmt\":\"345\",\"marketAmt\":\"100\",\"bigOrderDealDateTime\":\"2017-03-23T17:20:45\",\"innerTransNo\":\"200145221254\"}}";

            //string result = "{\"data\":{\"bigOrderAmt\":\"100\",\"bigOrderDate\":\"2017-03-27\",\"bigOrderDealDateTime\":\"2017-03-27T11:33:57\",\"bigOrderNo\":\"888820170316103500666668\",\"bigOrderReqNo\":\"888820170316103500666668\",\"bigOrderStatus\":\"B\",\"innerTransNo\":\"20170327113003510405\",\"marketAmt\":\"\",\"respCode\":\"0000\",\"respMsg\":\"成功\"},\"interfaceId\":\"044001\",\"reqDateTime\":\"2017-03-27T11:33:57\",\"serviceCode\":\"031001002107061\",\"sign\":\"3E84B348C082B0436A403BC8DD153C53413EB0024F3FC6DC9E4D2065FBED3590B720E7EB7239600A3E261DB8EBA7EE610170B942B4B26FF8F342E4BE95D7C702F85AE5D5BF017916275E0D49DD53DB23AEAECDB26B08FA3CD6AB001B2F17D2FB9DD7415F27604A0FDA250EA02CD5003A293AC480BB3DE44D157D32DA4F9B2571A2E6CE0A3845B1BD5FB4CD682AD8DAB9E1AA9DB966D3A4B81D73CB0BAE3634AC1B8BF7574E69CAD6DFF2ED0F13CD56BC\",\"signType\":\"01\",\"version\":\"2.0.0\"}";
            //UTF8Encoding utf8 = new UTF8Encoding();
            //Byte[] encodedBytes = utf8.GetBytes(result);
            //result = utf8.GetString(encodedBytes); ;
            //string result = "{\"data\":{\"bigOrderAmt\":\"100\",\"bigOrderDate\":\"2017-03-29\",\"bigOrderDealDateTime\":\"2017-03-29T16:44:36\",\"bigOrderNo\":\"888820170316103500666675\",\"bigOrderReqNo\":\"888820170316103500666675\",\"bigOrderStatus\":\"B\",\"innerTransNo\":\"20170329163959510635\",\"marketAmt\":\"\",\"respCode\":\"0000\",\"respMsg\":\"成功\"},\"interfaceId\":\"044001\",\"reqDateTime\":\"2017-03-29T16:44:36\",\"serviceCode\":\"031001002107061\",\"sign\":\"489FD752DB97F22A7A586A8BB248EBB04AE9948F93B928DF984FB5C58886AEBFB666AEA485B2084B79A12588B9340696BB3A6F65CDBDD15B2EC2C4472C721E203B7683CD2E9F3898CB60A039A0E6C9D28764186330F293B8F2C3A80AB0DAE8E65E53BEAB1D60D3CC2E6980945773F8CEBD642D6A0168A6B8AC7FEE9D380C869357E8607755D171CED036C716BCF121512E9206AC6B737F7F5422EACF54CAEC9ECE83D0541FDC415A591068EA064D5541\",\"signType\":\"01\",\"version\":\"2.0.0\"}";
            string result = "{\"data\":{\"bigOrderAmt\":\"100\",\"bigOrderDate\":\"2017-03-30\",\"bigOrderDealDateTime\":\"2017-03-30T14:00:09\",\"bigOrderNo\":\"888820170316103500666677\",\"bigOrderReqNo\":\"888820170316103500666677\",\"bigOrderStatus\":\"B\",\"innerTransNo\":\"20170330134934510738\",\"marketAmt\":\"\",\"respCode\":\"0000\",\"respMsg\":\"成功\"},\"interfaceId\":\"044001\",\"reqDateTime\":\"2017-03-30T14:00:09\",\"serviceCode\":\"031001002107061\",\"sign\":\"525817C581FE801729ECF121552527D0CA22555D95906C205ABE5A7A547C82903217DAEAC251E14E55E9FA51D92FE20CB1046ACFFCA92EB0E03E7550CB4083807E3F47E93D481B4EDBBBF6F89A5F3871A23FDF10447F67EC8C247DB382942646AE77062D915713E670276CBCCA29CB19073B0AAA05FE5AD830460980F8D55DC3861C28299A2CA437CFFDB0DC51D24A18FE810E8964EA0A0EE1738A8112C680D0204DFCD364C4560429C45F53C89EA18C\",\"signType\":\"01\",\"version\":\"2.0.0\"}";
            if (!string.IsNullOrEmpty(result.Trim()))
            {
                JsonData jdP = JsonMapper.ToObject(result.Trim());
                if (jdP.Count > 0)
                {
                    string RinterfaceId = Convert.ToString(jdP["interfaceId"]).Trim();//接口编号
                    string Rversion = Convert.ToString(jdP["version"]).Trim();//接口版本
                    string RserviceCode = Convert.ToString(jdP["serviceCode"]).Trim();//一级商户代码
                    string RreqDateTime = Convert.ToString(jdP["reqDateTime"]).Trim();//响应日期时间
                    string RsignType = Convert.ToString(jdP["signType"]).Trim();//签名类型
                    string Rsign = Convert.ToString(jdP["sign"]).Trim();//签名
                    JsonData jdBankFields = jdP["data"];//银行响应消息
                    string RrespCode = string.Empty;
                    string RrespMsg = string.Empty;
                    string RbigOrderDate = string.Empty;
                    string RbigOrderStatus = string.Empty;
                    string RbigOrderNo = string.Empty;
                    string RbigOrderReqNo = string.Empty;
                    string RbigOrderAmt = string.Empty;
                    string RmarketAmt = string.Empty;
                    string RbigOrderDealDateTime = string.Empty;
                    string RinnerTransNo = string.Empty;

                    if (jdBankFields.Count > 0)
                    {
                        RrespCode = Convert.ToString(jdBankFields["respCode"]);
                        RrespMsg = Convert.ToString(jdBankFields["respMsg"]);
                        RbigOrderDate = Convert.ToString(jdBankFields["bigOrderDate"]);
                        RbigOrderStatus = Convert.ToString(jdBankFields["bigOrderStatus"]);
                        RbigOrderNo = Convert.ToString(jdBankFields["bigOrderNo"]);
                        RbigOrderReqNo = Convert.ToString(jdBankFields["bigOrderReqNo"]);
                        RbigOrderAmt = Convert.ToString(jdBankFields["bigOrderAmt"]);
                        RmarketAmt = Convert.ToString(jdBankFields["marketAmt"]);
                        RbigOrderDealDateTime = Convert.ToString(jdBankFields["bigOrderDealDateTime"]);
                        RinnerTransNo = Convert.ToString(jdBankFields["innerTransNo"]);
                    }

                    StringBuilder sbSign = new StringBuilder();
                    sbSign.Clear();
                    //sbSign.Append(RinterfaceId).Append(Rversion).Append(RserviceCode).Append(RreqDateTime).Append(RsignType).Append(jdBankFields.ToJson());
                    //sbSign.Append(jdBankFields.ToJson());
                    //sbSign.Append(RrespCode).Append(RrespMsg).Append(RinterfaceId).Append(Rversion).Append(RserviceCode).Append(RreqDateTime).Append(RsignType).Append(jdBankFields.ToJson());
                    sbSign.Append(RinterfaceId).Append(Rversion).Append(RserviceCode).Append(RreqDateTime).Append(RsignType);
                    sbSign.Append("{\"respCode\":\"").Append(RrespCode).Append("\"");
                    sbSign.Append(",\"respMsg\":\"").Append(RrespMsg).Append("\"");
                    sbSign.Append(",\"bigOrderDate\":\"").Append(RbigOrderDate).Append("\"");
                    sbSign.Append(",\"bigOrderStatus\":\"").Append(RbigOrderStatus).Append("\"");
                    sbSign.Append(",\"bigOrderNo\":\"").Append(RbigOrderNo).Append("\"");
                    sbSign.Append(",\"bigOrderReqNo\":\"").Append(RbigOrderReqNo).Append("\"");
                    sbSign.Append(",\"bigOrderAmt\":\"").Append(RbigOrderAmt).Append("\"");
                    sbSign.Append(",\"marketAmt\":\"").Append(RmarketAmt).Append("\"");
                    sbSign.Append(",\"bigOrderDealDateTime\":\"").Append(RbigOrderDealDateTime).Append("\"");
                    sbSign.Append(",\"innerTransNo\":\"").Append(RinnerTransNo).Append("\"}");
                    _log.Info("支付回调接口加密前报文：" + sbSign.ToString());

                    //解密
                    bool isPass = ValitedSignX(Rsign, sbSign.ToString(), pulickKeyLP);//pulickKeyL
                    _log.Info("支付回调接口浦发解密结果：" + isPass);

                }
            }
        }

        //订单查询
        public static void OrderQuery(RsaKeyParameters publicKeyParam, RsaPrivateCrtKeyParameters privateKeyParam, RsaKeyParameters pulickKeyLP)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Clear();
            sbData.Append("{\"serviceCode\":\"").Append("031001002107061").Append("\"");//一级商户代码
            sbData.Append(",\"bigOrderNo\":\"").Append("888820170316103500666668").Append("\"");//总订单号
            sbData.Append(",\"bigReqNo\":\"").Append("R888820170316103500666668").Append("\"");//总订单请求流水号
            sbData.Append("}");

            //获取验证签名
            string sSign = GetSignX(sbData.ToString(), privateKeyParam);

            //拼接Form并Post数据
            PostService ps = new PostService();
            ps.Add("data", sbData.ToString().Trim());
            ps.Add("sign", sSign);
            //ps.Url = "http://172.30.122.200:8099/paycashier/account/BFqueryTransDetail.do";
            ps.Url = "https://222.44.42.5/paycashier/account/queryOrderStatus.do";
            string result = ps.SendHttpPost();
            //ps.Post();
            //返回数据
            _log.Info("查询订单接口返回结果：" + result);

            //解密
            if (!string.IsNullOrEmpty(result.Trim()))
            {
                string[] resultR = result.Trim().Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
                if (resultR.Length == 2)
                {
                    string RResult = resultR[0];
                    string RSignParam = resultR[1];

                    JsonData jdP = JsonMapper.ToObject(RResult.Trim());
                    if (jdP.Count > 0)
                    {
                        string RrespCode = Convert.ToString(jdP["respCode"]);//响应码
                        string RrespMsg = Convert.ToString(jdP["respMsg"]);//响应描述
                        string RbigOrderDate = string.Empty;
                        string RbigOrderNo = string.Empty;
                        string RbigReqNo = string.Empty;
                        string RorderStatus = string.Empty;
                        string RbigOrderAmt = string.Empty;
                        string RbigPayAmt = string.Empty;
                        string RinnerTranSeq = string.Empty;
                        if (RrespCode == "0000")
                        {
                            RbigOrderDate = Convert.ToString(jdP["bigOrderDate"]);//总订单日期 格式：YYYYMMDD
                            RbigOrderNo = Convert.ToString(jdP["bigOrderNo"]);//总订单号唯一标识
                            RbigReqNo = Convert.ToString(jdP["bigReqNo"]);//总订单请求流水号唯一标识
                            RorderStatus = Convert.ToString(jdP["orderStatus"]);//订单状态 A请求 B成功 C请求 D待支付
                            RbigOrderAmt = Convert.ToString(jdP["bigOrderAmt"]);//总订单金额 单位：分
                            RbigPayAmt = Convert.ToString(jdP["bigPayAmt"]);//总支付金额 单位：分
                            RinnerTranSeq = Convert.ToString(jdP["innerTranSeq"]);//支付平台内部流水号
                        }
                        //解密
                        bool isPass = ValitedSignX(RSignParam.Trim().Split('=')[1], RResult, pulickKeyLP);//pulickKeyL
                        _log.Info("创建订单接口浦发解密结果：" + isPass);
                        if (isPass == true)
                        {
                            if (RrespCode == "0000")
                            {
                                if (RorderStatus.Trim() == "B")//支付成功
                                {

                                }
                                else
                                {
                                    //请求失败或待支付
                                }
                            }
                        }
                        else
                        {
                            //验签失败
                        }
                    }
                }
                else
                {
                    //接口返回异常
                }
                
            }
        }

        public static string GetSign(string jsonContent, RsaPrivateCrtKeyParameters pr)
        {
            // MD5计算
            //string SignMD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(jsonContent, "MD5").ToUpper();

            // SHA1计算
            string SignSHA1 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(jsonContent, "SHA1");

            // Rsa计算
            //Org.BouncyCastle.Math.BigInteger mod = new Org.BouncyCastle.Math.BigInteger(pr.Modulus.ToString(), 16);
            //Org.BouncyCastle.Math.BigInteger pubExp = new Org.BouncyCastle.Math.BigInteger(pr.PublicExponent.ToString(), 16);
            //Org.BouncyCastle.Math.BigInteger privExp = new Org.BouncyCastle.Math.BigInteger(pr.Exponent.ToString(), 16);
            //Org.BouncyCastle.Math.BigInteger p = new Org.BouncyCastle.Math.BigInteger(pr.P.ToString(), 16);
            //Org.BouncyCastle.Math.BigInteger q = new Org.BouncyCastle.Math.BigInteger(pr.Q.ToString(), 16);
            //Org.BouncyCastle.Math.BigInteger pExp = new Org.BouncyCastle.Math.BigInteger(pr.DP.ToString(), 16);
            //Org.BouncyCastle.Math.BigInteger qExp = new Org.BouncyCastle.Math.BigInteger(pr.DQ.ToString(), 16);
            //Org.BouncyCastle.Math.BigInteger crtCoef = new Org.BouncyCastle.Math.BigInteger(pr.QInv.ToString(), 16);

            //Org.BouncyCastle.Math.BigInteger mod = new Org.BouncyCastle.Math.BigInteger("kPHK4s9lbqwZ7KG1IzDdK1CcaYtM+AlSnw8Ev3yJm1uS2/9OUQlDrd4YDZ53w1NEuoam60KBBP7vmAznuF0kxeV0NluOp/vZGEX/SVJRKYPJjd2pTAOV804qrFzfLcZvJ9ftAyeBaKPjcyO2C3oMi7e1hAtmWXmsSrGxXr2zaI7OK8pEaAwixpAnOUFOmdfzWTwxpjuFZX0HBW8igGcWIGhiGzwfJKrDBP1YlpsimL8=", 16);
            //Org.BouncyCastle.Math.BigInteger pubExp = new Org.BouncyCastle.Math.BigInteger("Aw==", 16);
            //Org.BouncyCastle.Math.BigInteger privExp = new Org.BouncyCastle.Math.BigInteger("YKEx7IpDnx1mncEjbMs+HOBoRlzd+rDhv19Yf6hbvOe3PVTe4LDXyT66s776gjeDJwRvR4GrWKn1EAiaeujDLpj4JD0JxVKQutlU24w2G60xCT5w3Ve5S9W02EwvB6HfaY3Ysj58T9Me97x4B+tnYFShUKqQDDY5odbyBSkI5E2PRqYWbTZoY2AD4o3+sLxoFK68BCcyUhMhydtSUAiEZUdXDDV0wtGFNhv0rMCM62s=", 16);
            //Org.BouncyCastle.Math.BigInteger p = new Org.BouncyCastle.Math.BigInteger("9/ePNP2L6sZE0qjiruoJztJ0CaGEFZaeatKyPieBtiY8eGHV2wgQCK8QvzevMYQ8AwfNa5gFJ0alWY0OpPmkY/zulBvyU8ngGYPhoR5s06Nk5xPph2blrw==", 16);
            //Org.BouncyCastle.Math.BigInteger q = new Org.BouncyCastle.Math.BigInteger("laPYtZsWaNnEsH8VGtznGGKLf2B7g1rczfDYzWbFci+bduSBJR4CEcgxEesVCQH1fRmYALiLlhCU3IqRW8BF/FdoEgsWBoWoY9unStGTnNfO7FWp8uhR8Q==", 16);
            //Org.BouncyCastle.Math.BigInteger pExp = new Org.BouncyCastle.Math.BigInteger("pU+0zf5dRy7YjHCXH0axNIxNW8ECuQ8URzchfsUBJBl9pZaOkgVgBcoLKiUfdlgoAgUznRADb4RuO7NfGKZtl/30Yr1MN9vqu61BFhRIjReYmg1GWkSZHw==", 16);
            //Org.BouncyCastle.Math.BigInteger qExp = new Org.BouncyCastle.Math.BigInteger("Y8KQeRIO8JEtyv9jZz3vZZcHqkBSV5HoiUs7M5nY9spnpJhWGL6sC9rLYUdjW1ajqLu6qyWyZAsN6Fxg59WD/Y+atrIOrwPFl+fE3Iu3veU0nY5xTJrhSw==", 16);
            //Org.BouncyCastle.Math.BigInteger crtCoef = new Org.BouncyCastle.Math.BigInteger("W98uDNXHvFxd5BhykLLxoRcnMb3FTgPvw+NOC8S73PejPgmzNtzZ1Hwi9Kq+4KnVGYx2PTAas0DJDmO8v6Oo027UmmzNNdceUTJVsrnvZvOGLn2beBGmCQ==", 16);

            //Org.BouncyCastle.Math.BigInteger mod = new Org.BouncyCastle.Math.BigInteger("90f1cae2cf656eac19eca1b52330dd2b509c698b4cf809529f0f04bf7c899b5b92dbff4e510943adde180d9e77c35344ba86a6eb428104feef980ce7b85d24c5e574365b8ea7fbd91845ff4952512983c98ddda94c0395f34e2aac5cdf2dc66f27d7ed03278168a3e37323b60b7a0c8bb7b5840b665979ac4ab1b15ebdb3688ece2bca44680c22c6902739414e99d7f3593c31a63b85657d07056f228067162068621b3c1f24aac304fd58969b2298bf", 16);
            //Org.BouncyCastle.Math.BigInteger pubExp = new Org.BouncyCastle.Math.BigInteger("3", 16);
            //Org.BouncyCastle.Math.BigInteger privExp = new Org.BouncyCastle.Math.BigInteger("60a131ec8a439f1d669dc1236ccb3e1ce068465cddfab0e1bf5f587fa85bbce7b73d54dee0b0d7c93ebab3befa82378327046f4781ab58a9f510089a7ae8c32e98f8243d09c55290bad954db8c361bad31093e70dd57b94bd5b4d84c2f07a1df698dd8b23e7c4fd31ef7bc7807eb676054a150aa900c3639a1d6f2052908e44d8f46a6166d3668636003e28dfeb0bc6814aebc042732521321c9db525008846547570c3574c2d185361bf4acc08ceb6b", 16);
            //Org.BouncyCastle.Math.BigInteger p = new Org.BouncyCastle.Math.BigInteger("f7f78f34fd8beac644d2a8e2aeea09ced27409a18415969e6ad2b23e2781b6263c7861d5db081008af10bf37af31843c0307cd6b98052746a5598d0ea4f9a463fcee941bf253c9e01983e1a11e6cd3a364e713e98766e5af", 16);
            //Org.BouncyCastle.Math.BigInteger q = new Org.BouncyCastle.Math.BigInteger("95a3d8b59b1668d9c4b07f151adce718628b7f607b835adccdf0d8cd66c5722f9b76e481251e0211c83111eb150901f57d199800b88b961094dc8a915bc045fc5768120b160685a863dba74ad1939cd7ceec55a9f2e851f1", 16);
            //Org.BouncyCastle.Math.BigInteger pExp = new Org.BouncyCastle.Math.BigInteger("a54fb4cdfe5d472ed88c70971f46b1348c4d5bc102b90f144737217ec50124197da5968e92056005ca0b2a251f7658280205339d10036f846e3bb35f18a66d97fdf462bd4c37dbeabbad411614488d17989a0d465a44991f", 16);
            //Org.BouncyCastle.Math.BigInteger qExp = new Org.BouncyCastle.Math.BigInteger("63c29079120ef0912dcaff63673def659707aa40525791e8894b3b3399d8f6ca67a4985618beac0bdacb6147635b56a3a8bbbaab25b2640b0de85c60e7d583fd8f9ab6b20eaf03c597e7c4dc8bb7bde5349d8e714c9ae14b", 16);
            //Org.BouncyCastle.Math.BigInteger crtCoef = new Org.BouncyCastle.Math.BigInteger("5bdf2e0cd5c7bc5c5de4187290b2f1a1172731bdc54e03efc3e34e0bc4bbdcf7a33e09b336dcd9d47c22f4aabee0a9d5198c763d301ab340c90e63bcbfa3a8d36ed49a6ccd35d71e513255b2b9ef66f3862e7d9b7811a609", 16);

            //RsaKeyParameters pubParameters = new RsaKeyParameters(false, mod, pubExp);
            //RsaKeyParameters privParameters = new RsaPrivateCrtKeyParameters(mod, pubExp, privExp, p, q, pExp, qExp, crtCoef);

            RsaKeyParameters pubParameters = new RsaKeyParameters(false, pr.Modulus, pr.PublicExponent);
            RsaKeyParameters privParameters = new RsaPrivateCrtKeyParameters(pr.Modulus, pr.PublicExponent, pr.Exponent, pr.P, pr.Q, pr.DP, pr.DQ, pr.QInv);

            byte[] digInfo = System.Text.Encoding.Default.GetBytes(SignSHA1);

            ISigner rawSig = SignerUtilities.GetSigner("RSA");
            rawSig.Init(true, privParameters);
            rawSig.BlockUpdate(digInfo, 0, digInfo.Length);
            // Sign签名
            byte[] rawResult = rawSig.GenerateSignature();

            // 十六进制计算
            string SignRSA = byteToHexStr(rawResult).ToUpper();

            return SignRSA;
        }

        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        public static bool ValitedSign(string sign, string data, RsaKeyParameters pu)
        {
            try
            {
                if (string.IsNullOrEmpty(sign) || string.IsNullOrEmpty(data))
                    return false;

                //// Rsa计算
                //Org.BouncyCastle.Math.BigInteger mod = new Org.BouncyCastle.Math.BigInteger(pu.Modulus.ToString(), 16);
                //Org.BouncyCastle.Math.BigInteger pubExp = new Org.BouncyCastle.Math.BigInteger(pu.Exponent.ToString(), 16);
                //RsaKeyParameters pubParameters = new RsaKeyParameters(false, mod, pubExp);

                RsaKeyParameters pubParameters = new RsaKeyParameters(false, pu.Modulus, pu.Exponent);

                byte[] rawResult = Hex.Decode(sign);

                // MD5计算
                //string SignMD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(data, "MD5").ToUpper();

                // SHA1计算
                string SignSHA1 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(data, "SHA1");

                byte[] digInfo = System.Text.Encoding.Default.GetBytes(SignSHA1);

                // Rsa验签
                ISigner rawSig = SignerUtilities.GetSigner("RSA");
                rawSig.Init(false, pubParameters);
                rawSig.BlockUpdate(digInfo, 0, digInfo.Length);

                bool result = rawSig.VerifySignature(rawResult);

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string GetSignX(string jsonContent, RsaPrivateCrtKeyParameters pr)
        {
            // MD5计算
            string SignMD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(jsonContent, "MD5").ToUpper();

            // SHA1计算
            string SignSHA1 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(SignMD5, "SHA1");

            // Rsa计算
            //Org.BouncyCastle.Math.BigInteger mod = new Org.BouncyCastle.Math.BigInteger(pr.Modulus.ToString(), 16);
            //Org.BouncyCastle.Math.BigInteger pubExp = new Org.BouncyCastle.Math.BigInteger(pr.PublicExponent.ToString(), 16);
            //Org.BouncyCastle.Math.BigInteger privExp = new Org.BouncyCastle.Math.BigInteger(pr.Exponent.ToString(), 16);
            //Org.BouncyCastle.Math.BigInteger p = new Org.BouncyCastle.Math.BigInteger(pr.P.ToString(), 16);
            //Org.BouncyCastle.Math.BigInteger q = new Org.BouncyCastle.Math.BigInteger(pr.Q.ToString(), 16);
            //Org.BouncyCastle.Math.BigInteger pExp = new Org.BouncyCastle.Math.BigInteger(pr.DP.ToString(), 16);
            //Org.BouncyCastle.Math.BigInteger qExp = new Org.BouncyCastle.Math.BigInteger(pr.DQ.ToString(), 16);
            //Org.BouncyCastle.Math.BigInteger crtCoef = new Org.BouncyCastle.Math.BigInteger(pr.QInv.ToString(), 16);

            //Org.BouncyCastle.Math.BigInteger mod = new Org.BouncyCastle.Math.BigInteger("kPHK4s9lbqwZ7KG1IzDdK1CcaYtM+AlSnw8Ev3yJm1uS2/9OUQlDrd4YDZ53w1NEuoam60KBBP7vmAznuF0kxeV0NluOp/vZGEX/SVJRKYPJjd2pTAOV804qrFzfLcZvJ9ftAyeBaKPjcyO2C3oMi7e1hAtmWXmsSrGxXr2zaI7OK8pEaAwixpAnOUFOmdfzWTwxpjuFZX0HBW8igGcWIGhiGzwfJKrDBP1YlpsimL8=", 16);
            //Org.BouncyCastle.Math.BigInteger pubExp = new Org.BouncyCastle.Math.BigInteger("Aw==", 16);
            //Org.BouncyCastle.Math.BigInteger privExp = new Org.BouncyCastle.Math.BigInteger("YKEx7IpDnx1mncEjbMs+HOBoRlzd+rDhv19Yf6hbvOe3PVTe4LDXyT66s776gjeDJwRvR4GrWKn1EAiaeujDLpj4JD0JxVKQutlU24w2G60xCT5w3Ve5S9W02EwvB6HfaY3Ysj58T9Me97x4B+tnYFShUKqQDDY5odbyBSkI5E2PRqYWbTZoY2AD4o3+sLxoFK68BCcyUhMhydtSUAiEZUdXDDV0wtGFNhv0rMCM62s=", 16);
            //Org.BouncyCastle.Math.BigInteger p = new Org.BouncyCastle.Math.BigInteger("9/ePNP2L6sZE0qjiruoJztJ0CaGEFZaeatKyPieBtiY8eGHV2wgQCK8QvzevMYQ8AwfNa5gFJ0alWY0OpPmkY/zulBvyU8ngGYPhoR5s06Nk5xPph2blrw==", 16);
            //Org.BouncyCastle.Math.BigInteger q = new Org.BouncyCastle.Math.BigInteger("laPYtZsWaNnEsH8VGtznGGKLf2B7g1rczfDYzWbFci+bduSBJR4CEcgxEesVCQH1fRmYALiLlhCU3IqRW8BF/FdoEgsWBoWoY9unStGTnNfO7FWp8uhR8Q==", 16);
            //Org.BouncyCastle.Math.BigInteger pExp = new Org.BouncyCastle.Math.BigInteger("pU+0zf5dRy7YjHCXH0axNIxNW8ECuQ8URzchfsUBJBl9pZaOkgVgBcoLKiUfdlgoAgUznRADb4RuO7NfGKZtl/30Yr1MN9vqu61BFhRIjReYmg1GWkSZHw==", 16);
            //Org.BouncyCastle.Math.BigInteger qExp = new Org.BouncyCastle.Math.BigInteger("Y8KQeRIO8JEtyv9jZz3vZZcHqkBSV5HoiUs7M5nY9spnpJhWGL6sC9rLYUdjW1ajqLu6qyWyZAsN6Fxg59WD/Y+atrIOrwPFl+fE3Iu3veU0nY5xTJrhSw==", 16);
            //Org.BouncyCastle.Math.BigInteger crtCoef = new Org.BouncyCastle.Math.BigInteger("W98uDNXHvFxd5BhykLLxoRcnMb3FTgPvw+NOC8S73PejPgmzNtzZ1Hwi9Kq+4KnVGYx2PTAas0DJDmO8v6Oo027UmmzNNdceUTJVsrnvZvOGLn2beBGmCQ==", 16);

            //Org.BouncyCastle.Math.BigInteger mod = new Org.BouncyCastle.Math.BigInteger("90f1cae2cf656eac19eca1b52330dd2b509c698b4cf809529f0f04bf7c899b5b92dbff4e510943adde180d9e77c35344ba86a6eb428104feef980ce7b85d24c5e574365b8ea7fbd91845ff4952512983c98ddda94c0395f34e2aac5cdf2dc66f27d7ed03278168a3e37323b60b7a0c8bb7b5840b665979ac4ab1b15ebdb3688ece2bca44680c22c6902739414e99d7f3593c31a63b85657d07056f228067162068621b3c1f24aac304fd58969b2298bf", 16);
            //Org.BouncyCastle.Math.BigInteger pubExp = new Org.BouncyCastle.Math.BigInteger("3", 16);
            //Org.BouncyCastle.Math.BigInteger privExp = new Org.BouncyCastle.Math.BigInteger("60a131ec8a439f1d669dc1236ccb3e1ce068465cddfab0e1bf5f587fa85bbce7b73d54dee0b0d7c93ebab3befa82378327046f4781ab58a9f510089a7ae8c32e98f8243d09c55290bad954db8c361bad31093e70dd57b94bd5b4d84c2f07a1df698dd8b23e7c4fd31ef7bc7807eb676054a150aa900c3639a1d6f2052908e44d8f46a6166d3668636003e28dfeb0bc6814aebc042732521321c9db525008846547570c3574c2d185361bf4acc08ceb6b", 16);
            //Org.BouncyCastle.Math.BigInteger p = new Org.BouncyCastle.Math.BigInteger("f7f78f34fd8beac644d2a8e2aeea09ced27409a18415969e6ad2b23e2781b6263c7861d5db081008af10bf37af31843c0307cd6b98052746a5598d0ea4f9a463fcee941bf253c9e01983e1a11e6cd3a364e713e98766e5af", 16);
            //Org.BouncyCastle.Math.BigInteger q = new Org.BouncyCastle.Math.BigInteger("95a3d8b59b1668d9c4b07f151adce718628b7f607b835adccdf0d8cd66c5722f9b76e481251e0211c83111eb150901f57d199800b88b961094dc8a915bc045fc5768120b160685a863dba74ad1939cd7ceec55a9f2e851f1", 16);
            //Org.BouncyCastle.Math.BigInteger pExp = new Org.BouncyCastle.Math.BigInteger("a54fb4cdfe5d472ed88c70971f46b1348c4d5bc102b90f144737217ec50124197da5968e92056005ca0b2a251f7658280205339d10036f846e3bb35f18a66d97fdf462bd4c37dbeabbad411614488d17989a0d465a44991f", 16);
            //Org.BouncyCastle.Math.BigInteger qExp = new Org.BouncyCastle.Math.BigInteger("63c29079120ef0912dcaff63673def659707aa40525791e8894b3b3399d8f6ca67a4985618beac0bdacb6147635b56a3a8bbbaab25b2640b0de85c60e7d583fd8f9ab6b20eaf03c597e7c4dc8bb7bde5349d8e714c9ae14b", 16);
            //Org.BouncyCastle.Math.BigInteger crtCoef = new Org.BouncyCastle.Math.BigInteger("5bdf2e0cd5c7bc5c5de4187290b2f1a1172731bdc54e03efc3e34e0bc4bbdcf7a33e09b336dcd9d47c22f4aabee0a9d5198c763d301ab340c90e63bcbfa3a8d36ed49a6ccd35d71e513255b2b9ef66f3862e7d9b7811a609", 16);

            //RsaKeyParameters pubParameters = new RsaKeyParameters(false, mod, pubExp);
            //RsaKeyParameters privParameters = new RsaPrivateCrtKeyParameters(mod, pubExp, privExp, p, q, pExp, qExp, crtCoef);

            RsaKeyParameters pubParameters = new RsaKeyParameters(false, pr.Modulus, pr.PublicExponent);
            RsaKeyParameters privParameters = new RsaPrivateCrtKeyParameters(pr.Modulus, pr.PublicExponent, pr.Exponent, pr.P, pr.Q, pr.DP, pr.DQ, pr.QInv);

            byte[] digInfo = System.Text.Encoding.Default.GetBytes(SignSHA1);

            ISigner rawSig = SignerUtilities.GetSigner("RSA");
            rawSig.Init(true, privParameters);
            rawSig.BlockUpdate(digInfo, 0, digInfo.Length);
            // Sign签名
            byte[] rawResult = rawSig.GenerateSignature();

            // 十六进制计算
            string SignRSA = byteToHexStr(rawResult).ToUpper();

            return SignRSA;
        }

        public static bool ValitedSignX(string sign, string data, RsaKeyParameters pu)
        {
            try
            {
                if (string.IsNullOrEmpty(sign) || string.IsNullOrEmpty(data))
                    return false;

                //// Rsa计算
                //Org.BouncyCastle.Math.BigInteger mod = new Org.BouncyCastle.Math.BigInteger(pu.Modulus.ToString(), 16);
                //Org.BouncyCastle.Math.BigInteger pubExp = new Org.BouncyCastle.Math.BigInteger(pu.Exponent.ToString(), 16);
                //RsaKeyParameters pubParameters = new RsaKeyParameters(false, mod, pubExp);

                RsaKeyParameters pubParameters = new RsaKeyParameters(false, pu.Modulus, pu.Exponent);

                byte[] rawResult = Hex.Decode(sign);

                // MD5计算
                string SignMD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(data, "MD5").ToUpper();

                // SHA1计算
                string SignSHA1 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(SignMD5, "SHA1");

                byte[] digInfo = System.Text.Encoding.Default.GetBytes(SignSHA1);

                // Rsa验签
                ISigner rawSig = SignerUtilities.GetSigner("RSA");
                rawSig.Init(false, pubParameters);
                rawSig.BlockUpdate(digInfo, 0, digInfo.Length);

                bool result = rawSig.VerifySignature(rawResult);

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool ValitedSignY(string sign, string data, RsaKeyParameters pu)
        {
            try
            {
                if (string.IsNullOrEmpty(sign) || string.IsNullOrEmpty(data))
                    return false;

                //// Rsa计算
                //Org.BouncyCastle.Math.BigInteger mod = new Org.BouncyCastle.Math.BigInteger(pu.Modulus.ToString(), 16);
                //Org.BouncyCastle.Math.BigInteger pubExp = new Org.BouncyCastle.Math.BigInteger(pu.Exponent.ToString(), 16);
                //RsaKeyParameters pubParameters = new RsaKeyParameters(false, mod, pubExp);

                RsaKeyParameters pubParameters = new RsaKeyParameters(false, pu.Modulus, pu.Exponent);

                byte[] rawResult = Hex.Decode(sign);

                // MD5计算
                string SignMD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(data, "MD5");

                // SHA1计算
                string SignSHA1 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(SignMD5, "SHA1");

                byte[] digInfo = System.Text.Encoding.Default.GetBytes(SignSHA1);

                // Rsa验签
                ISigner rawSig = SignerUtilities.GetSigner("RSA");
                rawSig.Init(false, pubParameters);
                rawSig.BlockUpdate(digInfo, 0, digInfo.Length);

                bool result = rawSig.VerifySignature(rawResult);

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// RSA密钥格式转换
        /// </summary>public class RSAKeyConvert{    
        /// <summary>    
        /// RSA私钥格式转换，java->.net    
        /// </summary>    
        /// <param name="privateKey">java生成的RSA私钥</param>    
        /// <returns></returns>    
        public static string RSAPrivateKeyJava2DotNet(string privateKey)
        {
            RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));
            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
            Convert.ToBase64String(privateKeyParam.Modulus.ToByteArrayUnsigned()),
            Convert.ToBase64String(privateKeyParam.PublicExponent.ToByteArrayUnsigned()),
            Convert.ToBase64String(privateKeyParam.P.ToByteArrayUnsigned()),
            Convert.ToBase64String(privateKeyParam.Q.ToByteArrayUnsigned()),
            Convert.ToBase64String(privateKeyParam.DP.ToByteArrayUnsigned()),
            Convert.ToBase64String(privateKeyParam.DQ.ToByteArrayUnsigned()),
            Convert.ToBase64String(privateKeyParam.QInv.ToByteArrayUnsigned()),
            Convert.ToBase64String(privateKeyParam.Exponent.ToByteArrayUnsigned()));
        }

        /// <summary>    
        /// RSA私钥格式转换，.net->java    
        /// </summary>    
        /// <param name="privateKey">.net生成的私钥</param>    
        /// <returns></returns>    
        public static string RSAPrivateKeyDotNet2Java(string privateKey)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(privateKey);
            BigInteger m = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Modulus")[0].InnerText));
            BigInteger exp = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Exponent")[0].InnerText));
            BigInteger d = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("D")[0].InnerText));
            BigInteger p = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("P")[0].InnerText));
            BigInteger q = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Q")[0].InnerText));
            BigInteger dp = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("DP")[0].InnerText));
            BigInteger dq = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("DQ")[0].InnerText));
            BigInteger qinv = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("InverseQ")[0].InnerText));
            RsaPrivateCrtKeyParameters privateKeyParam = new RsaPrivateCrtKeyParameters(m, exp, d, p, q, dp, dq, qinv);
            PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKeyParam);
            byte[] serializedPrivateBytes = privateKeyInfo.ToAsn1Object().GetEncoded();
            return Convert.ToBase64String(serializedPrivateBytes);
        }

        /// <summary>    
        /// RSA公钥格式转换，java->.net    
        /// </summary>    
        /// <param name="publicKey">java生成的公钥</param>    
        /// <returns></returns>    
        public static string RSAPublicKeyJava2DotNet(string publicKey)
        {
            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
            Convert.ToBase64String(publicKeyParam.Modulus.ToByteArrayUnsigned()),
            Convert.ToBase64String(publicKeyParam.Exponent.ToByteArrayUnsigned()));
        }

        /// <summary>    
        /// RSA公钥格式转换，.net->java    
        /// </summary>    
        /// <param name="publicKey">.net生成的公钥</param>    
        /// <returns></returns>    
        public static string RSAPublicKeyDotNet2Java(string publicKey)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(publicKey);
            BigInteger m = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Modulus")[0].InnerText));
            BigInteger p = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Exponent")[0].InnerText));
            RsaKeyParameters pub = new RsaKeyParameters(false, m, p);
            SubjectPublicKeyInfo publicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pub);
            byte[] serializedPublicBytes = publicKeyInfo.ToAsn1Object().GetDerEncoded();
            return Convert.ToBase64String(serializedPublicBytes);
        }


        /// <summary>
        /// 计算签名
        /// </summary>
        /// <param name="jsonContent"></param>
        /// <returns></returns>
        private static string GetSignY(string jsonContent, RsaPrivateCrtKeyParameters pr)
        {
            // MD5计算
            string SignMD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(jsonContent, "MD5").ToUpper();

            // SHA1计算
            string SignSHA1 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(SignMD5, "SHA1");

            // Rsa计算
            Org.BouncyCastle.Math.BigInteger mod = new Org.BouncyCastle.Math.BigInteger(pr.Modulus.ToString(), 16);
            Org.BouncyCastle.Math.BigInteger pubExp = new Org.BouncyCastle.Math.BigInteger(pr.PublicExponent.ToString(), 16);
            Org.BouncyCastle.Math.BigInteger privExp = new Org.BouncyCastle.Math.BigInteger(pr.Exponent.ToString(), 16);
            Org.BouncyCastle.Math.BigInteger p = new Org.BouncyCastle.Math.BigInteger(pr.P.ToString(), 16);
            Org.BouncyCastle.Math.BigInteger q = new Org.BouncyCastle.Math.BigInteger(pr.Q.ToString(), 16);
            Org.BouncyCastle.Math.BigInteger pExp = new Org.BouncyCastle.Math.BigInteger(pr.DP.ToString(), 16);
            Org.BouncyCastle.Math.BigInteger qExp = new Org.BouncyCastle.Math.BigInteger(pr.DQ.ToString(), 16);
            Org.BouncyCastle.Math.BigInteger crtCoef = new Org.BouncyCastle.Math.BigInteger(pr.QInv.ToString(), 16);

            RsaKeyParameters pubParameters = new RsaKeyParameters(false, mod, pubExp);
            RsaKeyParameters privParameters = new RsaPrivateCrtKeyParameters(mod, pubExp, privExp, p, q, pExp, qExp, crtCoef);

            byte[] digInfo = System.Text.Encoding.Default.GetBytes(SignSHA1);

            ISigner rawSig = SignerUtilities.GetSigner("RSA");
            rawSig.Init(true, privParameters);
            rawSig.BlockUpdate(digInfo, 0, digInfo.Length);
            // Sign签名
            byte[] rawResult = rawSig.GenerateSignature();

            // 十六进制计算
            string SignRSA = byteToHexStrY(rawResult).ToUpper();

            return SignRSA;
        }

        #region 字节数组转16进制字符串
        /// <summary> 
        /// 字节数组转16进制字符串 
        /// </summary> 
        /// <param name="bytes"></param> 
        /// <returns></returns> 
        public static string byteToHexStrY(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }
        #endregion
    }
}