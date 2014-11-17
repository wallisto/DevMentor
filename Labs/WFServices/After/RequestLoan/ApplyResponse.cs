using System.Runtime.Serialization;

namespace RequestLoan
{
    [DataContract]
    public class ApplyResponse
    {
        [DataMember]
        public int ApplicationId { get; set; }
        [DataMember]
        public double MonthlyRepayment { get; set; }
    }
}