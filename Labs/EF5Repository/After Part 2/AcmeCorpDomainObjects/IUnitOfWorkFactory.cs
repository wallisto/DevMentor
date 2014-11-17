namespace AcmeCorpDomainObjects
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}