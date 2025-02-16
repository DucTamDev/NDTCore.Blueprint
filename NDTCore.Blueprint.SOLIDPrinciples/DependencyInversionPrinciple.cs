namespace NDTCore.Blueprint.SOLIDPrinciples
{
    // Violation of Dependency Inversion Principle (DIP) example
    public class EmailService
    {
        public void SendEmail(string message)
        {
            Console.WriteLine("Sendding email message: " + message);
        }
    }

    public class Notification
    {
        private EmailService _emailService = new EmailService();

        public void Send(string message)
        {
            _emailService.SendEmail(message);
        }
    }


    // Refactored code to follow DIP
    public interface IMessageService
    {
        void SendMessage(string message);
    }

    public class EmailServiceDIP : IMessageService
    {
        public void SendMessage(string message)
        {
            Console.WriteLine("Sendding email message: " + message);
        }
    }

    public class Notification
    {
        private readonly IMessageService _messageService;

        public Notification(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public void Send(string message)
        {
            _messageService.SendMessage(message);
        }
    }
}
