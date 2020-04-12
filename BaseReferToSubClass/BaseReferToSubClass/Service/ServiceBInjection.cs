using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseReferToSubClass.Service
{
    public class ServiceBInjection
    {
        private readonly IAESProvider _provider;
        public ServiceBInjection(IAESProvider provider)
        {
            this._provider = provider;
        }

        public string GetDataBy()
        {
            return _provider.Encrypt("ServiceBInjection");
        }
    }
}
