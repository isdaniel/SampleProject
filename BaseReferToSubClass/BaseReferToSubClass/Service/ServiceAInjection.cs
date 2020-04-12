using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseReferToSubClass.Service
{
    public class ServiceAInjection
    {
        private IAESProvider _provider;

        public ServiceAInjection(IAESProvider provider)
        {
            _provider = provider;
        }

        public string GetDataBy()
        {
            return _provider.Encrypt("ServiceAInjection");
        }

    }
}
