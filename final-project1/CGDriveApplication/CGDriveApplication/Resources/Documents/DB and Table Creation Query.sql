/* Creating Employees Database*/
CREATE DATABASE Employees

USE Employees


/* Creating Master Table here with the name 'Employee_Marital_Status' */
CREATE table Employee_Marital_Status(
Status_id int not null primary key identity(1,1),
Marital_Status varchar(20) not null,
Created_By varchar(30) not null,
Created_At datetime not null default(GETDATE()),
Updated_By varchar(30) null,
Updated_At datetime null,
Is_Active bit default 1 not null,
Is_Deleted bit default 0 not null
)


/* Creating Employee Information table having name 'employee_info' */


CREATE TABLE employee_info(
emp_id int not null primary key identity(1,1),
FirstName varchar(50) not null,
MiddleName varchar(50) null,
LastName varchar(50) null,
Age int null,
MobileNo bigint not null,
PhoneNo varchar(12) null,
EmailId varchar(100) not null unique,
DOB date not null,
Created_By varchar(30) not null,
Created_At datetime not null default(GETDATE()),
Updated_By varchar(30) null,
Updated_At datetime null,
Is_Active bit default 1 null,
Is_Deleted bit default 0 null,
MaritalStatus_emp int not null default 1,
--Assinging foreign key here
foreign key (Maritalstatus_emp) references Employee_Marital_Status(Status_id)
)
