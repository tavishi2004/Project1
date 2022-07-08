create database CG_Docs
use [CG_Docs]
Go
create table User_table
( User_id int not null Primary key identity(1,1),
UserName varchar(100) not null,
User_Password varchar(100) not null,
Created_At datetime
)
create table Folder
(
Folder_Id int not null Primary Key identity(1,1),
FolderName varchar(100),
F_CreatedBy int ,
F_CreatedAt datetime,
Is_Favourite bit default 0,
Is_Deleted bit default 0
Foreign key (F_CreatedBy) references User_table(User_id)
)
create table Documents
(
Doc_Id int not null Primary Key identity(1,1),
Doc_Name varchar(100) not null,
content_Type varchar(100),
Size int ,
D_CreatedBy int ,
D_CreatedAt datetime,
Fol_doc_Id int not null,
Is_Favourite bit default 0,
Is_Deleted bit default 0,
Foreign key(D_CreatedBy) references User_table(User_id),
Foreign Key(Fol_Doc_Id) references Folder(Folder_Id)
)
