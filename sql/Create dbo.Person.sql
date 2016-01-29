USE [SimpleInjectionExample]
GO

/****** Object: Table [dbo].[Person] Script Date: 1/28/2016 8:19:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Person] (
    [PersonId]  UNIQUEIDENTIFIER NOT NULL,
    [FirstName] VARCHAR (50)     NULL,
    [LastName]  VARCHAR (50)     NULL
);


