using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BaseReferToSubClass
{
    public class AES128Provider : AESBase
    {
        public override string Encrypt(string data)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(Key);
            byte[] ivBytes = Encoding.UTF8.GetBytes(Iv);
            using (RijndaelManaged cipher = new RijndaelManaged())
            {
                cipher.Mode = CipherMode.CBC;
                cipher.Padding = PaddingMode.PKCS7;
                cipher.KeySize = 128;
                cipher.BlockSize = 128;
                cipher.Key = keyBytes;
                cipher.IV = ivBytes;

                byte[] valueBytes = Encoding.UTF8.GetBytes(data);

                byte[] encrypted;
                using (ICryptoTransform encryptor = cipher.CreateEncryptor())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream writer = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            writer.Write(valueBytes, 0, valueBytes.Length);
                            writer.FlushFinalBlock();
                            encrypted = ms.ToArray();

                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < encrypted.Length; i++)
                                sb.Append(Convert.ToString(encrypted[i], 16).PadLeft(2, '0'));
                            return sb.ToString().ToUpperInvariant();
                        }
                    }
                }
            }
        }

        public override string Decrypt(string data)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(Key);
            byte[] ivBytes = Encoding.UTF8.GetBytes(Iv);

            using (RijndaelManaged cipher = new RijndaelManaged())
            {
                cipher.Mode = CipherMode.CBC;
                cipher.Padding = PaddingMode.PKCS7;
                cipher.KeySize = 128;
                cipher.BlockSize = 128;
                cipher.Key = keyBytes;
                cipher.IV = ivBytes;

                List<byte> lstBytes = new List<byte>();
                for (int i = 0; i < data.Length; i += 2)
                    lstBytes.Add(Convert.ToByte(data.Substring(i, 2), 16));

                using (ICryptoTransform decryptor = cipher.CreateDecryptor())
                {
                    using (MemoryStream msDecrypt = new MemoryStream(lstBytes.ToArray()))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }

        public AES128Provider(string iv, string key) : base(iv, key)
        {
        }
    }
}