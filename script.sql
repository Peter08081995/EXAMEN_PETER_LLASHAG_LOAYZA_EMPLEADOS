GO
CREATE DATABASE [Prueba]
GO
USE [Prueba]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 2/18/2021 10:18:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Employee_name] [varchar](50) NULL,
	[Employee_salary] [int] NULL,
	[Employee_age] [int] NULL,
	[Profile_image] [varchar](100) NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[InsertEmployee]    Script Date: 2/18/2021 10:18:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE   PROCEDURE [dbo].[InsertEmployee]
	@p_Employee_name VARCHAR(50),
	@p_Employee_age INT,
    @p_Employee_salary INT,
    @p_Profile_image VARCHAR(100)
AS

insert into Employee 
(
Employee_name,
Employee_salary,
Employee_age,
Profile_image
)

values(
@p_Employee_name,
@p_Employee_salary,
@p_Employee_age,
@p_Profile_image
)
GO
