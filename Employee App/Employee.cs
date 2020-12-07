using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_App
{
    public abstract class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Employee(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }
        public override string ToString()
        {
            return string.Format($"{LastName}, {FirstName}");
        }
        public abstract decimal CalculateMonthlyPay();
        
    }
    public class FullTimeEmployee : Employee
    {
        public decimal Salary { get; set; }
        public FullTimeEmployee(string firstname, string lastname, decimal salary) : base(firstname, lastname)
        {
            Salary = salary;
        }

        public override string ToString()
        {
            return string.Format($"{LastName.ToUpper()}, {FirstName} - Full Time");
        }

        public override decimal CalculateMonthlyPay()
        {
            return Salary / 12;
        }
    }
    public class PartTimeEmployee : Employee
    {
        public decimal HourlyRate { get; set; }
        public double HoursWorked { get; set; }
        public PartTimeEmployee(string firstname, string lastname, decimal hourlyrate, double hoursworked) : base(firstname, lastname) 
        {
            HourlyRate = hourlyrate;
            HoursWorked = hoursworked;
        }
        public override string ToString()
        {
            return string.Format($"{LastName.ToUpper()}, {FirstName} - Part Time");
        }

        public override decimal CalculateMonthlyPay()
        {
            return HourlyRate * (decimal)HoursWorked;
        }
    }
}
