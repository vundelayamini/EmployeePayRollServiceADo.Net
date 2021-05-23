 select *from employee_payroll
select * from salary

select * from  employee_payroll e where  e.Id IN(select id from Salary where EmpId=2);

Create function TotalEmpSalary()
returns int
as
begin
declare @result int
select @result=sum(Salary)from employee_payroll
if(@result is null)
set @result=0;
return @result
end
select dbo.TotalEmpSalary()as SumOfSalary

Alter trigger testTriggerInsert
on employee_payroll
after insert
as
begin
select count(*)from employee_payroll
end

insert into employee_payroll(Name,Address)values('TestTrigger','Hyderabad')


