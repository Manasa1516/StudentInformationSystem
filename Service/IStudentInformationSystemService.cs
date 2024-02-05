using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Service
{
    internal interface IStudentInformationSystemService
    {
        void DisplayStudentInfoS();

        void DisplayCourseInfoS();

        void DisplayTeacherInfoS();

        void GetEnrollmentsS();
        
        void GetEnrolledCoursesS();

        void UpdateTeacherInfoS();

        void GetPaymentHistoryS();

        void EnrollInCourseS();

        void UpdateStudentInfoS();

        void MakePaymentS();
        
        void UpdateCourseInfoS();

        void GetTeacherS();

        void GetCourseS();

        void GetPaymentDateS();

        void GetPaymentAmountS();

        void GenerateEnrollmentReportS();

        void CalculateCourseStatisticsS();

        void GetStudentS();
        
        void GetAssignedCoursesS();
        
        void AssignTeacherToCourseS();
    }
}
