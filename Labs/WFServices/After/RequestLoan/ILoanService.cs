using System.ServiceModel;

namespace RequestLoan
{
    [ServiceContract(Namespace = "http://develop.com/")]
    public interface ILoanService
    {
        [OperationContract]
        ApplyResponse Apply(double amount, int term);

        [OperationContract]
        bool Confirm(int applicationId);
    }
}