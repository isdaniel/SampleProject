using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncodingEncryption
{
    public static class HashExtension
    {
        public static string ToSHA512Hash(this string input)
        {
            SHA512 sha512 = new SHA512CryptoServiceProvider();//建立一個SHA512
            byte[] source = Encoding.Default.GetBytes(input);//將字串轉為Byte[]
            byte[] crypto = sha512.ComputeHash(source);//進行雜湊
            return Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串
        }
    }


    public class HashProvider
    {
        public string Hash(string input)
        {
            SHA512 sha512 = new SHA512CryptoServiceProvider();//建立一個SHA512
            byte[] source = Encoding.Default.GetBytes(input);//將字串轉為Byte[]
            byte[] crypto = sha512.ComputeHash(source);//進行雜湊
            return Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串
        }
    }

    public class EncryptProvider
    {

        private string _key;
        private string _Iv;
        public EncryptProvider(string key, string iv)
        {
            _key = key;
            _Iv = iv;
        }
        public string Decrypt(string encrypt)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] key = Encoding.ASCII.GetBytes(_key);
            byte[] iv = Encoding.ASCII.GetBytes(_Iv);
            des.Key = key;
            des.IV = iv;

            byte[] dataByteArray = Convert.FromBase64String(encrypt);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }

        public string Encrypt(string source)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = Encoding.ASCII.GetBytes(_key);
            des.IV  = Encoding.ASCII.GetBytes(_Iv);
            byte[] dataByteArray = Encoding.UTF8.GetBytes(source);

            string encrypt = "";
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(dataByteArray, 0, dataByteArray.Length);
                cs.FlushFinalBlock();
                encrypt = Convert.ToBase64String(ms.ToArray());
            }
            return encrypt;
        }
    }

    public class Base64Provider
    {
        public string Encoding(string input)
        {
           var buffer = System.Text.Encoding.UTF8.GetBytes(input);
           return Convert.ToBase64String(buffer);
        }

        public string Decoding(string base64String)
        {
            var base64Buffer = Convert.FromBase64String(base64String);
            return System.Text.Encoding.UTF8.GetString(base64Buffer);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string input = "test";
    
            
            Console.ReadKey();
        }

        
    }
}
