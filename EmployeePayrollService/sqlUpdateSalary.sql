Alter PROCEDURE dbo.spUpdateSalary
@id int,
@salary int,
@month varchar (20),
@EmpId int

As
BEGIN
SET XACT_ABORT ON;
BEGIN TRY
BEGIN TRANSACTION;
Update Salary
set EmployeeSalary= @salary
where SalaryId3=@id and Month=@month and EmployeeId=@EmpId;
Select e.EmployeeId,e.EmployeeName,e.JobDiscription,s.Month,s.SalaryId,s.EmployeeSalary
from employee e inner join Salary s
ON e.EmployeeId=s.SalaryId where s.SalaryId=@Id;
COMMIT TRANSACTION;
END TRY
BEGIN CATCH
select ERROR_NUMBER()As ErrorNumber,ERROR_MESSAGE()AS ErrorMessage;
if(XACT_STATE())=-1
BEGIN
PRINT N'The transaction is in an uncommitable state.'+'Rolling back transcation.'
ROLLBACK TRANSACTION;
END;
if(XACT_STATE())=-1
BEGIN
PRINT N'The transaction is commitable state.'+' commiting transaction.'
COMMIT TRANSACTION;
END;
END CATCH
END



