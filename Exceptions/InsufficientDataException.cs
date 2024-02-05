using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exceptions
{
    internal class InsufficientDataException:ApplicationException
    {
        public InsufficientDataException()
        {
            
        }
        public InsufficientDataException(string message):base(message)
        {
            
        }
    }
}
