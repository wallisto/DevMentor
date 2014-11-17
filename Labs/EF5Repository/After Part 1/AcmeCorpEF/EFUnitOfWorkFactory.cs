using AcmeCorpDomainObjects;

namespace AcmeCorpEF
{
    public class EFUnitOfWorkFactory  :IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new EFUnitOfWork("AcmeCorpEntities");
        }
    }
}