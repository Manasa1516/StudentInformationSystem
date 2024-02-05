using StudentInformationSystem;
using StudentInformationSystem.Exceptions;
using StudentInformationSystem.model;
using StudentInformationSystem.Repository;
using StudentInformationSystem.Service;

    IStudentInformationSystemService sisService = new StudentInformationSystemService();

    string menu = "\n Press1:: Student Management \n Press2::Course Management \n Press3::Enrollment Management \n Press4::Teacher Management \n Press5::Payment Management \n Press6:: SIS Management";
    Console.WriteLine(" Welcome To Our Student Management System choose from the Below options To continue");
    Console.WriteLine(menu);
    Console.WriteLine("Enter your choice");
    int choice = int.Parse(Console.ReadLine());
    Console.Clear();
    switch (choice)
    {
        case 1:
            string menu1 = "\n Press1::EnrollInCourse \n Press2::UpdateStudentInfo \n Press3::MakePayment \n Press4::DisplayStudentInfo\n Press5::GetEnrolledCourses\n Press6::GetPaymentHistory";
            Console.WriteLine(menu1);
            Console.WriteLine("Enter your choice");
            int choice1 = int.Parse(Console.ReadLine());
            Console.Clear();
            switch (choice1)
            {
                case 1:
                    sisService.EnrollInCourseS();
                    break;
                case 2:
                    sisService.UpdateStudentInfoS();
                    break;
                case 3:
                    sisService.MakePaymentS();
                    break;
                case 4:
                    sisService.DisplayStudentInfoS();
                    break;
                case 5:
                    sisService.GetEnrolledCoursesS();
                    break;
                case 6:
                    sisService.GetPaymentHistoryS();
                    break;

                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
            break;
        case 2:
            string menu2 = "\n Press1:: AssignTeacherToCourse \n Press2::UpdateCourseInfo \n Press3::DisplayCourseInfo \n Press4::GetTeacher";
            Console.WriteLine(menu2);
            Console.WriteLine("Enter your choice");
            int choice2 = int.Parse(Console.ReadLine());
            Console.Clear();
            switch (choice2)
            {
                case 1:
                    sisService.AssignTeacherToCourseS();
                    break;
                case 2:
                    sisService.UpdateCourseInfoS();
                    break;
                case 3:
                    sisService.DisplayCourseInfoS();
                    break;
                case 4:
                    sisService.GetTeacherS();
                    break;
                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
            break;
        case 3:
            string menu3 = "\n Press1:: GetStudent \n Press2::GetCourse ";
            Console.WriteLine(menu3);
            Console.WriteLine("Enter your choice");
            int choice3 = int.Parse(Console.ReadLine());
            Console.Clear();
            switch (choice3)
            {
                case 1:
                    sisService.GetStudentS();
                    break;
                case 2:
                    sisService.GetCourseS();
                    break;
                default:
                    Console.WriteLine("Invalid Choice");
                    break;

            }
            break;
        case 4:
            string menu4 = "\n Press1::UpdateTeacherInfoS \n Press2::DisplayTeacherInfoS\n Press3::GetAssignedCourses";
            Console.WriteLine(menu4);
            Console.WriteLine("Enter your choice");
            int choice4 = int.Parse(Console.ReadLine());
            Console.Clear();
            switch (choice4)
            {
                case 1:
                    sisService.UpdateTeacherInfoS();
                    break;
                case 2:
                    sisService.DisplayTeacherInfoS();
                    break;
                case 3:
                    sisService.GetAssignedCoursesS();
                    break;
                default:
                    Console.WriteLine("Invalid Choice");
                    break;

            }
            break;
        case 5:
            string menu5 = "\n Press1:: GetStudent \n Press2::GetPaymentAmount \n Press3::sisService.GetPaymentDate";
            Console.WriteLine(menu5);
            Console.WriteLine("Enter your choice");
            int choice5 = int.Parse(Console.ReadLine());
            Console.Clear();
            switch (choice5)
            {
                case 1:
                    sisService.GetStudentS();
                    break;
                case 2:
                    sisService.GetPaymentAmountS();
                    break;
                case 3:
                    sisService.GetPaymentDateS();
                    break;
                default:
                    Console.WriteLine("Invalid Choice");
                    break;

            }
            break;
        case 6:
            string menu6 = "\n Press1::EnrollInCourse \n Press2::AssignTeacherToCourse\n Press3::MakePayment\n Press4::GenerateEnrollmentReportS\n Press5::CalculateCourseStatistics";
            Console.WriteLine(menu6);
            Console.WriteLine("Enter your choice");
            int choice6 = int.Parse(Console.ReadLine());
            Console.Clear();
            switch (choice6)
            {
                case 1:
                    sisService.EnrollInCourseS();
                    break;
                case 2:
                    sisService.AssignTeacherToCourseS();
                    break;
                case 3:
                    sisService.MakePaymentS();
                    break;
                case 4:
                    sisService.GenerateEnrollmentReportS();
                    break;
                case 5:
                    sisService.CalculateCourseStatisticsS();
                    break;
                default:
                    Console.WriteLine("Invalid Choice");
                    break;

            }
            break;
        default:
            Console.WriteLine("Invalid Choice");
            break;

    }

