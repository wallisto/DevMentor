using System;
using System.ServiceModel;

namespace DM
{
    [MessageContract(IsWrapped=false)]
    public class OrderMessage
    {
        [MessageBodyMember(Name = "Order", Namespace = Constants.MESSAGE_NAMESPACE)]
        public Order Order;
    }
}
