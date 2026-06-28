CREATE TRIGGER [dbo].[TR_Employee_Insert_CreateCompany]
    ON [dbo].[Employee]
    AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Distinct new company+address combos that don't already exist
    DECLARE @ToCreate TABLE
    (
        RowKey      INT IDENTITY(1,1) PRIMARY KEY,
        CompanyName NVARCHAR(20),
        Street      NVARCHAR(50),
        City        NVARCHAR(20),
        State       NVARCHAR(50),
        ZipCode     NVARCHAR(50)
    );

    INSERT INTO @ToCreate (CompanyName, Street, City, State, ZipCode)
    SELECT DISTINCT
        LEFT(i.[CompanyName], 20),
        a.[Street], a.[City], a.[State], a.[ZipCode]
    FROM inserted AS i
        INNER JOIN [dbo].[Address] AS a ON a.[Id] = i.[AddressId]
    WHERE NOT EXISTS
    (
        SELECT 1
        FROM [dbo].[Company]  AS c
        INNER JOIN [dbo].[Address] AS ca ON ca.[Id] = c.[AddressId]
        WHERE c.[Name] = LEFT(i.[CompanyName], 20)
          AND ISNULL(ca.[Street], '')  = ISNULL(a.[Street], '')
          AND ISNULL(ca.[City], '')    = ISNULL(a.[City], '')
          AND ISNULL(ca.[State], '')   = ISNULL(a.[State], '')
          AND ISNULL(ca.[ZipCode], '') = ISNULL(a.[ZipCode], '')
    );

    -- Set-based address copy with old->new mapping
    DECLARE @AddressMap TABLE (RowKey INT, NewAddressId INT);

    MERGE INTO [dbo].[Address] AS tgt
    USING @ToCreate AS src
    ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT ([Street], [City], [State], [ZipCode])
        VALUES (src.[Street], src.[City], src.[State], src.[ZipCode])
    OUTPUT src.RowKey, inserted.[Id]
    INTO @AddressMap (RowKey, NewAddressId);

    -- Create companies attached to the new addresses
    INSERT INTO [dbo].[Company] ([Name], [AddressId])
    SELECT t.CompanyName, m.NewAddressId
    FROM @ToCreate AS t
        INNER JOIN @AddressMap AS m ON m.RowKey = t.RowKey;
END;