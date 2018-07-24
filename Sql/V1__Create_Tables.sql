USE VUeling

GO

IF OBJECT_ID(N'Vueling.dbo.Client', N'U') IS NULL 
BEGIN

CREATE TABLE dbo.Client
(
	Id [NVARCHAR](36) NOT NULL PRIMARY KEY,
	Name [NVARCHAR](50) NOT NULL,
	Email [NVARCHAR](100) NOT NULL,
	Role [NVARCHAR] (10) NOT NULL
);

CREATE TABLE dbo.Policy
(
	Id [NVARCHAR](36) NOT NULL PRIMARY KEY,
	AmountInsured decimal NOT NULL,
	Email [NVARCHAR](100) NOT NULL,
	InceptionDate Date NOT NULL,
	InstallmentPayment Bit NOT NULL,
	ClientId [NVARCHAR](36),
	CONSTRAINT fk_ClientId FOREIGN KEY (ClientId) REFERENCES Client (id)
);

END