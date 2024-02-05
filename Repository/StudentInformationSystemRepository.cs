using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Exceptions;
using StudentInformationSystem.model;
using StudentInformationSystem.Utility;

namespace StudentInformationSystem.Repository
{
    internal class StudentInformationSystemRepository : IStudentInformationSystemRepository
    {
        public string connectionString = DBConnectionUtil.GetConnectionString();
        SqlConnection sqlconnection = null;
        SqlCommand cmd = null;

        public StudentInformationSystemRepository()
        {
            sqlconnection = new SqlConnection(connectionString);
            cmd = new SqlCommand();
        }
        #region DisplayStudentInfo
        public Student DisplayStudentInfo(int studentId)
        {
            Student student = new Student();
            cmd.CommandText = "select * from Students where StudentId = @ID;";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ID", studentId);
            cmd.Connection = sqlconnection;
            sqlconnection.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    student.StudentID = (int)reader["StudentId"];
                    student.FirstName = (string)reader["FirstName"];
                    student.LastName = (string)reader["LastName"];
                    student.DateOfBirth = (DateTime)reader["DateOfBirth"];
                    student.Email = (string)reader["Email"];
                    student.PhoneNumber = (string)reader["PhoneNumber"];
                }
                else
                {
                    sqlconnection.Close();

                    throw new StudentNotFoundException($"Student with ID {studentId} not found.");
                }
            }

            sqlconnection.Close();
            return student;

        }
        #endregion
        #region DisplayCourseInfo
        public Course DisplayCourseInfo(int courseId)
        {
            Course course = new Course();
            cmd.CommandText = "select * from Courses where CourseId = @ID;";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ID", courseId);
            cmd.Connection = sqlconnection;
            sqlconnection.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    course.CourseID = (int)reader["CourseId"];
                    course.CourseName = (string)reader["CourseName"];
                    course.Credits = (int)reader["Credits"];
                    int teacherId = (int)(reader)["TeacherId"];
                    sqlconnection.Close();
                    cmd.CommandText = "select FirstName,LastName from Teacher where TeacherId = @ID;";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", teacherId);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    using (SqlDataReader reader1 = cmd.ExecuteReader())
                    {
                        if (reader1.Read())
                        {
                            course.InstructorName = (string)reader1["FirstName"];
                            course.InstructorName += " " + (string)reader1["LastName"];
                        }

                    }
                }

                else
                {
                    sqlconnection.Close();

                    throw new CourseNotFoundException($"Course with ID {courseId} not found.");
                }
            }
            sqlconnection.Close();
            return course;
        }
        #endregion
        #region DisplayTeacherInfo
        public Teacher DisplayTeacherInfo(int id)
        {
            Teacher teacher = new Teacher();
            cmd.CommandText = "select * from Teacher where TeacherId = @ID;";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Connection = sqlconnection;
            sqlconnection.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    teacher.TeacherID = (int)reader["TeacherId"];
                    teacher.FirstName = (string)reader["FirstName"];
                    teacher.LastName = (string)reader["LastName"];
                    teacher.Email = (string)reader["Email"];
                }
                else
                {
                    sqlconnection.Close();

                    throw new TeacherNotFoundException($"Teacher with ID {id} not found.");
                }
            }

            sqlconnection.Close();


            return teacher;

        }
        #endregion
        #region GetEnrollments
        public List<Enrollment> GetEnrollments()
        {
            List<Enrollment> enrollmentList = new List<Enrollment>();
            try
            {
                cmd.CommandText = "select * from Enrollments";
                cmd.Parameters.Clear();
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Enrollment enrollment = new Enrollment();
                    enrollment.EnrollmentID = (int)reader["EnrollmentId"];
                    Student student = new Student { StudentID = (int)reader["StudentId"] };
                    enrollment.Student = student;
                    Course course = new Course { CourseID = (int)reader["CourseId"] };
                    enrollment.Course = course;
                    enrollment.EnrollmentDate = (DateTime)reader["EnrollmentDate"];

                    enrollmentList.Add(enrollment);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error Occured:{ex.Message}");
            }
            sqlconnection.Close();
            return enrollmentList;
        }
        #endregion
        #region GetEnrolledCourses
        public List<Course> GetEnrolledCourses(int studentId)
        {
            List<Course> courseList = new List<Course>();
            try
            {
                cmd.CommandText = "select * from Courses where CourseId in (select CourseId from Enrollments where StudentId=@id)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", studentId);
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    sqlconnection.Close();
                    throw new StudentNotFoundException($"Student with ID {studentId} not found.");
                }
                while (reader.Read())
                {
                    Course course = new Course();
                    course.CourseID = (int)reader["CourseId"];
                    course.CourseName = (string)reader["CourseName"];
                    course.Credits = (int)reader["Credits"];
                    int teacherId = (int)(reader)["TeacherId"];
                    sqlconnection.Close();
                    cmd.CommandText = "select FirstName,LastName from Teacher where TeacherId = @ID;";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", teacherId);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    SqlDataReader reader1 = cmd.ExecuteReader();
                    while (reader1.Read())
                    {
                        course.InstructorName = (string)reader1["FirstName"];
                        course.InstructorName += " " + (string)reader1["LastName"];

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error Occured:{ex.Message}");
            }
            sqlconnection.Close();
            return courseList;
        }
        #endregion
        #region UpdateTeacherInfo
        public void UpdateTeacherInfo(int teacherID, string firstname, string lastname, string email)
        {

            cmd.CommandText = "UPDATE Teacher SET FirstName = @firstname,LastName=@lastname,Email=@email WHERE TeacherId=@teacherID";

            sqlconnection.Open();
            cmd.Connection = sqlconnection;

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@teacherID", teacherID);
            cmd.Parameters.AddWithValue("@firstname", firstname);
            cmd.Parameters.AddWithValue("@lastname", lastname);
            cmd.Parameters.AddWithValue("@email", email);

            int updateTeacherStatus = cmd.ExecuteNonQuery();
            sqlconnection.Close();

            if (updateTeacherStatus > 0)
            {
                Console.WriteLine("Teacher Updated successfully.");
            }
            else
            {
                throw new TeacherNotFoundException($"Teacher with id {teacherID} not found.");

            }
            sqlconnection.Close();

        }
        #endregion
        #region GetPaymentHistory
        public List<Payment> GetPaymentHistory(int studentId)
        {
            List<Payment> paymentList = new List<Payment>();


            cmd.CommandText = "select * from Payments where StudentId=@studentId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@studentId", studentId);
            cmd.Connection = sqlconnection;
            sqlconnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                sqlconnection.Close();
                throw new StudentNotFoundException($"Student with id {studentId} not found");
            }
            while (reader.Read())
            {
                Payment payment = new Payment();

                payment.PaymentID = (int)reader["PaymentId"];
                Student student = new Student { StudentID = (int)reader["StudentId"] };
                payment.Student = student;
                payment.PaymentDate = (DateTime)reader["PaymentDate"];
                payment.Amount = (decimal)reader["Amount"];
                paymentList.Add(payment);
            }


            sqlconnection.Close();
            return paymentList;
        }
        #endregion
        #region EnrollInCourse
        public void EnrollInCourse(Course course, int studentId)
        {
            try
            {
                cmd.CommandText = "INSERT INTO enrollments VALUES (@StudentId, @CourseId,GetDate());";
                sqlconnection.Open();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@student_id", studentId);
                cmd.Parameters.AddWithValue("@course_id", course.CourseID);
                cmd.Connection = sqlconnection;

                int addEnrollmentStatus = cmd.ExecuteNonQuery();
                sqlconnection.Close();
                if (addEnrollmentStatus > 0)
                {
                    Console.WriteLine("Enrolled Successfully");
                }
                else
                {
                    Console.WriteLine("Error.Enrollment unsuccessful.");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            sqlconnection.Close();

        }
        #endregion
        #region UpdateStudentInfo
        public void UpdateStudentInfo(int student_id, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            try
            {
                cmd.CommandText = "UPDATE Students SET FirstName = @firstname,LastName=@lastname,Email=@email,DateOfBirth=@dateOfBirth,PhoneNumber=@phoneNumber WHERE StudentId=@student_id";

                sqlconnection.Open();
                cmd.Connection = sqlconnection;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@student_id", student_id);
                cmd.Parameters.AddWithValue("@firstname", firstName);
                cmd.Parameters.AddWithValue("@lastname", lastName);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
                cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                int updateStudentStatus = cmd.ExecuteNonQuery();
                sqlconnection.Close();

                if (updateStudentStatus > 0)
                {
                    Console.WriteLine("Student Updated successfully.");
                }
                else
                {
                    sqlconnection.Close();
                    throw new StudentNotFoundException($"Student with id {student_id}");
                }
                sqlconnection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        #endregion
        #region MakePayment
        public void MakePayment(int student_id, decimal amount)
        {
            try
            {

                cmd.CommandText = "INSERT INTO Payments VALUES (@student_id, @amount,getdate());";
                sqlconnection.Open();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@student_id", student_id);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Connection = sqlconnection;

                int makePaymentStatus = cmd.ExecuteNonQuery();
                sqlconnection.Close();
                if (makePaymentStatus > 0)
                {
                    Console.WriteLine("Payment made Successfully");
                }
                else
                {
                    sqlconnection.Close();
                    throw new StudentNotFoundException($"Student with id {student_id} n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"({ex.Message})");
            }
            sqlconnection.Close();

        }
        #endregion
        #region UpdateCourseInfo
        public void UpdateCourseInfo(int course_id, int credits, string courseName, int teacher_id)
        {
            cmd.CommandText = "UPDATE Courses SET CourseName = @courseName,Credits=@credits,TeacherId=@teacher_id WHERE CourseId=@course_id";

            sqlconnection.Open();
            cmd.Connection = sqlconnection;

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@courseName", courseName);
            cmd.Parameters.AddWithValue("@credits", credits);
            cmd.Parameters.AddWithValue("@teacher_id", teacher_id);
            cmd.Parameters.AddWithValue("@course_id", course_id);
            int updateCourseStatus = cmd.ExecuteNonQuery();
            sqlconnection.Close();

            if (updateCourseStatus > 0)
            {
                Console.WriteLine("Course Updated successfully.");
            }
            else
            {
                sqlconnection.Close();
                throw new CourseNotFoundException($"Course with id {course_id} not found.");
            }
            sqlconnection.Close();


        }
        #endregion
        #region GetTeacher
        public Teacher GetTeacher(int course_id)
        {
            Teacher teacher = new Teacher();

            cmd.CommandText = "select * from Teacher where TeacherId=(select TeacherId from Courses where CourseId=@course_id)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@course_id", course_id);
            cmd.Connection = sqlconnection;
            sqlconnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                sqlconnection.Close();
                throw new CourseNotFoundException($"Course with id {course_id} not found.");
            }
            while (reader.Read())
            {
                teacher.TeacherID = (int)reader["TeacherId"];
                teacher.FirstName = (string)reader["FirstName"];
                teacher.LastName = (string)reader["LastName"];
                teacher.Email = (string)reader["Email"];
            }

            sqlconnection.Close();
            return teacher;
        }
        #endregion
        #region GetCourse
        public Course GetCourse(int enrollment_id)
        {
            Course course = new Course();
            cmd.CommandText = "select * from Courses where CourseId  = (select distinct CourseId from Enrollments where EnrollmentId=@enrollment_id)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@enrollment_id", enrollment_id);
            cmd.Connection = sqlconnection;
            sqlconnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                sqlconnection.Close();
                throw new InvalidEnrollmentDataException($"Enrollment with id {enrollment_id} not found.");
            }
            while (reader.Read())
            {
                course.CourseID = (int)reader["CourseId"];
                course.CourseName = (string)reader["CourseName"];
                course.Credits = (int)reader["Credits"];
                int course_id = (int)reader["CourseId"];
                sqlconnection.Close();
                cmd.CommandText = "select FirstName,LastName from teacher where TeacherId = (select distinct TeacherId from Courses where CourseId=@course_id)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@course_id", course_id);
                cmd.Connection = sqlconnection;
                sqlconnection.Open();

                SqlDataReader reader1 = cmd.ExecuteReader();
                string name;
                while (reader1.Read())
                {
                    name = (string)reader1["FirstName"] + " " + (string)reader1["LastName"];
                    course.InstructorName = name;
                }

            }

            sqlconnection.Close();
            return course;
        }
        #endregion
        #region GetPaymentDate
        public DateTime GetPaymentDate(int payment_id)
        {
            DateTime date = DateTime.Now;

            try
            {
                cmd.CommandText = "select PaymentDate from Payments where PaymentId=@payment_id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@payment_id", payment_id);
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    date = (DateTime)reader["PaymentDate"];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error Occured:{ex.Message}");
                sqlconnection.Close();
            }
            sqlconnection.Close();
            return date;
        }
        #endregion
        #region GetPaymentAmount
        public decimal GetPaymentAmount(int payment_id)
        {
            decimal amount = 0.0m;
            try
            {
                cmd.CommandText = "select Amount from Payments where PaymentId=@payment_id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@payment_id", payment_id);
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    amount = (decimal)reader["Amount"];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error Occured:{ex.Message}");
                sqlconnection.Close();
            }
            sqlconnection.Close();
            return amount;
        }
        #endregion
        #region GenerateEnrollmentReport
        public void GenerateEnrollmentReport(Course course)
        {
            int course_id = course.CourseID;


            cmd.CommandText = "select * from Students where StudentId in (select StudnetId from Enrollments where CourseId=@course_id)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@course_id", course_id);
            cmd.Connection = sqlconnection;
            sqlconnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                sqlconnection.Close();
                throw new CourseNotFoundException($"Course with id {course_id} not found.");
            }
            while (reader.Read())
            {
                Console.WriteLine($"Student ID:{(int)reader["StudentId"]}\tFirst Name:{(string)reader["FirstName"]}\tLast Name:{(string)reader["LastName"]}\tDate of Birth:{(DateTime)reader["DateOfBirth"]}\tEmail:{(string)reader["Email"]}\tPhone Number:{(string)reader["PhoneNumber"]}");
            }
            sqlconnection.Close();
        }
        #endregion
        #region CalculateCourseStatistics
        public void CalculateCourseStatistics(Course course)
        {
            int course_id = course.CourseID;

            try
            {
                cmd.CommandText = "select count(*) as num from Enrollments where CourseId=@course_id group by CourseId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@course_id", course_id);
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Number of enrollments:{(int)reader["num"]}");
                }
                sqlconnection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error Occured:{ex.Message}");
                sqlconnection.Close();
            }
            sqlconnection.Close();
        }
        #endregion
        #region GetStudent
        public Student GetStudent(int enrollment_id)
        {
            Student student = new Student();

            cmd.CommandText = "select * from Students where StudentId = (select StudentId from Enrollments where EnrollmentId=@enrollment_id)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@enrollment_id", enrollment_id);
            cmd.Connection = sqlconnection;
            sqlconnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                sqlconnection.Close();
                throw new InvalidEnrollmentDataException($"Enrollment with id {enrollment_id} not found.");
            }
            while (reader.Read())
            {
                student.StudentID = (int)reader["StudentId"];
                student.FirstName = (string)reader["FirstName"];
                student.LastName = (string)reader["LastName"];
                student.DateOfBirth = (DateTime)reader["DateOfBirth"];
                student.Email = (string)reader["Email"];
                student.PhoneNumber = (string)reader["PhoneNumber"];
            }
            sqlconnection.Close();


            return student;
        }
        #endregion
        #region GetAssignedCourses
        public List<Course> GetAssignedCourses(int teacher_id)
        {
            List<Course> courseList = new List<Course>();


            cmd.CommandText = "select * from Courses where TeacherId=@teacher_id";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@teacher_id", teacher_id);
            cmd.Connection = sqlconnection;
            sqlconnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                sqlconnection.Close();
                throw new TeacherNotFoundException($"Teacher with id {teacher_id} not found.");
            }
            while (reader.Read())
            {
                Course course = new Course();

                course.CourseID = (int)reader["CourseId"];
                int course_id = (int)reader["CourseId"];
                course.CourseName = (string)reader["CourseName"];
                course.Credits = (int)reader["Credits"];
                sqlconnection.Close();
                cmd.CommandText = "select FirstName,LastName from teacher where TeacherId = (select distinct TeacherId from Courses where CourseId=@course_id)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@course_id", course_id);
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlDataReader reader1 = cmd.ExecuteReader();
                string name;
                while (reader1.Read())
                {
                    name = (string)reader1["FirstName"] + " " + (string)reader1["LastName"];
                    course.InstructorName = name;
                }

                courseList.Add(course);
            }

            sqlconnection.Close();
            return courseList;

        }
        #endregion
        #region AssignTeacherToCourse
        public void AssignTeacherToCourse(Teacher teacher, Course course)
        {
            try
            {
                cmd.CommandText = "Update Courses SET TeacherId=@teacher_id where CourseId=@course_id;";
                sqlconnection.Open();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@teacher_id", teacher.TeacherID);
                cmd.Parameters.AddWithValue("@course_id", course.CourseID);
                cmd.Connection = sqlconnection;

                int courseAssignStatus = cmd.ExecuteNonQuery();
                sqlconnection.Close();
                if (courseAssignStatus > 0)
                {
                    Console.WriteLine("Course Assigned Successfully");
                }
                else
                {
                    Console.WriteLine("Error.Course Assignment unsuccessful.");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                sqlconnection.Close();

            }
            sqlconnection.Close();


        }
        #endregion


    }
}
