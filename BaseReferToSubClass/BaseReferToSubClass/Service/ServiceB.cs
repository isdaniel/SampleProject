namespace BaseReferToSubClass.Service
{
    public class ServiceB
    {
        public string GetDataBy()
        {
            return new AES256Provider("1234567812345678","1234567812345678")
                .Encrypt("ServiceAInjection");
        }
    }
}