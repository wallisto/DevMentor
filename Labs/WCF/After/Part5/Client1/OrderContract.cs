using System.ServiceModel;

namespace DM.PetShop
{
    [ServiceContract(Name = "PetShopOrderContract", Namespace = "urn:dm:wcf:labs:architecure")]
    public interface IOrderContract
    {
        [OperationContract(Name = "PlaceOrder", Action = "PlaceOrder", ReplyAction = "PlaceOrderReply")]
        void PlaceOrder(Order orderData);
    }
}
