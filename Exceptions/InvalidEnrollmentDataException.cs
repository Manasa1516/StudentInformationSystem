using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exceptions
{
    internal class InvalidEnrollmentDataException:ApplicationException
    {
        public InvalidEnrollmentDataException()
        {
            
        }
        public InvalidEnrollmentDataException(string message):base(message)
        {
            
        }
    }
}
