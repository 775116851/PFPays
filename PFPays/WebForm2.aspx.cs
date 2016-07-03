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

            //string k = RSAPrivateKeyJava2DotNet(pr);
            //string n = RSAPublicKeyJava2DotNet(pu);

            RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(pr));
            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(pu));
            
            //string s = string.Format("{0:X}", Convert.ToBase64String(lpu, 16));
            //byte[] m = Convert.FromBase64String(lpu);
            //string k = Convert.ToBase64String(m);
            //string str = System.Text.Encoding.Default.GetString(m);
            //string k = System.Numerics.BigInteger.Parse(pu).ToString("x");

            byte[] pu8 = Convert.FromBase64String(pu);

            //byte[] rB = new byte[pu.Length / 2];
            //for (int i = 0; i < rB.Length; i++)
            //    rB[i] = Convert.ToByte(pu.Substring(i * 2, 2), 64);

            //string ppu = "EEE81E52267060718526F25F035175E924434A465DA4E9558E5A67C1168137193D9087FC83E21F39CEE713D3E3BF994529B962486E815203575C46802CAD7D7F2B2838A754DE4CCF4E49683AC521FDBC03CA333C9DB4B0336355F0423D871DA337F1F5784BF2AB51F59E4DB0733FB653FF02AD65A590D070AA7D1AC3EF7E516D8114537C87887236DC1F821B7469C330E373C331655EF245524CAB4B3F798A7E9671AE91B35746FA5377EEFFEF341C93";
            byte[] returnBytes = new byte[lpu.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(lpu.Substring(i * 2, 2), 16);

            return;
            //加密
            string mSign = GetSign(jsonContent, privateKeyParam);
            //解密
            bool isPass = ValitedSign(mSign, jsonContent, publicKeyParam);
        }

        public static string GetSign(string jsonContent, RsaPrivateCrtKeyParameters pr)
        {
            // MD5计算
            string SignMD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(jsonContent, "MD5").ToUpper();

            // SHA1计算
            string SignSHA1 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(SignMD5, "SHA1");

            // Rsa计算
            //Org.BouncyCastle.Math.BigInteger mod = new Org.BouncyCastle.Math.BigInteger(sMod, 16);
            //Org.BouncyCastle.Math.BigInteger pubExp = new Org.BouncyCastle.Math.BigInteger(sPubExp, 16);
            //Org.BouncyCastle.Math.BigInteger privExp = new Org.BouncyCastle.Math.BigInteger(sPrivExp, 16);
            //Org.BouncyCastle.Math.BigInteger p = new Org.BouncyCastle.Math.BigInteger(sP, 16);
            //Org.BouncyCastle.Math.BigInteger q = new Org.BouncyCastle.Math.BigInteger(sQ, 16);
            //Org.BouncyCastle.Math.BigInteger pExp = new Org.BouncyCastle.Math.BigInteger(sPExp, 16);
            //Org.BouncyCastle.Math.BigInteger qExp = new Org.BouncyCastle.Math.BigInteger(sQExp, 16);
            //Org.BouncyCastle.Math.BigInteger crtCoef = new Org.BouncyCastle.Math.BigInteger(sCrtCoef, 16);

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
                //Org.BouncyCastle.Math.BigInteger mod = new Org.BouncyCastle.Math.BigInteger("", 16);
                //Org.BouncyCastle.Math.BigInteger pubExp = new Org.BouncyCastle.Math.BigInteger("", 16);
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