CREATE TRIGGER [dbo].[TR_Employee_Insert_CreateCompany]
ON [dbo].[Employee]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CompanyName NVARCHAR(20);
    DECLARE @Street NVARCHAR(50);
    DECLARE @City NVARCHAR(20);
    DECLARE @State NVARCHAR(50);
    DECLARE @ZipCode NVARCHAR(50);
    DECLARE @NewAddressId INT;

    DECLARE company_cursor CURSOR LOCAL FAST_FORWARD FOR
        SELECT DISTINCT
            LEFT(i.[CompanyName], 20) AS [CompanyName],
            a.[Street],
            a.[City],
            a.[State],
            a.[ZipCode]
        FROM inserted AS i
        INNER JOIN [dbo].[Address] AS a ON a.[Id] = i.[AddressId]
        WHERE NOT EXISTS
        (
            SELECT 1
            FROM [dbo].[Company] AS c
            INNER JOIN [dbo].[Address] AS ca ON ca.[Id] = c.[AddressId]
            WHERE c.[Name] = LEFT(i.[CompanyName], 20)
              AND ISNULL(ca.[Street], '') = ISNULL(a.[Street], '')
              AND ISNULL(ca.[City], '') = ISNULL(a.[City], '')
              AND ISNULL(ca.[State], '') = ISNULL(a.[State], '')
              AND ISNULL(ca.[ZipCode], '') = ISNULL(a.[ZipCode], '')
        );

    OPEN company_cursor;

    FETCH NEXT FROM company_cursor INTO @CompanyName, @Street, @City, @State, @ZipCode;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        INSERT INTO [dbo].[Address] ([Street], [City], [State], [ZipCode])
        VALUES (@Street, @City, @State, @ZipCode);

        SET @NewAddressId = CAST(SCOPE_IDENTITY() AS INT);

        INSERT INTO [dbo].[Company] ([Name], [AddressId])
        VALUES (@CompanyName, @NewAddressId);

        FETCH NEXT FROM company_cursor INTO @CompanyName, @Street, @City, @State, @ZipCode;
    END;

    CLOSE company_cursor;
    DEALLOCATE company_cursor;
END;