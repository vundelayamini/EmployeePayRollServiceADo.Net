using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayrollService
{
    class EmployeeModel //class to map the 
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string PhoneNumber { get; set; }
        public string Department { get; set; }
        public string Gender { get; set; }
        public double BasicPay { get; set; }
        public double Deduction { get; set; }
        public double TaxablePay { get; set; }
        public double Tax { get; set; }
        public double NetPay { get; set; }
        public DateTime StartDate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; internal set; }
    }
}

