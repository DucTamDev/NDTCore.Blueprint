using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDTCore.Blueprint.SOLIDPrinciples
{

    // Violation of Open/Closed Principle (OCP) example
    public class PaymentProcessor
    {
        public void ProcessPayment(string paymentType)
        {
            if (paymentType == "CreditCard")
            {
                // Process credit card payment
            }
            else if (paymentType == "PayPal")
            {
                // Process PayPal payment
            }
        }
    }

    // Refactored code to follow OCP
    public interface IPaymentService
    {
        void Process();
    }

    public class CreditCardPayment : IPaymentService
    {
        public void Process()
        {
            // Process credit card payment
        }
    }

    public class PayPalPayment : IPaymentService
    {
        public void Process()
        {
            // Process PayPal payment
        }
    }

    public class PaymentProcessorOCP
    {
        public void ProcessPaymentOCP(IPaymentService paymentService)
        {
            paymentService.Process();
        }
    }
}
