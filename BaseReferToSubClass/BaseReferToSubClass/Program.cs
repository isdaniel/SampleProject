using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseReferToSubClass.Service;

namespace BaseReferToSubClass
{
    class Program
    {
        static void Main(string[] args)
        {
            IAESProvider aesProvider = new AES128Provider("1234567812345678","1234567812345678");
            ServiceBInjection service = new ServiceBInjection(aesProvider);
            Console.WriteLine(aesProvider.Decrypt(service.GetDataBy()));

            Console.ReadKey();
        }

        public static void ShowEncryptData(IAESProvider aesProvider,string data)
        {
            var encryptData = aesProvider.Encrypt(data);
            Console.WriteLine(encryptData);
        }

        public static void ShowDecryptData(IAESProvider aesProvider,string data)
        {
            var decryptData = aesProvider.Decrypt(data);
            Console.WriteLine(decryptData);
        }
    }
}
