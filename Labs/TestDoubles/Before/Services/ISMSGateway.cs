using ThirdParty;

namespace Services
{
    public interface ISMSGateway
    {
        void Send(string to, string message);
    }

    public class RealSMSGateway : ISMSGateway
    {
        SMSGateway gateway = new SMSGateway();
        public void Send(string to, string message)
        {
            gateway.Send(to, message);
        }
    }
}