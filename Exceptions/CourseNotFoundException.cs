using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exceptions
{
    internal class CourseNotFoundException:ApplicationException
    {
        public CourseNotFoundException()
        {
            

        }
        public CourseNotFoundException(string message):base(message)
        {
            
        }
    }
}
