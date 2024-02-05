using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exceptions
{
    internal class StudentNotFoundException:ApplicationException
    {
        public StudentNotFoundException()
        {
            
        }
        public StudentNotFoundException(string message):base(message)
        {
            
        }
    }
}
