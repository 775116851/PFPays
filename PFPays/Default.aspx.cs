//using Org.BouncyCastle.Crypto.Generators;
//using Org.BouncyCastle.Crypto.Parameters;
//using Org.BouncyCastle.Crypto;
//using Org.BouncyCastle.Security;
//using Org.BouncyCastle.Crypto.Engines;  //IAsymmetricBlockCipher engine = new RsaEngine(); 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using System.Security.Cryptography.X509Certificates;

namespace PFPays
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int a = 1;
            //X509Certificate2 pc = new X509Certificate2(@"D:/182000899000001_01.pfx", "123456", X509KeyStorageFlags.MachineKeySet);
            ////return BigNum.ToDecimalStr(BigNum.ConvertFromHex(pc.SerialNumber)); 低于4.0版本的.NET请使用此方法
            ////return BigInteger.Parse(pc.SerialNumber, System.Globalization.NumberStyles.HexNumber).ToString();
            //Org.BouncyCastle.Math.BigInteger mod = new Org.BouncyCastle.Math.BigInteger(pc.GetSerialNumberString(), 16);
            string m = "123333333333333333333333333333333333333333333333333333333333333333333333333333333333333333344444444444444444444444444FFFFFFFFFFFFGGGGGGGGGGGGGGGGGGGGGGGGGGGGG";
            string sign = Cmn.GetSign(m);
            bool f = Cmn.ValitedSign(sign, m);
            int a = 0;

            //RSA密钥对的构造器 
            RsaKeyPairGenerator keyGenerator = new RsaKeyPairGenerator();
            //RSA密钥构造器的参数 
            RsaKeyGenerationParameters param = new RsaKeyGenerationParameters(
                Org.BouncyCastle.Math.BigInteger.ValueOf(3),
                new Org.BouncyCastle.Security.SecureRandom(),
                1024,   //密钥长度 
                25);
            //用参数初始化密钥构造器 
            keyGenerator.Init(param);
            //产生密钥对 
            AsymmetricCipherKeyPair keyPair = keyGenerator.GenerateKeyPair();
            //获取公钥和密钥 
            AsymmetricKeyParameter publicKey = keyPair.Public;
            AsymmetricKeyParameter privateKey = keyPair.Private;
            if (((RsaKeyParameters)publicKey).Modulus.BitLength < 1024)
            {
                Console.WriteLine("failed key generation (1024) length test");
            }
            //一个测试…………………… 
            //输入，十六进制的字符串，解码为byte[] 
            //string input = "4e6f77206973207468652074696d6520666f7220616c6c20676f6f64206d656e"; 
            //byte[] testData = Org.BouncyCastle.Utilities.Encoders.Hex.Decode(input);  
            string input = "popozh RSA test";
            byte[] testData = Encoding.UTF8.GetBytes(input);
            Console.WriteLine("明文:" + input + Environment.NewLine);
            //非对称加密算法，加解密用 
            IAsymmetricBlockCipher engine = new RsaEngine();
            //公钥加密 
            engine.Init(true, publicKey);
            try
            {
                testData = engine.ProcessBlock(testData, 0, testData.Length);
                Console.WriteLine("密文（base64编码）:" + Convert.ToBase64String(testData) + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("failed - exception " + Environment.NewLine + ex.ToString());
            }
            //string K = Convert.ToBase64String(testData);//System.Text.Encoding.Default.GetString(testData);
            //string sign = Cmn.GetSign(K);
            //int m = 1;
            //私钥解密 
            engine.Init(false, privateKey);
            try
            {
                testData = engine.ProcessBlock(testData, 0, testData.Length);

            }
            catch (Exception ef)
            {
                Console.WriteLine("failed - exception " + ef.ToString());
            }
            if (input.Equals(Encoding.UTF8.GetString(testData)))
            {
                Console.WriteLine("解密成功");
            }
            Console.Read(); 
        }
    }

}