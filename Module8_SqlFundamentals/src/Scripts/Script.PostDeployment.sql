INSERT INTO [dbo].[Address] ([Street], [City], [State], [ZipCode])
VALUES
    (N'10 Downing Street', N'London', N'Greater London', N'SW1A 2AA'),
    (N'1600 Pennsylvania Ave NW', N'Washington', N'DC', N'20500'),
    (N'1 Microsoft Way', N'Redmond', N'WA', N'98052');

INSERT INTO [dbo].[Person] ([FirstName], [LastName])
VALUES
    (N'John', N'Doe'),
    (N'Jane', N'Smith'),
    (N'Alex', N'Johnson');

INSERT INTO [dbo].[Company] ([Name], [AddressId])
VALUES
    (N'Contoso', 1),
    (N'Northwind', 2),
    (N'Adventure', 3);

INSERT INTO [dbo].[Employee]
(
    [AddressId],
    [PersonId],
    [CompanyName],
    [Position],
    [EmployeeName]
)
VALUES
    (1, 1, N'Contoso', N'Developer', N'John Doe'),
    (2, 2, N'Northwind', N'Analyst', NULL),
    (3, 3, N'Adventure', NULL, N'Alex J.');