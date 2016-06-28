using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace PFPays
{
    public class Cmn
    {
        #region 签名

        /// <summary>
        /// 计算签名
        /// </summary>
        /// <param name="jsonContent"></param>
        /// <returns></returns>
        public static string GetSign(string jsonContent)
        {
            // MD5计算
            string SignMD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(jsonContent, "MD5").ToUpper();

            // SHA1计算
            string SignSHA1 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(SignMD5, "SHA1");

            X509Certificate2 pc = new X509Certificate2(@"D:/182000899000001_01.pfx", "123456", X509KeyStorageFlags.MachineKeySet);
            string sTR = pc.GetSerialNumberString();
            // Rsa计算
            Org.BouncyCastle.Math.BigInteger mod = new Org.BouncyCastle.Math.BigInteger(sTR, 16);
            Org.BouncyCastle.Math.BigInteger pubExp = new Org.BouncyCastle.Math.BigInteger(sTR, 16);
            Org.BouncyCastle.Math.BigInteger privExp = new Org.BouncyCastle.Math.BigInteger(sTR, 16);
            Org.BouncyCastle.Math.BigInteger p = new Org.BouncyCastle.Math.BigInteger(sTR, 16);
            Org.BouncyCastle.Math.BigInteger q = new Org.BouncyCastle.Math.BigInteger(sTR, 16);
            Org.BouncyCastle.Math.BigInteger pExp = new Org.BouncyCastle.Math.BigInteger(sTR, 16);
            Org.BouncyCastle.Math.BigInteger qExp = new Org.BouncyCastle.Math.BigInteger(sTR, 16);
            Org.BouncyCastle.Math.BigInteger crtCoef = new Org.BouncyCastle.Math.BigInteger(sTR, 16);

            RsaKeyParameters pubParameters = new RsaKeyParameters(false, mod, pubExp);
            RsaKeyParameters privParameters = new RsaPrivateCrtKeyParameters(mod, pubExp, privExp, p, q, pExp, qExp, crtCoef);

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

        #region 字节数组转16进制字符串
        /// <summary> 
        /// 字节数组转16进制字符串 
        /// </summary> 
        /// <param name="bytes"></param> 
        /// <returns></returns> 
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
        #endregion

        #endregion

        #region 验签

        /// <summary>
        /// Rsa验签
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool ValitedSign(string sign, string data)
        {
            try
            {
                if (string.IsNullOrEmpty(sign) || string.IsNullOrEmpty(data))
                    return false;

                // Rsa计算
                Org.BouncyCastle.Math.BigInteger mod = new Org.BouncyCastle.Math.BigInteger("", 16);
                Org.BouncyCastle.Math.BigInteger pubExp = new Org.BouncyCastle.Math.BigInteger("", 16);
                RsaKeyParameters pubParameters = new RsaKeyParameters(false, mod, pubExp);

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

        #endregion
    }
}