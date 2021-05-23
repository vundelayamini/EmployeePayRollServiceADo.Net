using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayrollService
{
    class EmployeeRepo 
    {
        public static string ConnectionString = @"server=(localdb)\MSSQLLocaldb(SQL server 13.0.4001-DESKTOP2LF5QN8; Intitially catlog=payroll_service; UserID=Yamini; password=yamini448";
        SqlConnection connection = new SqlConnection(ConnectionString);
        private readonly object displayModel;

        public void GetAllEmployees()
        {
            try
            { 
                EmployeeModel model = new EmployeeModel();
                using (this.connection)
                {
                    string query = @"select* from dbo.employee_payroll";
                    //Define the sqlCommand object
                    SqlCommand command = new SqlCommand(query, connection);

                    this.connection.Open();

                    SqlDataReader reader = command.ExecuteReader();//Execute sql data reader 

                    if (reader.HasRows)//if checking data reader has rows or not
                    {
                        while (reader.Read())//using while loop for read multiple rows
                        {
                            model.EmployeeID = reader.GetInt32(0);
                            model.EmployeeName = reader.GetString(1);
                            model.BasicPay = reader.GetDouble(2);
                            model.StartDate = reader.GetDateTime(3);
                            model.Gender = reader.GetString(4);
                            model.PhoneNumber = reader.GetString(6);
                            model.Address = reader.GetString(7);
                            model.Department = reader.GetString(8);
                            model.Deduction = reader.GetDouble(9);
                            model.TaxablePay = reader.GetDouble(10);
                             model.NetPay = reader.GetDouble(11);
                             model.Tax = reader.GetDouble(12);
                            //dispay retrived record
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}", model.EmployeeID, model.EmployeeName, model.BasicPay, model.StartDate, model.Gender, model.Address, model.Department, model.TaxablePay,model.Tax,model.Deduction,model.NetPay);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    //close data reader
                    reader.Close();
                    this.connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.connection.Close();//Always enuring the closing of the command
            }

        
        }
        public bool AddEmployee(EmployeeModel model)
        {
            try
            {
                using(this.connection)
                {
                    SqlCommand command = new SqlCommand("dbo.SpAddEmployeeDetails",this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@name", model.EmployeeName);
                    command.Parameters.AddWithValue("@Base_pay", model.BasicPay);
                    command.Parameters.AddWithValue("@Start", model.StartDate);
                    command.Parameters.AddWithValue("@gender", model.Gender);
                    command.Parameters.AddWithValue("@nphoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@nAddress", model.Address);
                    command.Parameters.AddWithValue("@department", model.Department);
                    command.Parameters.AddWithValue("@deduction", model.Deduction);
                    command.Parameters.AddWithValue("@nTaxable_pay", model.TaxablePay);
                    command.Parameters.AddWithValue("@Netpay", model.NetPay);
                    command.Parameters.AddWithValue("@Tax", model.Tax);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if(result!=0)
                    {
                        return true;

                    }
                    return false;


                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void InsertIntoTwoTables()
        {
            using(connection)
            {
                connection.Open();
                //Enlist a command in the current transaction.
                SqlCommand command = connection.CreateCommand();
                try
                {
                    //Execute two seperate commands.
                    command.CommandText =
                        "Insert into employee_payroll(Name,Address)values('Test2','Banglore')";
                    command.ExecuteScalar();
                    Console.WriteLine("Inserted into Employee table successfuly.");
                    command.CommandText =
                        "Insert into Salary(Salary,EmpId)values(4000,1)";
                    command.ExecuteNonQuery();
                    Console.WriteLine("Inserted into Salary table successfully.");

                }
                catch(Exception ex)
                {
                    //Handle the exception if the transaction fails to commit.
                    Console.WriteLine(ex.Message);
                }
            }
            
        }
        public void InsertIntoTwoTablesWithTransaction()
        {
            using(connection)
            {
                connection.Open();
                //Start a local transaction.
                SqlTransaction sqlTran = connection.BeginTransaction();
                //Enlist a command in the current transaction.
                SqlCommand command = connection.CreateCommand();
                command.Transaction = sqlTran;
                try
                {
                    //Execute two seperate commands.
                    command.CommandText =
                     "Insert into Employee(EmployeeName,JobDiscription)values('TestingRollBack23,'Test23')";
                    command.ExecuteNonQuery();
                    command.CommandText =
                        "Insert into Salary(EmployeeSalary,Month,EmployeeId)values(12345,'june',24)";
                    command.ExecuteNonQuery();
                    //commit the transactions
                    sqlTran.Commit();
                    Console.WriteLine("Both records were written to database");



                }
                catch(Exception ex)
                {
                    //Handle the exception if the transaction fails to commit.
                    Console.WriteLine(ex.Message);
                    try
                    {
                        //Attempt to roll back the transaction.
                        sqlTran.Rollback();
                    }
                    catch(Exception exRollBack)
                    {
                        //Throws an InvalidOperationException if the connection
                        //is closed or the transaction has already been rolled
                        //back on the server.
                        Console.WriteLine(exRollBack.Message);

                    }
                       
                }
            }

        }
        public int UpdateSalary(SalaryUpdateModel model)
        {
            try
            {
                int salary = 0;
                using(this.connection)
                {
                    SalaryDetailsModel detailsModel = new SalaryDetailsModel();
                    SqlCommand command = new SqlCommand("dbo.sqlUpdateSalary1",this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Salary", model.EmployeeSalary);
                    command.Parameters.AddWithValue("@EmpId", model.EmployeeId);

                    connection.Open();
                    SqlDataAdapter reader = command.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            displayModel.EmpId = reader.GetInt32(0);
                            displayModel.EmployeeName = reader["EmployeeName"].ToString();
                            displayModel.JobDiscription = reader["JobDiscription"].ToString();
                            displayModel.Month = reader["Month"].ToString();
                            displayModel.SalaryId = reader.GetInt32(4);
                            displayModel.EmployeeSalary = reader.GetInt32(5);
                            Console.WriteLine("EmployeeId={0}\nEmployeeName={1}\nEmployeeSalary={2}\nMonth={3}\nSalaryId={5}");
                            salary = displayModel.EmployeeSalary;
                        }

                        
                    }
                    else
                    {
                        Console.WriteLine("No data found");

                    }
                    reader.Close();

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

    }

}
