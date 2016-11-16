using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MeetUp.ApiTokenDemo.SDK
{
    public static class TokenHelper
    {
        //private const string _DefaultKeyName = "_DEFAULT";
        private static Dictionary<string, RSACryptoServiceProvider> _RSACSP_STORE = new Dictionary<string, RSACryptoServiceProvider>();
        private static HashAlgorithm _HALG = new SHA256CryptoServiceProvider();
        
        public static void AddKeyFile(string name, string path)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(File.ReadAllText(path));
            _RSACSP_STORE[name] = rsa;
        }

        public static void ResetKey()
        {
            _RSACSP_STORE.Clear();
        }

        //static TokenHelper()
        //{
        //    string keyfile = null;
        //    keyfile = ConfigurationManager.AppSettings["SDK.TokenHelper.KeyFile"];

        //    if (HttpContext.Current != null)
        //    {
        //        keyfile = HttpContext.Current.Server.MapPath("~/App_Data/RSAKEY.xml");
        //        if (File.Exists(keyfile) == false) keyfile = null;
        //    }

        //    if (string.IsNullOrEmpty(keyfile))
        //    {
        //        keyfile = Path.Combine(System.Reflection.Assembly.GetEntryAssembly().Location, "RASKEY.xml");
        //        if (File.Exists(keyfile) == false) keyfile = null;
        //    }

        //    //if (File.Exists(keyfile) == false) throw new FileNotFoundException("RSA KEY file not found.", keyfile);
        //    if (string.IsNullOrEmpty(keyfile)) return;
        //    if (File.Exists(keyfile) == false) return;

        //    AddKeyFile(_DefaultKeyName, keyfile);
        //}
        
        /// <summary>
        /// TokenData 編碼用的分隔字元
        /// </summary>
        private const char _SplitChar = '|';

        
        public static T CreateToken<T>() where T : TokenData, new()
        {
            T token = new T();
            
            token.TypeName = typeof(T).FullName;

            return token;
        }

        //public static T DecodeToken<T>(string tokenText) where T : TokenData, new()
        //{
        //    return DecodeToken<T>(_DefaultKeyName, tokenText);
        //}
        public static T DecodeToken<T>(string keyName, string tokenText) where T : TokenData, new()
        {
            bool isSecure;
            bool isValidate;
            T token = TryDecodeToken<T>(keyName, tokenText, out isSecure, out isValidate);

            if (isSecure == false) throw new TokenNotSecureException();

            if (isValidate == false) throw new TokenNotValidateException();

            return token;
        }

        
        public static T TryDecodeToken<T>(string keyName, string tokenText, out bool isSecure, out bool isValidate) where T : TokenData, new()
        {
            string[] parts = tokenText.Split(_SplitChar);

            if (parts == null || parts.Length != 2) throw new TokenFormatException();

            byte[] data_buffer = Convert.FromBase64String(parts[0]);
            byte[] sign_buffer = Convert.FromBase64String(parts[1]);

            // 還原 token 物件，將資料反序列化還原為 object, 同時驗證 token 的授權是否合法
            T token = null;
            {
                MemoryStream ms = new MemoryStream(data_buffer, false);
                using (BsonReader br = new BsonReader(ms))
                {
                    JsonSerializer js = new JsonSerializer();
                    token = js.Deserialize<T>(br);

                    if (token == null) throw new TokenFormatException();
                }
                isValidate = token.IsValidate();
            }
            
            isSecure = _RSACSP_STORE[keyName].VerifyData(
                data_buffer,
                _HALG,
                sign_buffer);

            return token;
        }

        //public static string EncodeToken(TokenData token)
        //{
        //    return EncodeToken(_DefaultKeyName, token);
        //}
        public static string EncodeToken(string keyName, TokenData token)
        {
            if (_RSACSP_STORE[keyName].PublicOnly) throw new InvalidOperationException("Need Private KEY.");

            // TokenData 經過序列化之後的 binary data (使用 BSON format)
            byte[] data_buffer = null;
            {
                MemoryStream dataMS = new MemoryStream();
                using (BsonWriter bw = new BsonWriter(dataMS))
                {
                    JsonSerializer js = new JsonSerializer();
                    token.TypeName = token.GetType().FullName;
                    js.Serialize(bw, token);
                }
                data_buffer = dataMS.ToArray();
            }
            
            // data_buffer 的簽章
            byte[] sign_buffer = null;
            {
                //sign_buffer = _PublicKeyStoreDict[_CurrentSiteID].SignData(
                sign_buffer = _RSACSP_STORE[keyName].SignData(
                    data_buffer,
                    _HALG);
            }

            // 打包 data_buffer, sign_buffer
            return string.Format(
                @"{1}{0}{2}",
                _SplitChar,
                Convert.ToBase64String(data_buffer),
                Convert.ToBase64String(sign_buffer));
        }
    }
}
