using ThirdParty;

namespace Services
{
    public interface IEmailGateway
    {
        void Send(string from, string to, string subject, string message);
    }

    public class RealEmailGateway : IEmailGateway
    {
        EmailGateway gateway = new EmailGateway();

        public void Send(string from, string to, string subject, string message)
        {
            gateway.Send(from, to, subject, message);
        }
    }
}