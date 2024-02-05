using StudentInformationSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Exceptions;
using StudentInformationSystem.model;

namespace StudentInformationSystem.Service
{
    internal class StudentInformationSystemService: IStudentInformationSystemService
    {
        readonly IStudentInformationSystemRepository _sis;
        public StudentInformationSystemService()
        {
            _sis = new StudentInformationSystemRepository();
        }
        # region DisplayStudentInfoS
        public void DisplayStudentInfoS()
        {
            try
            {
                Console.WriteLine("Enter Student ID: ");
                int id = int.Parse(Console.ReadLine());

                Student student = new Student();

                student = _sis.DisplayStudentInfo(id);

                Console.WriteLine($"Student ID:{student.StudentID}\tFirst Name:{student.FirstName}\tLast Name:{student.LastName}\tDate of Birth:{student.DateOfBirth}\tEmail:{student.Email}\tPhone Number:{student.PhoneNumber}");
            }
            catch (StudentNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        #endregion
        #region DisplayCourseInfoS
        public void DisplayCourseInfoS()
        {
            try
            {
                Console.WriteLine("Enter Course ID: ");
                int id = int.Parse(Console.ReadLine());

                Course course = new Course();

                course = _sis.DisplayCourseInfo(id);
                Console.WriteLine($"Course ID:{course.CourseID}\tCourse Name:{course.CourseName}\tCredits:{course.Credits}\tInstructor Name:{course.InstructorName}");
            }
            catch (CourseNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        #endregion
        #region DisplayTeacherInfoS
        public void DisplayTeacherInfoS()
        {
            try
            {
                Console.WriteLine("Enter Teacher ID: ");
                int id = int.Parse(Console.ReadLine());

                Teacher teacher = new Teacher();

                teacher = _sis.DisplayTeacherInfo(id);
                Console.WriteLine($"Teacher ID:{teacher.TeacherID}\tFirst Name:{teacher.FirstName}\tLast Name:{teacher.LastName}\tEmail:{teacher.Email}");

            }
            catch (TeacherNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        #endregion
        #region GetEnrollmentsS
        public void GetEnrollmentsS()
        {
            List<Enrollment> enrollmentList = new List<Enrollment>();
            enrollmentList = _sis.GetEnrollments();

            foreach (var enrollment in enrollmentList)
            {
                Console.WriteLine($"\nEnrollment ID: {enrollment.EnrollmentID}\t Course ID: {enrollment.Course.CourseID}\t Student ID:: {enrollment.Student.StudentID}\t Enrollment Date: {enrollment.EnrollmentDate}");
            }
        }
        #endregion
        #region GetEnrolledCoursesS
        public void GetEnrolledCoursesS()
        {
            try
            {
                Console.WriteLine("Enter student ID of student: ");
                int studentID = int.Parse(Console.ReadLine());
                List<Course> courseList = new List<Course>();
                courseList = _sis.GetEnrolledCourses(studentID);
                foreach (var course in courseList)
                {
                    Console.WriteLine($"Course ID:{course.CourseID}\tCourse Name:{course.CourseName}\tCredits:{course.Credits}\tInstructor Name:{course.InstructorName}");
                }
            }
            catch (StudentNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        #endregion
        #region UpdateTeacherInfoS
        public void UpdateTeacherInfoS()
        {
            try
            {
                Console.WriteLine("Enter Teacher ID of Teacher you want to update: ");
                int teacherID = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Teacher First Name: ");
                string firstname = Console.ReadLine();
                Console.WriteLine("Enter Teacher Last Name: ");
                string lastname = Console.ReadLine();
                Console.WriteLine("Enter Email: ");
                string email = Console.ReadLine();

                _sis.UpdateTeacherInfo(teacherID, firstname, lastname, email);
            }
            catch (TeacherNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        #endregion
        #region GetPaymentHistoryS
        public void GetPaymentHistoryS()
        {
            try
            {
                List<Payment> paymentList = new List<Payment>();
                Console.WriteLine("Enter StudentId: ");
                int id = int.Parse(Console.ReadLine());

                paymentList = _sis.GetPaymentHistory(id);

                foreach (var payment in paymentList)
                {
                    Console.WriteLine($"Payment ID:{payment.PaymentID}\tStudent Id:{payment.Student.StudentID}\tAmount:{payment.Amount}\tPayment Date:{payment.PaymentDate}");
                }
            }
            catch (StudentNotFoundException ex)
            {
                Console.WriteLine($"Student with id {ex.Message}");
            }

        }
        #endregion
        #region EnrollInCourseS
        public void EnrollInCourseS()
        {
            Course course = new Course();
            Console.WriteLine("Enter Student Id: ");
            int sid = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Course Id: ");
            course.CourseID = int.Parse(Console.ReadLine());
            _sis.EnrollInCourse(course, sid);
        }
        #endregion
        #region UpdateStudentInfoS
        public void UpdateStudentInfoS()
        {
            try
            {
                Console.WriteLine("Enter Student ID of student to be updated: ");
                int student_id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter First Name:");
                string first_name = Console.ReadLine();
                Console.WriteLine("Enter Last Name:");
                string last_name = Console.ReadLine();
                Console.WriteLine("Enter Email:");
                string email = Console.ReadLine();
                Console.WriteLine("Enter Phone Number:");
                string phoneNumber = Console.ReadLine();
                Console.WriteLine("Enter Date of Birth : ");
                DateTime dob = DateTime.Parse(Console.ReadLine());

                _sis.UpdateStudentInfo(student_id, first_name, last_name, dob, email, phoneNumber);

            }
            catch (StudentNotFoundException ex)
            {
                Console.WriteLine($"Student with Student ID {ex.Message}.");
            }

        }
        #endregion
        #region MakePaymentS
        public void MakePaymentS()
        {
            try
            {
                Console.WriteLine("Enter student ID: ");
                int student_id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Amount: ");
                decimal amount = decimal.Parse(Console.ReadLine());
                _sis.MakePayment(student_id, amount);
            }
            catch (StudentNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        #endregion
        #region UpdateCourseInfoS
        public void UpdateCourseInfoS()
        {
            try
            {
                Console.WriteLine("Enter course ID: ");
                int course_id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter course credits: ");
                int credits = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Course Name:");
                string courseName = Console.ReadLine();
                Console.WriteLine("Enter teacher ID:");
                int teacher_id = int.Parse(Console.ReadLine());
                _sis.UpdateCourseInfo(course_id, credits, courseName, teacher_id);
            }
            catch (CourseNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        #endregion
        #region GetTeacherS
        public void GetTeacherS()
        {
            try
            {
                Console.WriteLine("Enter Course ID:  ");
                int course_id = int.Parse(Console.ReadLine());

                Teacher teacher = new Teacher();
                teacher = _sis.GetTeacher(course_id);

                Console.WriteLine($"Teacher id:{teacher.TeacherID}\tFirst Name:{teacher.FirstName}\tLast Name:{teacher.LastName}\tEmail:{teacher.Email}");
            }
            catch (CourseNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        #endregion
        #region GetCourseS
        public void GetCourseS()
        {
            try
            {
                Course course = new Course();
                Console.WriteLine("Enter Enrollment ID:");
                int enrollment_id = int.Parse(Console.ReadLine());
                course = _sis.GetCourse(enrollment_id);

                Console.WriteLine($"Course ID:{course.CourseID}\tCourse Name:{course.CourseName}\tCredits:{course.Credits}\tInstructor Name:{course.InstructorName}");

            }
            catch (InvalidEnrollmentDataException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        #endregion
        #region GetPaymentDateS
        public void GetPaymentDateS()
        {
            Console.WriteLine("Enter Payment ID: ");
            int payment_id = int.Parse(Console.ReadLine());

            DateTime payment_date = _sis.GetPaymentDate(payment_id);
            Console.WriteLine($"Payment date:{payment_date}");
        }
        #endregion
        #region GetPaymentAmountS
        public void GetPaymentAmountS()
        {
            Console.WriteLine("Enter Payment ID: ");
            int id = int.Parse(Console.ReadLine());
            decimal amount = _sis.GetPaymentAmount(id);
            Console.WriteLine($"Amount: {amount}");
        }
        #endregion
        #region GenerateEnrollmentReportS
        public void GenerateEnrollmentReportS()
        {
            try
            {
                Course course = new Course();
                Console.WriteLine("Enter Course ID: ");
                int course_id = int.Parse(Console.ReadLine());
                course.CourseID = course_id;
                _sis.GenerateEnrollmentReport(course);
            }
            catch (CourseNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        #endregion
        #region CalculateCourseStatisticsS
        public void CalculateCourseStatisticsS()
        {
            Course course = new Course();

            Console.WriteLine("Enter Course ID: ");
            int course_id = int.Parse(Console.ReadLine());
            course.CourseID = course_id;
            _sis.CalculateCourseStatistics(course);
        }
        #endregion
        #region GetStudentS
        public void GetStudentS()
        {
            try
            {
                Console.WriteLine("Enter Enrollmentt ID: ");
                int id = int.Parse(Console.ReadLine());
                Student student = new Student();
                student = _sis.GetStudent(id);
                Console.WriteLine($"Student ID:{student.StudentID}\tFirst Name:{student.FirstName}\tLast Name:{student.LastName}\tDate of Birth:{student.DateOfBirth}\tEmail:{student.Email}\tPhone Number:{student.PhoneNumber}");

            }
            catch (InvalidEnrollmentDataException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        #endregion
        #region GetAssignedCoursesS
        public void GetAssignedCoursesS()
        {
            try
            {
                List<Course> courseList = new List<Course>();
                Console.WriteLine("Enter teacher ID:");
                int teacher_id = int.Parse(Console.ReadLine());
                courseList = _sis.GetAssignedCourses(teacher_id);

                foreach (var course in courseList)
                {
                    Console.WriteLine($"Course ID:{course.CourseID}\tCourse Name:{course.CourseName}\tCredits:{course.Credits}\tInstructor Name:{course.InstructorName}");
                }
            }
            catch (TeacherNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        #endregion
        #region AssignTeacherToCourseS
        public void AssignTeacherToCourseS()
        {
            Teacher teacher = new Teacher();
            Course course = new Course();

            Console.WriteLine("Enter Teacher ID: ");
            teacher.TeacherID = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Course ID: ");
            course.CourseID = int.Parse(Console.ReadLine());

            _sis.AssignTeacherToCourse(teacher, course);
        }
        #endregion




    }
}
