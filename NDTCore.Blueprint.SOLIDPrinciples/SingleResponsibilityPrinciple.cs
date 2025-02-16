using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDTCore.Blueprint.SOLIDPrinciples
{
    // Violation of Single Responsibility Principle (SRP) example

    public class UserService
    {
        public void RegisterUser(string username, string password)
        {
            SaveToDatabase(username, password);
            SendWelcomeEmail(username);
        }

        private void SaveToDatabase(string username, string password)
        {
            // Code to save user to the database
        }

        private void SendWelcomeEmail(string username)
        {
            // Code to send a welcome email
        }
    }


    // Refactored code to follow SRP
    public class UserServiceSRP
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public UserServiceSRP(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public void RegisterUser(string username, string password)
        {
            _userRepository.Save(username, password);
            _emailService.SendWelcomeEmail(username);
        }
    }

    public interface IUserRepository
    {
        void Save(string username, string password);
    }

    public interface IEmailService
    {
        void SendWelcomeEmail(string username);
    }
}
