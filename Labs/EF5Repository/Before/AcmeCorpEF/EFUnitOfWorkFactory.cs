using AcmeCorpDomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorpEF
{
    public class EFUnitOfWorkFactory :IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new EFUnitOfWork("AcmeCorpEntities");
        }
    }
}
