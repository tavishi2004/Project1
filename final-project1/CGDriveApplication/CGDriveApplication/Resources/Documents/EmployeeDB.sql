create database EmployeeDB
Go
/* Master Table*/
create table Emp_MaritalStatus( Status_id int not null primary key identity(1,1),
Marital_Status varchar(20) not null,
Created_By varchar(30) not null,
Created_At datetime not null default(GETDATE()),
Updated_By varchar(30) null,
Updated_At datetime null,
Is_Active bit default 1,
Is_Deleted bit default 0)

/*Employee Information Table*/

create table Emp_info(emp_id int not null primary key identity(1,1),
First_Name varchar(50) not null,
Middle_Name varchar(50) null,
Last_Name varchar(50) null,
Age int null,
DOB date not null,
Phone_No varchar(12) null,
Mobile_No bigint not null,
Email_id varchar(100) not null unique,
Created_By varchar(30) not null,
Created_At datetime not null default(GETDATE()),
Updated_By varchar(30) null,
Updated_At datetime null,
Is_Active bit default 1,
Is_Deleted bit default 0,
Marital_Status int not null default 1,
foreign key (Marital_status) references Emp_MaritalStatus(Status_id))
GO

use[EmployeeDB]
go
/* STORED PROCEDURE TO INSERT*/
create procedure prc_Insert_Marital_Status
@Marital_Status varchar(20),
@Created_By varchar(30),
@Updated_By varchar(30)=null,
@Updated_At datetime=null,
@Is_Active bit=1,
@Is_Deleted bit=0
AS
Begin
Set NOCOUNT ON;
INSERT INTO Emp_MaritalStatus
(Marital_Status,Created_By,Updated_By,Updated_At,Is_Active,Is_Deleted)
VALUES
(@Marital_Status,
@Created_By,
@Updated_By,
@Updated_At,
@Is_Active,
@Is_Deleted)
end
Go
EXEC prc_Insert_Marital_Status @Marital_Status='Unmarried',@created_By='Shaurya'
GO
/* STORED PROCEDURE TO GET*/
create proc prc_get_Marital_Status
@Status_id int
as
begin
SET NOCOUNT ON;
select Status_id,Marital_Status,
case when 
          Is_Active=1 then 'Active'
		  else 'Inactive' 
		  end 'Status'
		  from  Emp_MaritalStatus where Status_id=@Status_id
end
GO
EXEC prc_get_Marital_Status @Status_id=1
Go



/* STORED PROCEDURE TO UPDATE*/
create proc prc_Update_Marital_Statuss
@Status_id int,
@Updated_By varchar(30),
@Is_Active bit
as
begin
set nocount on;
Update Emp_MaritalStatus set Updated_By=@Updated_By,
Updated_At=GETDATE(),Is_Active=@Is_Active where Status_id=@Status_id
end
GO
Exec prc_Update_Marital_Statuss @Updated_By='Shaurya',@Is_Active=1,@Status_Id=1
Go
/* STORED PROCEDURE TO DELETE*/
create proc prc_Delete_Marital_Status
@Status_Id int
as
begin
set NOCOUNT on;
update emp_maritalStatus set Is_Deleted=1 where Status_Id=@Status_Id
end
go
exec prc_Delete_Marital_Status @Status_Id=1
Go
/* STORED PROCEDURE To INSERT Record */
create procedure prc_Insert_Employee1
@First_Name varchar(50),
@Middle_Name varchar(50)=null,
@Last_Name varchar(50)=null,
@Age int=null,
@DOB date,
@Phone_No varchar(12)=null,
@Mobile_No bigint,
@Email_id varchar(100),
@Marital_Status int,
@Created_By varchar(30),
@Updated_By varchar(30)=null,
@Updated_At datetime=null,
@Is_Active bit=1,
@Is_Deleted bit=0
AS
Begin
Set NOCOUNT ON;
INSERT INTO Emp_info
(First_Name, Middle_Name, Last_Name, Age, DOB, Phone_No, Mobile_No, Marital_Status, Email_id,Created_By,Updated_By,Updated_At,Is_Active,Is_Deleted)
VALUES
(@First_Name,@Middle_Name,@Last_Name,@age,@DOB,@Phone_No,@Mobile_No,@Marital_Status,@Email_id,@Created_By,
@Updated_By,
@Updated_At,
@Is_Active,
@Is_Deleted)
end
Go

EXEC prc_Insert_Employee1 @First_Name='Shaurya',
@Last_Name='Sethi',
@Age=21,
@DOB='2002-09-11',
@Mobile_No=9876543210,
@Marital_Status=2,
@email_id='shaurya.sethi@gmail.com',
@Created_By='Shaurya';
Go
/* STORED PROCEDURE To FETCH Records*/
create procedure prc_GetEmployee
 @emp_id int
as
begin
Set NOCOUNT ON;
select emp_id,First_Name,Middle_Name,Last_Name,Age,DOB,Phone_No,Mobile_No,Email_id,b.Marital_Status,case
      when a.Is_Active=1 then 'Active'
	  else 'Inactive' 
	  end 'Status' from Emp_info as a inner join emp_maritalStatus as b on b.Status_id=a.Marital_Status
	   where emp_id = @emp_id 
end
Go
EXEC prc_GetEmployee @emp_id=2
Go
/* STORED PROCEDURE To UPDATE Record*/
create procedure prc_UpdateEmployee
@emp_id int,
@Mobile_No bigint,
@Marital_Status int,
@Updated_By varchar(30),
@Is_Active bit
As
begin
Set NOCOUNT ON;
Update Emp_info SET Mobile_No=@Mobile_No,Marital_Status=@Marital_Status,Updated_By=@Updated_By,Updated_At=GETDATE(),Is_Active=@Is_Active where emp_id=@emp_id
end
Go
EXEC prc_UpdateEmployee @Mobile_No=65230987,@Marital_Status=2,@Updated_By='Shaurya',@Is_Active=1,@emp_id=1
Go



/* STORED PROCEDURE To DELETE Record */
create procedure prc_deleteEmployee
@emp_id int
as
begin
Set NOCOUNT ON;
update Emp_info set Is_Deleted=1 where emp_id=@emp_id
end
Go
Exec prc_deleteEmployee @emp_id=1
Go
