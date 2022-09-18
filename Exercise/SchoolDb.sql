USE [master]
GO

/*** Delete db if exist ***/
DROP DATABASE IF EXISTS [Exer3Db]
GO

/*** Create db ***/
CREATE DATABASE [Exer3Db]
GO

/*** Use DB ***/
USE [Exer3Db]
GO

/*** Drop Table ***/
DROP TABLE IF EXISTS [dbo].[School]
GO
/*** Drop Table ***/
DROP TABLE IF EXISTS [dbo].[College]
GO
/*** Drop Table ***/
DROP TABLE IF EXISTS [dbo].[Department]
GO

/*** Create school table ***/
CREATE TABLE [dbo].[School]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] NVARCHAR(50) NOT NULL, 
    [Address] NVARCHAR(50) NOT NULL, 
    [Motto] NVARCHAR(50) NOT NULL, 
    [AverageTuition] DECIMAL(18,2) NOT NULL
)
GO

/** Create college table **/
CREATE TABLE [dbo].[College]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(50) NOT NULL,
    [Dean]  NVARCHAR(50) NOT NULL,
    [Theme] NVARCHAR(20) NOT NULL,
    [SchoolId] INT NOT NULL,
    CONSTRAINT FK_CollegeSchool FOREIGN KEY ([SchoolId]) REFERENCES [dbo].[School](Id)
)
GO


/** Create department table **/
CREATE TABLE [dbo].[Department]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(50) NOT NULL,
    [Chair]  NVARCHAR(50) NOT NULL,
    [CollegeId] INT NOT NULL,
    CONSTRAINT FK_DepartmentCollege FOREIGN KEY ([CollegeId]) REFERENCES [dbo].[College](Id)
)
GO

/** Insert school data **/
SET IDENTITY_INSERT [dbo].[School] ON
INSERT [dbo].[School] ([Id], [Name], [Address], [Motto], [AverageTuition]) 
VALUES (1, 'CIT-U', 'N. Bacalso Ave, Cebu City', 'CCS IS THE BEST COLLEGE', 69.69);

INSERT [dbo].[School] ([Id], [Name], [Address], [Motto], [AverageTuition]) 
VALUES (2, 'USC', 'Somewhere bukid, Cebu City', 'Daghan gwapa ug gwapo', 34.35);
SET IDENTITY_INSERT [dbo].[School] OFF

/** Insert college data **/
SET IDENTITY_INSERT [dbo].[College] ON
INSERT [dbo].[College] ([Id], [Name], [Dean], [Theme], [SchoolId])
VALUES (1, 'College of Computer Studies', 'Maam Dean', 'Orange/Black', 1); 

INSERT [dbo].[College] ([Id], [Name], [Dean], [Theme], [SchoolId])
VALUES (2, 'College of Engineering', 'Maam CEA', 'Maroon/Gold', 1);

INSERT [dbo].[College] ([Id], [Name], [Dean], [Theme], [SchoolId])
VALUES (3, 'College of Business Administration', 'Andrew Tate', 'White', 2);
SET IDENTITY_INSERT [dbo].[College] OFF

/** Insert department data **/
INSERT [dbo].[Department] ([Name], [Chair], [CollegeId])
VALUES ('Computer Science', 'Maam Dean', 1); 

INSERT [dbo].[Department] ([Name], [Chair], [CollegeId])
VALUES ('Information Technology', 'Maam Pantaleon', 1); 

INSERT [dbo].[Department] ([Name], [Chair], [CollegeId])
VALUES ('Computer Engineering', 'Engr. IDK', 2); 

INSERT [dbo].[Department] ([Name], [Chair], [CollegeId])
VALUES ('Marketing', 'Dr. Uy', 3); 



