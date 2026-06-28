/*
    Post-Deployment Script
    ----------------------
    Seeds tables once. Uses SET IDENTITY_INSERT because IDs are referenced
    by foreign keys and must be deterministic.
*/

-- ============================================
-- Address
-- ============================================
IF NOT EXISTS (SELECT 1 FROM [dbo].[Address])
BEGIN
    SET IDENTITY_INSERT [dbo].[Address] ON;

    INSERT INTO [dbo].[Address] ([Id], [Street], [City], [State], [ZipCode])
    VALUES
        (1, '123 Main St',    'New York',    'NY', '10001'),
        (2, '456 Oak Ave',    'Los Angeles', 'CA', '90001'),
        (3, '789 Pine Rd',    'Chicago',     'IL', '60601'),
        (4, '321 Maple Dr',   'Houston',     'TX', '77001'),
        (5, '654 Cedar Ln',   'Phoenix',     'AZ', '85001'),
        (6, '987 Birch Blvd', 'Seattle',     'WA', '98101');

    SET IDENTITY_INSERT [dbo].[Address] OFF;
END;
GO

-- ============================================
-- Person
-- ============================================
IF NOT EXISTS (SELECT 1 FROM [dbo].[Person])
BEGIN
    SET IDENTITY_INSERT [dbo].[Person] ON;

    INSERT INTO [dbo].[Person] ([Id], [FirstName], [LastName])
    VALUES
        (1, 'John',    'Smith'),
        (2, 'Jane',    'Doe'),
        (3, 'Michael', 'Johnson'),
        (4, 'Emily',   'Williams'),
        (5, 'David',   'Brown');

    SET IDENTITY_INSERT [dbo].[Person] OFF;
END;
GO

-- ============================================
-- Company
-- ============================================
IF NOT EXISTS (SELECT 1 FROM [dbo].[Company])
BEGIN
    SET IDENTITY_INSERT [dbo].[Company] ON;

    INSERT INTO [dbo].[Company] ([Id], [Name], [AddressId])
    VALUES
        (1, 'TechCorp',     1),
        (2, 'BizSolutions', 2),
        (3, 'DataWorks',    3);

    SET IDENTITY_INSERT [dbo].[Company] OFF;
END;
GO

-- ============================================
-- Employee
-- ============================================
IF NOT EXISTS (SELECT 1 FROM [dbo].[Employee])
BEGIN
    SET IDENTITY_INSERT [dbo].[Employee] ON;

    INSERT INTO [dbo].[Employee]
        ([Id], [AddressId], [PersonId], [CompanyName], [Position], [EmployeeName])
    VALUES
        (1, 1, 1, 'TechCorp',     'Developer',   NULL),
        (2, 2, 2, 'BizSolutions', 'Manager',     'Jane D. Doe'),
        (3, 3, 3, 'DataWorks',    'Analyst',     NULL),
        (4, 4, 4, 'TechCorp',     'QA Engineer', NULL),
        (5, 5, 5, 'BizSolutions', 'Consultant',  'David B. Brown');

    SET IDENTITY_INSERT [dbo].[Employee] OFF;
END;
GO