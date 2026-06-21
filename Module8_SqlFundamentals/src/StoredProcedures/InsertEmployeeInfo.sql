CREATE PROCEDURE [dbo].[InsertEmployeeInfo]
    @EmployeeName  NVARCHAR(100) = NULL,   -- optional
    @FirstName     NVARCHAR(50)  = NULL,   -- optional
    @LastName      NVARCHAR(50)  = NULL,   -- optional
    @CompanyName   NVARCHAR(100),          -- required (wide param so truncation is meaningful)
    @Position      NVARCHAR(30)  = NULL,   -- optional
    @Street        NVARCHAR(50),           -- required
    @City          NVARCHAR(20)  = NULL,   -- optional
    @State         NVARCHAR(50)  = NULL,   -- optional
    @ZipCode       NVARCHAR(50)  = NULL,   -- optional
    @NewEmployeeId INT = NULL OUTPUT        -- returns new Employee.Id
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -------------------------------------------------------------------
    -- 1. VALIDATION
    -------------------------------------------------------------------

    -- Condition 1: at least one name field must be non-null/non-empty/non-whitespace
    IF NULLIF(LTRIM(RTRIM(COALESCE(@EmployeeName, ''))), '') IS NULL
       AND NULLIF(LTRIM(RTRIM(COALESCE(@FirstName, ''))), '') IS NULL
       AND NULLIF(LTRIM(RTRIM(COALESCE(@LastName, ''))), '') IS NULL
    BEGIN
        THROW 50001, 'At least one of EmployeeName, FirstName, or LastName must contain a non-empty value.', 1;
    END;

    -- Required: CompanyName
    IF NULLIF(LTRIM(RTRIM(COALESCE(@CompanyName, ''))), '') IS NULL
    BEGIN
        THROW 50002, 'CompanyName is required.', 1;
    END;

    -- Required: Street
    IF NULLIF(LTRIM(RTRIM(COALESCE(@Street, ''))), '') IS NULL
    BEGIN
        THROW 50003, 'Street is required.', 1;
    END;

    -------------------------------------------------------------------
    -- 2. NORMALIZE INPUTS
    -------------------------------------------------------------------
    DECLARE @NormalizedEmployeeName NVARCHAR(100) = NULLIF(LTRIM(RTRIM(@EmployeeName)), '');
    DECLARE @NormalizedFirstName    NVARCHAR(50)  = COALESCE(NULLIF(LTRIM(RTRIM(@FirstName)), ''), '');
    DECLARE @NormalizedLastName     NVARCHAR(50)  = COALESCE(NULLIF(LTRIM(RTRIM(@LastName)),  ''), '');

    -- Condition 2: truncate CompanyName to 20 symbols
    DECLARE @NormalizedCompanyName  NVARCHAR(20)  = LEFT(LTRIM(RTRIM(@CompanyName)), 20);

    DECLARE @NormalizedStreet       NVARCHAR(50)  = LTRIM(RTRIM(@Street));
    DECLARE @NormalizedPosition     NVARCHAR(30)  = NULLIF(LTRIM(RTRIM(@Position)), '');
    DECLARE @NormalizedCity         NVARCHAR(20)  = NULLIF(LTRIM(RTRIM(@City)),     '');
    DECLARE @NormalizedState        NVARCHAR(50)  = NULLIF(LTRIM(RTRIM(@State)),    '');
    DECLARE @NormalizedZipCode      NVARCHAR(50)  = NULLIF(LTRIM(RTRIM(@ZipCode)),  '');

    DECLARE @AddressId INT;
    DECLARE @PersonId  INT;

    -------------------------------------------------------------------
    -- 3. INSERT (atomic transaction)
    -------------------------------------------------------------------
    BEGIN TRY
        BEGIN TRANSACTION;

        -- 3a. Person
        INSERT INTO [dbo].[Person] ([FirstName], [LastName])
        VALUES (@NormalizedFirstName, @NormalizedLastName);

        SET @PersonId = CAST(SCOPE_IDENTITY() AS INT);

        -- 3b. Address
        INSERT INTO [dbo].[Address] ([Street], [City], [State], [ZipCode])
        VALUES (@NormalizedStreet, @NormalizedCity, @NormalizedState, @NormalizedZipCode);

        SET @AddressId = CAST(SCOPE_IDENTITY() AS INT);

        -- 3c. Employee
        INSERT INTO [dbo].[Employee]
            ([AddressId], [PersonId], [CompanyName], [Position], [EmployeeName])
        VALUES
            (@AddressId, @PersonId, @NormalizedCompanyName, @NormalizedPosition, @NormalizedEmployeeName);

        SET @NewEmployeeId = CAST(SCOPE_IDENTITY() AS INT);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;  -- re-raise original error
    END CATCH;
END;