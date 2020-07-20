CREATE DATABASE CrucTest;
GO
/****** Object: Table [dbo].[Customers] Script Date: 7/5/2020 12:06:33 AM ******/
USE [CrucTest]
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

CREATE TABLE [dbo].[Customers]
(
    [CustomerId] INT IDENTITY(1, 1) NOT NULL,
    [FirstName] VARCHAR(20) NOT NULL,
    [LastName] VARCHAR(20) NOT NULL,
    [DateOfBirth] DATE NOT NULL,
    [DialCode] SMALLINT NULL,
    [PhoneNumber] BIGINT NULL,
    [Email] VARCHAR(20) NOT NULL,
    [BankAccountNumber] BIGINT NULL,
    [CreateDate] DATETIME2(7) NOT NULL,
    [LastUpdateDate] DATETIME2(7) NULL
);

