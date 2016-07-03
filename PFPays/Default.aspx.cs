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
using System.Collections.Specialized;
using log4net;
using LitJson;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.Math;

namespace PFPays
{
    public partial class Default : System.Web.UI.Page
    {
        private static readonly ILog _log = log4net.LogManager.GetLogger(typeof(Default));
        protected void Page_Load(object sender, EventArgs e)
        {
            _log.Info("测试");
            //X509Certificate2 pc = new X509Certificate2(@"D:/182000899000001_01.pfx", "123456", X509KeyStorageFlags.MachineKeySet);
            ////return BigNum.ToDecimalStr(BigNum.ConvertFromHex(pc.SerialNumber)); 低于4.0版本的.NET请使用此方法
            ////return BigInteger.Parse(pc.SerialNumber, System.Globalization.NumberStyles.HexNumber).ToString();
            //Org.BouncyCastle.Math.BigInteger mod = new Org.BouncyCastle.Math.BigInteger(pc.GetSerialNumberString(), 16);
            //string m = "123333333333333333333333333333333333333333333333333333333333333333333333333333333333333333344444444444444444444444444FFFFFFFFFFFFGGGGGGGGGGGGGGGGGGGGGGGGGGGGG";
            //string sign = Cmn.GetSign(m);
            //bool f = Cmn.ValitedSign(sign, m);
            //int a = 0;

            //Org.BouncyCastle.Crypto.Parameters.RsaPrivateCrtKeyParameters)privateKey


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
            string input = "popozh RSA test";
            AsymmetricCipherKeyPair keyPair = keyGenerator.GenerateKeyPair();
            //获取公钥和密钥 
            AsymmetricKeyParameter publicKey = keyPair.Public;
            AsymmetricKeyParameter privateKey = keyPair.Private;
            if (((RsaKeyParameters)publicKey).Modulus.BitLength < 1024)
            {
                Console.WriteLine("failed key generation (1024) length test");
            }
            Org.BouncyCastle.Crypto.Parameters.RsaKeyParameters pu = (Org.BouncyCastle.Crypto.Parameters.RsaKeyParameters)publicKey;
            Org.BouncyCastle.Crypto.Parameters.RsaPrivateCrtKeyParameters pr = (Org.BouncyCastle.Crypto.Parameters.RsaPrivateCrtKeyParameters)privateKey;

            //加密
            string mSign = GetSign(input, pr);
            //解密
            bool isPass = ValitedSign(mSign, input, pu);
            return;
            //一个测试…………………… 
            //输入，十六进制的字符串，解码为byte[] 
            //string input = "4e6f77206973207468652074696d6520666f7220616c6c20676f6f64206d656e"; 
            //byte[] testData = Org.BouncyCastle.Utilities.Encoders.Hex.Decode(input);  
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

        public void Test()
        {
            //data={"keyA":"valueA","keyB":[{"keyB1":"valueB1","keyB2":"valueB2"}],"keyC":"valueC"}
            StringBuilder sbData = new StringBuilder();
            sbData.Clear();
            sbData.Append("{");
            sbData.Append("'data':{");
            sbData.Append("'respCode':'0000'");
            sbData.Append(",'respMsg':'成功'");
            sbData.Append(",'bigOrderNo':'2016062900000001'");
            sbData.Append(",'bigOrderReqNo':'2016062900000001'");
            sbData.Append(",'bigOrderAmt':'12000'");
            sbData.Append(",'resBankFields':[{");
            sbData.Append("'bankCode':'0002'");
            sbData.Append(",'payType':'1009'");
            sbData.Append(",'payAmt':'12000'");
            sbData.Append(",'isStaging':'1'");
            sbData.Append(",'instInfo':'000000007267,156,000000000600,000000'");
            sbData.Append(",'isExchangeRate':'1'");
            sbData.Append(",'exchangeRate':'0000002000,000000000200'");
            sbData.Append("}]");
            sbData.Append(",'resDate':'20160629'");
            sbData.Append(",'innerTranSeq':'2016062900000005'");
            sbData.Append("}");
            sbData.Append(",'sign':'1234'");
            sbData.Append("}");
            JsonData jdP = JsonMapper.ToObject(sbData.ToString());
            if (jdP.Count > 0)
            {
                JsonData jdData = jdP[0];
                JsonData jdSign = jdP[1];
                string rRespCode = Convert.ToString(jdData["respCode"]);
                string rRespMsg = Convert.ToString(jdData["respMsg"]);
                if (rRespCode == "0000")//成功
                {
                    string RbigOrderNo = Convert.ToString(jdData["bigOrderNo"]);
                    string RbigOrderReqNo = Convert.ToString(jdData["bigOrderReqNo"]);
                    string RbigOrderAmt = Convert.ToString(jdData["bigOrderAmt"]);
                    string RresDate = Convert.ToString(jdData["resDate"]);
                    string RinnerTranSeq = Convert.ToString(jdData["innerTranSeq"]);
                    JsonData jdBankFields = jdData["resBankFields"];
                    if (jdBankFields.Count > 0)
                    {
                        foreach (JsonData jdBF in jdBankFields)
                        {
                            string RbankCode = Convert.ToString(jdBF["bankCode"]);
                            string RpayType = Convert.ToString(jdBF["payType"]);
                            string RpayAmt = Convert.ToString(jdBF["payAmt"]);
                            string RisStaging = Convert.ToString(jdBF["isStaging"]);
                            string RinstInfo = Convert.ToString(jdBF["instInfo"]);
                            string RisExchangeRate = Convert.ToString(jdBF["isExchangeRate"]);
                            string RexchangeRate = Convert.ToString(jdBF["exchangeRate"]);
                        }
                    }
                }
            }
            int kk = 0;
            return;
        }
        
        /// <summary>
        /// 2.1认证会员授权码获取接口
        /// /entry_memberfront/customerinfo/MFGetAuthMemberAuthCode.do
        /// </summary>
        public void AuthCode()
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Clear();
            sbData.Append("{'serviceCode':'").Append("031001002107061").Append("'");//一级商户代码
            sbData.Append(",'protolCode':'").Append("").Append("'");//协议号
            sbData.Append(",'operType':'4'");//操作类型 1：证件信息 3：卡号 4：会员编号
            //sbData.Append(",'certType':'01'");//证件类型 operType为1时必填
            //sbData.Append(",'certNo':'").Append("").Append("'");//证件号 operType为1时必填
            //sbData.Append(",'cardNo':'").Append("").Append("'");//卡号 operType为3时必填
            sbData.Append(",'userId':'").Append("").Append("'");//会员编号 operType为4时必填
            //sbData.Append(",'operId':'").Append("").Append("'");//操作员ID
            sbData.Append("}");

            //获取验证签名
            string sSign = Cmn.GetSign(sbData.ToString());

            //拼接Form并Post数据


            //返回数据
            string returnPost = "";
            if(!string.IsNullOrEmpty(returnPost))
            {
                JsonData jdP = JsonMapper.ToObject(returnPost.Trim());
                if (jdP.Count > 0)
                {
                    JsonData jdData = jdP[0];
                    JsonData jdSign = jdP[1];
                    string rRespCode = Convert.ToString(jdData["respCode"]);//响应结果
                    string rRespMsg = Convert.ToString(jdData["respMsg"]);//响应描述
                    string rAuthCode = "";
                    if (rRespCode == "0000")//成功
                    {
                        bool isCheck = Cmn.ValitedSign(jdSign.ToString(), jdData.ToJson());
                        if (isCheck == true)
                        {
                            //取出授权码
                            rAuthCode = Convert.ToString(jdData["authCode"]);//授权码
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 2.2会员订单交易WAP收银台接口
        /// /paycashier/userwappay/customModeWappay.do
        /// </summary>
        public void ModeWappay()
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Clear();
            sbData.Append("{'bigOrderNo':'").Append("2016062900000001").Append("'");//总订单号
            sbData.Append(",'bigReqNo':'").Append("2016062900000001").Append("'");//总订单请求流水号
            sbData.Append(",'serviceCode':'").Append("031001002107061").Append("'");//一级商户代码
            sbData.Append(",'bigOrderDate':'").Append("20160629").Append("'");//总订单日期 YYYYMMDD
            sbData.Append(",'bigOrderAmt':'").Append("12000").Append("'");//总订单金额 单位：分
            sbData.Append(",'pgUrl':'").Append("http://www.baidu.com").Append("'");//前台通知地址
            sbData.Append(",'bgUrl':'").Append("http://www.hao123.com").Append("'");//后台通知地址
            sbData.Append(",'authCode':'").Append("").Append("'");//授权码
            sbData.Append(",'orderFields':[{");//订单信息域
            sbData.Append("'mchntCode':'").Append("").Append("'");//二级商户代码
            sbData.Append(",'tradeCode':'").Append("0001").Append("'");//交易代码 0001：普通订单 0004：积分众酬 0005：包刷卡金红包 0006：包积分红包
            sbData.Append(",'reqSeq':'").Append("2016062900000001").Append("'");//订单请求流水号
            sbData.Append(",'orderNo':'").Append("2016062900000001").Append("'");//订单号
            sbData.Append(",'orderDate':'").Append("20160629").Append("'");//订单日期 YYYYMMDD
            sbData.Append(",'orderAmt':'").Append("12000").Append("'");//订单金额 单位：分
            sbData.Append(",'validateFlag':'").Append("0").Append("'");//订单有效期标识 0：永久 1：指定
            //sbData.Append(",'validate':'").Append("20160930120000").Append("'");//订单有效期 validateFlag为1时必填 格式：YYYYMMDDHH24MISS
            //sbData.Append(",'bizInfo':[{");//业务信息 tradeCode不为0001时必填
            //sbData.Append("'bizAccountNo':'").Append("").Append("'");//业务账号
            //sbData.Append("}]");
            sbData.Append("}]");
            sbData.Append(",'bankFields':[{");//银行信息域
            sbData.Append("'isStaging':'").Append("2").Append("'");//是否分期 1：分期 2：不分期
            //sbData.Append(",'stagingInfo':'").Append("03").Append("'");//分期信息 isStaging为1时必填 期数：选择值（03/06/12/24/36）
            sbData.Append(",'isExchangeRate':'").Append("03").Append("'");//是否积分抵现 1：积分抵现 2：非积分抵现
            //sbData.Append(",'exchangeRate':'").Append("2000").Append("'");//isExchangeRate为1时必填 单位：分 注：抵现金额必须为100的整数倍
            sbData.Append("}]");
            sbData.Append(",'clientIp':'").Append("127.0.0.1").Append("'");//客户IP
            sbData.Append(",'clientMac':'").Append("88-88-88-88-88-88").Append("'");//客户物理MAC
            sbData.Append(",'termType':'").Append("08").Append("'");//终端类型 07：个人电脑 08：手机设备
            sbData.Append("}");

            //获取验证签名
            string sSign = Cmn.GetSign(sbData.ToString());

            //拼接Form并Post数据


        }

        // 支付回调 前台通知
        public void ModeWappayBackA()
        {
            StringBuilder sbParam = new StringBuilder();
            Dictionary<string, string> resData = new Dictionary<string, string>();
            NameValueCollection coll = Request.Form;
            string[] requestItem = coll.AllKeys;
            for (int i = 0; i < requestItem.Length; i++)
            {
                resData.Add(requestItem[i], Request.Form[requestItem[i]]);
                sbParam.Append(requestItem[i]).Append("=").Append(Request.Form[requestItem[i]]).Append("&");
            }
            sbParam = sbParam.Remove(sbParam.Length - 1, 1);


            //返回数据
            string returnPost = "";
            if (!string.IsNullOrEmpty(returnPost))
            {
                JsonData jdP = JsonMapper.ToObject(returnPost.Trim());
                if (jdP.Count > 0)
                {
                    JsonData jdData = jdP[0];
                    JsonData jdSign = jdP[1];
                    string rRespCode = Convert.ToString(jdData["respCode"]);//响应结果
                    string rRespMsg = Convert.ToString(jdData["respMsg"]);//响应描述
                    if (rRespCode == "0000")//成功
                    {
                        bool isCheck = Cmn.ValitedSign(jdSign.ToString(), jdData.ToJson());
                        if (isCheck == true)
                        {
                            string RbigOrderNo = Convert.ToString(jdData["bigOrderNo"]);//总订单号
                            string RbigOrderReqNo = Convert.ToString(jdData["bigOrderReqNo"]);//总订单请求流水号
                            string RbigOrderAmt = Convert.ToString(jdData["bigOrderAmt"]);//总订单金额 单位：分
                            string RresDate = Convert.ToString(jdData["resDate"]);//响应日期 YYYYMMDD
                            string RinnerTranSeq = Convert.ToString(jdData["innerTranSeq"]);//平台流水号
                            JsonData jdBankFields = jdData["resBankFields"];//银行响应消息
                            if (jdBankFields.Count > 0)
                            {
                                foreach (JsonData jdBF in jdBankFields)
                                {
                                    string RbankCode = Convert.ToString(jdBF["bankCode"]);//银行代码 0001：虚拟账户 0002：浦发银行信用卡 9999：无支付
                                    string RpayType = Convert.ToString(jdBF["payType"]);//支付方式 1001：消费 1009：快捷消费
                                    string RpayAmt = Convert.ToString(jdBF["payAmt"]);//支付金额 单位：分
                                    string RisStaging = Convert.ToString(jdBF["isStaging"]);//是否分期 1：分期 2：不分期
                                    string RinstInfo = Convert.ToString(jdBF["instInfo"]);//分期信息
                                    string RisExchangeRate = Convert.ToString(jdBF["isExchangeRate"]);//是否积分抵现 1：积分抵现 2：非积分抵现
                                    string RexchangeRate = Convert.ToString(jdBF["exchangeRate"]);//积分抵现信息
                                }
                            }
                        }
                    }
                }
            }
        }

        // 支付回调 后台通知
        public void ModeWappayBackB()
        {
            StringBuilder sbParam = new StringBuilder();
            Dictionary<string, string> resData = new Dictionary<string, string>();
            NameValueCollection coll = Request.Form;
            string[] requestItem = coll.AllKeys;
            for (int i = 0; i < requestItem.Length; i++)
            {
                resData.Add(requestItem[i], Request.Form[requestItem[i]]);
                sbParam.Append(requestItem[i]).Append("=").Append(Request.Form[requestItem[i]]).Append("&");
            }
            sbParam = sbParam.Remove(sbParam.Length - 1, 1);

            //返回数据
            string returnPost = "";
            if (!string.IsNullOrEmpty(returnPost))
            {
                JsonData jdP = JsonMapper.ToObject(returnPost.Trim());
                if (jdP.Count > 0)
                {
                    JsonData jdData = jdP[0];
                    JsonData jdSign = jdP[1];
                    string rRespCode = Convert.ToString(jdData["respCode"]);//响应结果
                    string rRespMsg = Convert.ToString(jdData["respMsg"]);//响应描述
                    if (rRespCode == "0000")//成功
                    {
                        bool isCheck = Cmn.ValitedSign(jdSign.ToString(), jdData.ToJson());
                        if (isCheck == true)
                        {
                            string RbigOrderNo = Convert.ToString(jdData["bigOrderNo"]);//总订单号
                            string RbigOrderReqNo = Convert.ToString(jdData["bigOrderReqNo"]);//总订单请求流水号
                            string RresDate = Convert.ToString(jdData["resDate"]);//响应日期 YYYYMMDD
                            string RinnerTranSeq = Convert.ToString(jdData["innerTranSeq"]);//平台流水号
                            JsonData jdBankFields = jdData["resBankFields"];//银行响应消息
                            if (jdBankFields.Count > 0)
                            {
                                foreach (JsonData jdBF in jdBankFields)
                                {
                                    string RbankCode = Convert.ToString(jdBF["bankCode"]);//银行代码 0001：虚拟账户 0002：浦发银行信用卡 9999：无支付
                                    string RpayType = Convert.ToString(jdBF["payType"]);//支付方式 1001：消费 1002：预授权 1009：快捷消费
                                    string RpayAmt = Convert.ToString(jdBF["payAmt"]);//支付金额 单位：分
                                    string RisStaging = Convert.ToString(jdBF["isStaging"]);//是否分期 1：分期 2：不分期
                                    string RstagingInfo = Convert.ToString(jdBF["stagingInfo"]);//分期信息
                                    string RisExchangeRate = Convert.ToString(jdBF["isExchangeRate"]);//是否积分抵现 1：积分抵现 2：非积分抵现
                                    string RexchangeRate = Convert.ToString(jdBF["exchangeRate"]);//积分抵现信息
                                    string RpayResult = Convert.ToString(jdBF["payResult"]);//支付结果 0000：表示成功
                                    string RbankResDate = Convert.ToString(jdBF["bankResDate"]);//银行响应日期
                                    string RbankResNo = Convert.ToString(jdBF["bankResNo"]);//银行响应流水号
                                }
                            }
                            JsonData jdOrderFields = jdData["resOrderFields"];//订单响应消息
                            if (jdOrderFields.Count > 0)
                            {
                                foreach (JsonData jdOF in jdOrderFields)
                                {
                                    string RmchntCode = Convert.ToString(jdOF["mchntCode"]);//商户代码
                                    string RinstId = Convert.ToString(jdOF["instId"]);//分支机构ID 暂不开放
                                    string RtradeCode = Convert.ToString(jdOF["tradeCode"]);//交易代码 0001：普通订单 0004：积分众酬 0005：包刷卡金红包 0006：包积分红包
                                    string RreqSeq = Convert.ToString(jdOF["reqSeq"]);//订单请求流水号
                                    string RorderNo = Convert.ToString(jdOF["orderNo"]);//订单号
                                    string RorderDate = Convert.ToString(jdOF["orderDate"]);//订单日期 YYYYMMDD
                                    string RorderAmt = Convert.ToString(jdOF["orderAmt"]);//订单金额 单位：分
                                    string RuserId = Convert.ToString(jdOF["userId"]);//客户ID 暂不开放
                                    string RvalidateFlag = Convert.ToString(jdOF["validateFlag"]);//订单有效期标识 暂不开放
                                    string Rvalidate = Convert.ToString(jdOF["validate"]);//订单有效期 暂不开放
                                    string RoldOrderNo = Convert.ToString(jdOF["oldOrderNo"]);//原总订单号 暂不开放
                                    string RoldOrderReqNo = Convert.ToString(jdOF["oldOrderReqNo"]);//原总订单请求流水号 暂不开放
                                    string RfullDesc = Convert.ToString(jdOF["fullDesc"]);//详细描述 暂不开放
                                    string RbusiResult = Convert.ToString(jdOF["busiResult"]);//业务结果 0000：表示成功
                                    string RbusiResDate = Convert.ToString(jdOF["busiResDate"]);//业务响应日期
                                    string RbusiResNo = Convert.ToString(jdOF["busiResNo"]);//业务响应流水号
                                    JsonData jdbizInfos = jdData["bizInfo"];//业务信息 tradeCode不为0001时显示
                                    if (jdOrderFields.Count > 0)
                                    {
                                        string RinAccountNo = Convert.ToString(jdbizInfos[0]["inAccountNo"]);//转入主账号
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 2.3订单反交易接口
        /// /paycashier/refund/BFrefund.do
        /// </summary>
        public void OrderRefund()
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Clear();
            sbData.Append("{'oldBigOrderNo':'").Append("2016062900000001").Append("'");//原总订单号
            sbData.Append(",'oldBigReqNo':'").Append("2016062900000001").Append("'");//原总订单请求流水号
            sbData.Append(",'serviceCode':'").Append("031001002107061").Append("'");//一级商户代码
            sbData.Append(",'refundDate':'").Append("20160629").Append("'");//反交易日期 YYYYMMDD
            sbData.Append(",'refundAmt':'").Append("12000").Append("'");//反交易金额 单位：分
            sbData.Append(",'mchntCode':'").Append("").Append("'");//二级商户代码
            sbData.Append(",'transCode':'").Append("2002").Append("'");//反交易类型 2001：冲正 2002：退款 2003：消费撤销 2004：预授权撤销
            sbData.Append(",'refundNo':'").Append("2016062900000001").Append("'");//反交易请求流水号
            sbData.Append("}");

            //获取验证签名
            string sSign = Cmn.GetSign(sbData.ToString());

            //拼接Form并Post数据

            //返回数据
            string returnPost = "";
            if (!string.IsNullOrEmpty(returnPost))
            {
                JsonData jdP = JsonMapper.ToObject(returnPost.Trim());
                if (jdP.Count > 0)
                {
                    JsonData jdData = jdP[0];
                    JsonData jdSign = jdP[1];
                    string rRespCode = Convert.ToString(jdData["respCode"]);//响应结果
                    string rRespMsg = Convert.ToString(jdData["respMsg"]);//响应描述
                    if (rRespCode == "0000")//成功
                    {
                        bool isCheck = Cmn.ValitedSign(jdSign.ToString(), jdData.ToJson());
                        if (isCheck == true)
                        {
                            string RoldBigOrderNo = Convert.ToString(jdData["oldBigOrderNo"]);//原总订单号
                            string RoldBigReqNo = Convert.ToString(jdData["oldBigReqNo"]);//原总订单请求流水号
                            string RrefundNo = Convert.ToString(jdData["refundNo"]);//反交易请求流水号
                            string RrefundAmt = Convert.ToString(jdData["refundAmt"]);//反交易金额 单位：分
                            string RinnerTranSeq = Convert.ToString(jdData["innerTranSeq"]);//平台流水号
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 2.4订单状态查询接口
        /// paycashier/account/queryOrderStatus.do
        /// </summary>
        public void OrderStatus()
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Clear();
            sbData.Append("{'serviceCode':'").Append("031001002107061").Append("'");//一级商户代码
            sbData.Append(",'bigOrderNo':'").Append("2016062900000001").Append("'");//总订单号
            sbData.Append(",'bigReqNo':'").Append("2016062900000001").Append("'");//总订单请求流水号
            sbData.Append("}");

            //获取验证签名
            string sSign = Cmn.GetSign(sbData.ToString());

            //拼接Form并Post数据

            //返回数据
            string returnPost = "";
            if (!string.IsNullOrEmpty(returnPost))
            {
                JsonData jdP = JsonMapper.ToObject(returnPost.Trim());
                if (jdP.Count > 0)
                {
                    JsonData jdData = jdP[0];
                    JsonData jdSign = jdP[1];
                    string rRespCode = Convert.ToString(jdData["respCode"]);//响应结果
                    string rRespMsg = Convert.ToString(jdData["respMsg"]);//响应描述
                    if (rRespCode == "0000")//成功
                    {
                        bool isCheck = Cmn.ValitedSign(jdSign.ToString(), jdData.ToJson());
                        if (isCheck == true)
                        {
                            string RbigOrderDate = Convert.ToString(jdData["bigOrderDate"]);//总订单日期 YYYYMMDD
                            string RbigOrderNo = Convert.ToString(jdData["bigOrderNo"]);//总订单号
                            string RbigReqNo = Convert.ToString(jdData["bigReqNo"]);//总订单请求流水号
                            string RorderStatus = Convert.ToString(jdData["orderStatus"]);//订单状态 A：请求 B：成功 C：失败 D：待支付
                            string RbigOrderAmt = Convert.ToString(jdData["bigOrderAmt"]);//总订单金额 单位：分
                            string RbigPayAmt = Convert.ToString(jdData["bigPayAmt"]);//总支付金额 单位：分
                            string RinnerTranSeq = Convert.ToString(jdData["innerTranSeq"]);//平台流水号
                        }
                    }
                }
            }
        }
    }

}