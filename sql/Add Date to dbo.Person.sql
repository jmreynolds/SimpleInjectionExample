USE [SimpleInjectionExample]
GO

/****** Object: Table [dbo].[Person] Script Date: 2/1/2016 6:39:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Person];


GO
CREATE TABLE [dbo].[Person] (
    [PersonId]  UNIQUEIDENTIFIER NOT NULL,
    [FirstName] VARCHAR (50)     NULL,
    [LastName]  VARCHAR (50)     NULL,
    [BirthDate] DATE             NULL
);


