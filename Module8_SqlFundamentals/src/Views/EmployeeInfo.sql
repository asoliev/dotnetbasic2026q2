CREATE VIEW [dbo].[EmployeeInfo]
AS
    SELECT TOP (100) PERCENT
        e.[Id] AS [EmployeeId],

        -- EmployeeName if not null/empty/whitespace, otherwise '{FirstName} {LastName}'
        COALESCE(
            NULLIF(LTRIM(RTRIM(e.[EmployeeName])), ''),
            CONCAT(p.[FirstName], ' ', p.[LastName])
        ) AS [EmployeeFullName],

        -- '{ZipCode}_{State}, {City}-{Street}'  (NULL-safe)
        CONCAT(
            COALESCE(a.[ZipCode], ''), '_',
            COALESCE(a.[State],   ''), ', ',
            COALESCE(a.[City],    ''), '-',
            a.[Street]
        ) AS [EmployeeFullAddress],

        -- '{CompanyName}({Position})'  (NULL-safe)
        CONCAT(e.[CompanyName], '(', COALESCE(e.[Position], ''), ')') AS [EmployeeCompanyInfo]

    FROM [dbo].[Employee] AS e
        INNER JOIN [dbo].[Person]  AS p ON p.[Id] = e.[PersonId]
        INNER JOIN [dbo].[Address] AS a ON a.[Id] = e.[AddressId]
    ORDER BY
        e.[CompanyName] ASC,
        a.[City] ASC;