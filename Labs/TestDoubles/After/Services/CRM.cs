namespace Services
{
    public class CRM
    {
        private IContactRepository repository;
        private readonly IDeliveryService smsService;
        private readonly IDeliveryService emailService;

        public CRM(IContactRepository repository, IDeliveryService smsService, IDeliveryService emailService)
        {
            this.repository = repository;
            this.smsService = smsService;
            this.emailService = emailService;
        }

        public void SendCustomerMessage(DeliveryMethod deliveryMethod, string messageTemplate)
        {
            switch (deliveryMethod)
            {
                case DeliveryMethod.SMS:
                    smsService.Send(repository.GetAll(), messageTemplate);
                    break;
                case DeliveryMethod.EMail:
                    emailService.Send(repository.GetAll(), messageTemplate);
                    break;
            }    
        }
    }
}