using System;
using System.Runtime.Serialization;

namespace DM
{
    [DataContract(Name="Order", Namespace=Constants.DATA_NAMESPACE)]
    public class Order
    {
        [DataMember(Name="Quantity", Order=1)]
        public int Quantity;

        [DataMember(Name="ProductName", Order=2)]
        public string ProductName;

        [DataMember(Name="Comments", Order=3)]
        public string Comments;
    }
}
