namespace BaseReferToSubClass
{
    public interface IAESProvider
    {
        string Encrypt(string data);
        string Decrypt(string data);
    }
}