using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exceptions
{
    internal class InvalidTeacherDataException:ApplicationException
    {
        public InvalidTeacherDataException()
        {
            
        }
        public InvalidTeacherDataException(string message):base(message)
        {
            
        }
    }
}
