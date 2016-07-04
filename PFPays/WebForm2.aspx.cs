using System;
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
using System.Text;

namespace PFPays
{
    //http://www.java123.net/994241-1.html
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string jsonContent = "测试1数V5据X";
            string pu = "MIHNMA0GCSqGSIb3DQEBAQUAA4G7ADCBtwKBsQCQ8criz2VurBnsobUjMN0rUJxpi0z4CVKfDwS/fImbW5Lb/05RCUOt3hgNnnfDU0S6hqbrQoEE/u+YDOe4XSTF5XQ2W46n+9kYRf9JUlEpg8mN3alMA5XzTiqsXN8txm8n1+0DJ4Foo+NzI7YLegyLt7WEC2ZZeaxKsbFevbNojs4rykRoDCLGkCc5QU6Z1/NZPDGmO4VlfQcFbyKAZxYgaGIbPB8kqsME/ViWmyKYvwIBAw==";
            string pr = "MIIDTAIBADANBgkqhkiG9w0BAQEFAASCAzYwggMyAgEAAoGxAJDxyuLPZW6sGeyhtSMw3StQnGmLTPgJUp8PBL98iZtbktv/TlEJQ63eGA2ed8NTRLqGputCgQT+75gM57hdJMXldDZbjqf72RhF/0lSUSmDyY3dqUwDlfNOKqxc3y3GbyfX7QMngWij43Mjtgt6DIu3tYQLZll5rEqxsV69s2iOzivKRGgMIsaQJzlBTpnX81k8MaY7hWV9BwVvIoBnFiBoYhs8HySqwwT9WJabIpi/AgEDAoGwYKEx7IpDnx1mncEjbMs+HOBoRlzd+rDhv19Yf6hbvOe3PVTe4LDXyT66s776gjeDJwRvR4GrWKn1EAiaeujDLpj4JD0JxVKQutlU24w2G60xCT5w3Ve5S9W02EwvB6HfaY3Ysj58T9Me97x4B+tnYFShUKqQDDY5odbyBSkI5E2PRqYWbTZoY2AD4o3+sLxoFK68BCcyUhMhydtSUAiEZUdXDDV0wtGFNhv0rMCM62sCWQD39480/YvqxkTSqOKu6gnO0nQJoYQVlp5q0rI+J4G2Jjx4YdXbCBAIrxC/N68xhDwDB81rmAUnRqVZjQ6k+aRj/O6UG/JTyeAZg+GhHmzTo2TnE+mHZuWvAlkAlaPYtZsWaNnEsH8VGtznGGKLf2B7g1rczfDYzWbFci+bduSBJR4CEcgxEesVCQH1fRmYALiLlhCU3IqRW8BF/FdoEgsWBoWoY9unStGTnNfO7FWp8uhR8QJZAKVPtM3+XUcu2Ixwlx9GsTSMTVvBArkPFEc3IX7FASQZfaWWjpIFYAXKCyolH3ZYKAIFM50QA2+EbjuzXximbZf99GK9TDfb6rutQRYUSI0XmJoNRlpEmR8CWGPCkHkSDvCRLcr/Y2c972WXB6pAUleR6IlLOzOZ2PbKZ6SYVhi+rAvay2FHY1tWo6i7uqslsmQLDehcYOfVg/2PmrayDq8DxZfnxNyLt73lNJ2OcUya4UsCWFvfLgzVx7xcXeQYcpCy8aEXJzG9xU4D78PjTgvEu9z3oz4Jszbc2dR8IvSqvuCp1RmMdj0wGrNAyQ5jvL+jqNNu1JpszTXXHlEyVbK572bzhi59m3gRpgk=";
            string lpu = "90F1CAE2CF656EAC19ECA1B52330DD2B509C698B4CF809529F0F04BF7C899B5B92DBFF4E510943ADDE180D9E77C35344BA86A6EB428104FEEF980CE7B85D24C5E574365B8EA7FBD91845FF4952512983C98DDDA94C0395F34E2AAC5CDF2DC66F27D7ED03278168A3E37323B60B7A0C8BB7B5840B665979AC4AB1B15EBDB3688ECE2BCA44680C22C6902739414E99D7F3593C31A63B85657D07056F228067162068621B3C1F24AAC304FD58969B2298BF";
            string lpuZ = "EEE81E52267060718526F25F035175E924434A465DA4E9558E5A67C1168137193D9087FC83E21F39CEE713D3E3BF994529B962486E815203575C46802CAD7D7F2B2838A754DE4CCF4E49683AC521FDBC03CA333C9DB4B0336355F0423D871DA337F1F5784BF2AB51F59E4DB0733FB653FF02AD65A590D070AA7D1AC3EF7E516D8114537C87887236DC1F821B7469C330E373C331655EF245524CAB4B3F798A7E9671AE91B35746FA5377EEFFEF341C93";

            //string k = RSAPrivateKeyJava2DotNet(pr);
            //string n = RSAPublicKeyJava2DotNet(pu);

            RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(pr));
            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(pu));

            //裸公钥
            BigInteger mod1 = new BigInteger(lpu, 16);
            BigInteger pub1 = new BigInteger("03", 16);
            RsaKeyParameters pulickKeyL = new RsaKeyParameters(false, mod1, pub1);

            //byte[] pu8 = Convert.FromBase64String(pu);

            //byte[] returnBytes = new byte[lpu.Length / 2];
            //for (int i = 0; i < returnBytes.Length; i++)
            //{
            //    returnBytes[i] = Convert.ToByte(lpu.Substring(i * 2, 2), 16);
            //}

            StringBuilder sbData = new StringBuilder();
            sbData.Clear();
            sbData.Append("{'serviceCode':'").Append("031001002107061").Append("'");//一级商户代码
            sbData.Append(",'bigOrderNo':'").Append("2016062900000001").Append("'");//总订单号
            sbData.Append(",'bigReqNo':'").Append("2016062900000001").Append("'");//总订单请求流水号
            sbData.Append("}");

            PostService ps = new PostService();
            jsonContent = sbData.ToString().Trim();

            //加密
            string mSign = GetSign(jsonContent, privateKeyParam);
            ps.Add("data", jsonContent);
            ps.Add("sign", mSign);
            ps.Url = "https://222.44.42.5/paycashier/account/queryOrderStatus.do";
            ps.Post();
            //string result = ps.SendHttpPost();
            //解密
            bool isPass = ValitedSign(mSign, jsonContent, pulickKeyL);//publicKeyParam
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


        

    }
}