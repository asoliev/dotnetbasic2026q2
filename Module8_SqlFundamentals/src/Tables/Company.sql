CREATE TABLE [dbo].[Company]
(
    [Id] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Company] PRIMARY KEY,
    [Name] NVARCHAR(20) NOT NULL,
    [AddressId] INT NOT NULL,
    CONSTRAINT [FK_Company_Address] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address]([Id])
);