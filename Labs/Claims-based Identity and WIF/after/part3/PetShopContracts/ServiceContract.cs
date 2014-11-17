using System;
using System.ServiceModel;

namespace DM
{
    [ServiceContract(Name = "PetShopService", Namespace = Constants.SERVICE_NAMESPACE)]
    public interface IPetShop
    {
        [OperationContract(Name= "PlaceOrder", Action= "PlaceOrder", ReplyAction="PlaceOrderReply")]
        void PlaceOrder(OrderMessage message);
    }
}
