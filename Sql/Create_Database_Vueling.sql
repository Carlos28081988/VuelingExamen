USE MASTER 
GO
IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'Vueling')
	CREATE DATABASE [Vueling]