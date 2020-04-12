namespace BaseReferToSubClass.Service
{
    public class ServiceA
    {
        public string GetDataBy()
        {
            return new AES256Provider("1234567812345678","1234567812345678")
                .Encrypt("ServiceAInjection");
        }
    }
}