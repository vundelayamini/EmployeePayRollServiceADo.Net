using System;

namespace EmployeePayrollService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee payroll");
            EmployeeRepo repo = new EmployeeRepo();
            EmployeeModel model = new EmployeeModel();

            model.EmployeeID = 158;
            model.EmployeeName = "Yamini";
            model.PhoneNumber = "9014066209";
            model.Address = "3-161-21";
            model.Department = "IT";
            model.Gender = "M";
            model.NetPay = 25000.00;
            model.BasicPay = 22000.00;
            model.Deduction = 1500.00;
            model.TaxablePay = 200.00;
            model.City = "Banglore";
            model.Country = "India";
            sqlUpdateModel update = new sqlUpdateModel();


                update.EmployeeID = 24;
            update.EmployeeSalary = 84324;
            

            //update.Month = "June";
            //update.SalaryId = "12";
            repo.UpdateSalary(update);
            //repo.GetAllEmployees();
            //repo.AddEmployee(model);
            //repo.InsertIntoTwoTables();
            repo.InsertIntoTwoTablesWithTransaction();

            Console.Read();
        }
    }

  
        
    
}
