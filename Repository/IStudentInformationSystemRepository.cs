using StudentInformationSystem.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Repository
{
    internal interface IStudentInformationSystemRepository
    {
        void EnrollInCourse(Course course, int student_id);

        void UpdateStudentInfo(int student_id, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber);

        void MakePayment(int student_id, decimal amount);

        Student DisplayStudentInfo(int student_id);

        List<Course> GetEnrolledCourses(int student_id);

        List<Payment> GetPaymentHistory(int student_id);

        void UpdateCourseInfo(int course_id, int credits, string courseName, int teacher_id);

        Course DisplayCourseInfo(int course_id);

        List<Enrollment> GetEnrollments();

        Teacher GetTeacher(int course_id);

        Student GetStudent(int enrollment_id);

        Course GetCourse(int enrollment_id);

        void UpdateTeacherInfo(int teacherID, string firstname, string lastname, string email);

        Teacher DisplayTeacherInfo(int id);

        List<Course> GetAssignedCourses(int teacher_id);

        decimal GetPaymentAmount(int payment_id);

        DateTime GetPaymentDate(int payment_id);

        void AssignTeacherToCourse(Teacher teacher, Course course);

        void GenerateEnrollmentReport(Course course);

        void CalculateCourseStatistics(Course course);

    }
}
