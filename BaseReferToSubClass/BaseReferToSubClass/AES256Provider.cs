using System;
using System.Security.Cryptography;
using System.Text;

namespace BaseReferToSubClass
{
    public class AES256Provider : AESBase
    {
        public override string Encrypt(string data)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(Key);
            byte[] ivArray = Encoding.UTF8.GetBytes(Iv);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(data);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.KeySize = 256;
            rDel.Key = keyArray;
            rDel.IV = ivArray;  // 初始化向量 initialization vector (IV)
            rDel.Mode = CipherMode.CBC; // 密碼分組連結（CBC，Cipher-block chaining）模式
            rDel.Padding = PaddingMode.Zeros;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public override string Decrypt(string data)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(Key);
            byte[] ivArray = Encoding.UTF8.GetBytes(Iv);
            byte[] toEncryptArray = Convert.FromBase64String(data);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.KeySize = 256;
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.Zeros;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }

        public AES256Provider(string iv, string key) : base(iv, key)
        {
        }
    }
}