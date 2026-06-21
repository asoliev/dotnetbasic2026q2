CREATE PROCEDURE [dbo].[InsertEmployeeInfo]
    @EmployeeName NVARCHAR(100) = NULL,
    @FirstName NVARCHAR(50) = NULL,
    @LastName NVARCHAR(50) = NULL,
    @CompanyName NVARCHAR(100),
    @Position NVARCHAR(30) = NULL,
    @Street NVARCHAR(50),
    @City NVARCHAR(20) = NULL,
    @State NVARCHAR(50) = NULL,
    @ZipCode NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF NULLIF(LTRIM(RTRIM(COALESCE(@EmployeeName, ''))), '') IS NULL
       AND NULLIF(LTRIM(RTRIM(COALESCE(@FirstName, ''))), '') IS NULL
       AND NULLIF(LTRIM(RTRIM(COALESCE(@LastName, ''))), '') IS NULL
    BEGIN
        THROW 50001, 'At least one of EmployeeName, FirstName, or LastName must contain a non-empty value.', 1;
    END;

    IF NULLIF(LTRIM(RTRIM(COALESCE(@CompanyName, ''))), '') IS NULL
    BEGIN
        THROW 50002, 'CompanyName is required.', 1;
    END;

    IF NULLIF(LTRIM(RTRIM(COALESCE(@Street, ''))), '') IS NULL
    BEGIN
        THROW 50003, 'Street is required.', 1;
    END;

    DECLARE @NormalizedEmployeeName NVARCHAR(100) = NULLIF(LTRIM(RTRIM(@EmployeeName)), '');
    DECLARE @NormalizedFirstName NVARCHAR(50) = COALESCE(NULLIF(LTRIM(RTRIM(@FirstName)), ''), '');
    DECLARE @NormalizedLastName NVARCHAR(50) = COALESCE(NULLIF(LTRIM(RTRIM(@LastName)), ''), '');
    DECLARE @NormalizedCompanyName NVARCHAR(20) = LEFT(LTRIM(RTRIM(@CompanyName)), 20);
    DECLARE @AddressId INT;
    DECLARE @PersonId INT;

    INSERT INTO [dbo].[Person] ([FirstName], [LastName])
    VALUES (@NormalizedFirstName, @NormalizedLastName);

    SET @PersonId = CAST(SCOPE_IDENTITY() AS INT);

    INSERT INTO [dbo].[Address] ([Street], [City], [State], [ZipCode])
    VALUES (@Street, @City, @State, @ZipCode);

    SET @AddressId = CAST(SCOPE_IDENTITY() AS INT);

    INSERT INTO [dbo].[Employee]
    (
        [AddressId],
        [PersonId],
        [CompanyName],
        [Position],
        [EmployeeName]
    )
    VALUES
    (
        @AddressId,
        @PersonId,
        @NormalizedCompanyName,
        @Position,
        @NormalizedEmployeeName
    );
END;