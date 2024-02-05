using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.model
{
    internal class Payment
    {
       
        int paymentID;
        decimal amount;
        DateTime paymentDate;
        Student student;

        public int PaymentID { get; set; }
        public Student Student { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public Payment()
        {

        }
        public Payment(int paymentID, Student student, decimal amount, DateTime paymentDate)
        {
            PaymentID = paymentID;
            Student = student;
            Amount = amount;
            PaymentDate = paymentDate;
        }

        public Student GetStudent()
        {
            return Student;
        }

        public decimal GetPaymentAmount()
        {
            return Amount;
        }

        public DateTime GetPaymentDate()
        {
            return PaymentDate;
        }

    }
}
