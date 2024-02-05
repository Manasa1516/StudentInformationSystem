using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exceptions
{
    internal class InvalidStudentDataException:ApplicationException
    {
        public InvalidStudentDataException()
        {
            
        }
        public InvalidStudentDataException(string message):base(message)
        {
            
        }
    }
}
