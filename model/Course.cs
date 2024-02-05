using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.model
{
    internal class Course
    {

        int courseID;
        string courseName, courseCode, instructorName;

        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public string InstructorName { get; set; }
        public Course() 
        { 

        }
        public Course(int courseID, string courseName, int credits, string instructorName)
        {
            CourseID = courseID;
            CourseName = courseName;
            Credits = credits;
            InstructorName = instructorName;
        }

    }
}