using System;
using System.ServiceModel;
using DM.PetShopSvcClient;

namespace DM
{
    class Program
    {
        static void Main(string[] args)
        {
            PetShopServiceClient proxy = new PetShopServiceClient();
            try
            {
                proxy.ClientCredentials.UserName.UserName = "Alice";
                proxy.ClientCredentials.UserName.Password = "Alice";
                
                Console.WriteLine("Calling PlaceOrder as " + proxy.ClientCredentials.UserName.UserName);
                proxy.PlaceOrder(new Order
                    {
                        Quantity = 1,
                        ProductName = "Anaconda",
                        Comments = "2342"
                    });
                Console.WriteLine("PlaceOrder called");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Calling PlaceOrder failed. {0}", ex.Message);
            }
            finally
            {
                try
                {
                    if (proxy.State != CommunicationState.Closed)
                        proxy.Close();
                }
                catch
                {
                    proxy.Abort();
                }
            }

        }
    }
}
