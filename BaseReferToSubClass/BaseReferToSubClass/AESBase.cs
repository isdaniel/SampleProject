namespace BaseReferToSubClass
{
    public abstract class AESBase : IAESProvider
    {
        public string Key { get;  }
        public string Iv { get;  }
        public AESBase(string iv, string key)
        {
            Iv = iv;
            Key = key;
        }

        public abstract string Encrypt(string data);

        public abstract string Decrypt(string data);
    }
}