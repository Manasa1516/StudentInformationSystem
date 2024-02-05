using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exceptions
{
    internal class TeacherNotFoundException:ApplicationException
    {
        public TeacherNotFoundException()
        {
            
        }
        public TeacherNotFoundException(string message):base(message)
        {
            
        }
    }
}
