using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Linq;
using Microsoft.IdentityModel.Claims;
using System.Security;

namespace DM
{
    [ServiceBehavior(Name = "PetShopService", Namespace = Constants.SERVICE_NAMESPACE)]
    class PetShop : IPetShop
    {
        static Dictionary<string, int> requiredPointsMap =
        new Dictionary<string, int>()
        {
            { "Guinea pig", 0 },
            { "Hamster", 0 },
            { "Boa constrictor", 50 },
            { "Anaconda", 100 },
        };

        public void PlaceOrder(OrderMessage message)
        {
            Console.WriteLine("Order:");
            Console.WriteLine("Product name: {0}", message.Order.ProductName);
            Console.WriteLine("Quantity: {0}", message.Order.Quantity);
            Console.WriteLine("Comments: {0}", message.Order.Comments);

            int requiredPoints;
            if (!requiredPointsMap.TryGetValue(message.Order.ProductName, out requiredPoints))
            {
                ReportSenderFault("An unknown product was ordered", "UnknownProduct");
            }

            try
            {
                ClaimsPrincipalPermission.CheckAccess(requiredPoints.ToString(), "BuyAnimal");
            }
            catch (SecurityException)
            {
                ReportSenderFault(string.Format("Required points: {0}.", requiredPoints),
                                    "InsufficientExperiencePoints");
            }

            Console.WriteLine("Order accepted.");
        }

        void ReportSenderFault(string message, string faultCodeName)
        {
            throw new FaultException(
                        new FaultReason(new FaultReasonText(message, "en")),
                               FaultCode.CreateSenderFaultCode(faultCodeName,
                                                   Constants.SERVICE_NAMESPACE));
        }

        void ReportReceiverFault(string message, string faultCodeName)
        {
            throw new FaultException(
                        new FaultReason(new FaultReasonText(message, "en")),
                              FaultCode.CreateReceiverFaultCode(faultCodeName,
                                                   Constants.SERVICE_NAMESPACE));
        }
    }
}
