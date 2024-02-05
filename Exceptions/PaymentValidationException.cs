using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exceptions
{
    internal class PaymentValidationException:ApplicationException
    {
        public PaymentValidationException()
        {
            
        }
        public PaymentValidationException(string message):base(message)
        {
            
        }
    }
}
