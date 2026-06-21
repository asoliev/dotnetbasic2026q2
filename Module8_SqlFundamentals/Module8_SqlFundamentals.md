# Module8: Sql Fundamentals

## Task 1:

Create a SQL DB project with the structure of the following tables:

1. Person
     - Id, int, not NULL, PK
     - FirstName, nvarchar(50), not NULL
     - LastName, nvarchar(50), not NULL
2. Address
     - Id, int, not NULL, PK
     - Street, nvarchar(50), not NULL
     - City, nvarchar(20), NULL
     - State, nvarchar(50), NULL
     - ZipCode, nvarchar(50), NULL
3. Employee
    - Id, int, not NULL, PK
    - AddressId, int, not NULL, FK (Address.Id)
    - PersonId, int, not NULL, FK (Person.Id)
    - CompanyName, nvarchar(20), not NULL
    - Position, nvarchar(30), NULL
    - EmployeeName, nvarchar(100), NULL
4. Company
    - Id, int, not NULL, PK
    - Name, nvarchar(20), not NULL
    - AddressId, int, not NULL, FK (Address.Id)
    - Publish DB into SQL Server with default information (create Script.postdeploy*.sql and fill once all tables with the appropriate data)

## Task 2:

Create ‘EmployeeInfo’ view to show data with the following structure that is sorted by ‘CompanyName, City’ ASC:

1. EmployeeId,
2. EmployeeFullName (EmployeeName (if not null) or ‘{FirstName} {LastName}’),
3. EmployeeFullAddress(‘{ZipCode}_{State}, {City}-{Street}’)
4. EmployeeCompanyInfo(‘{CompanyName}({Position})’)

## Task 3:

Create a stored procedure to insert Employee info into DB with the following params:

1. EmployeeName(optional)
2. FirstName(optional)
3. LastName(optional)
4. CompanyName(required)
5. Position(optional)
6. Street(required)
7. City(optional)
8. State(optional)
9. ZipCode(optional)

And the following conditions:

1. At least one field (either EmployeeName  or FirstName or LastName) should be not be:
    - NULL
    - empty string
    - contains only ‘space’ symbols
2. CompanyName should be truncated if length is more than 20 symbols

## Task 4:

Create a trigger for Employee table on insert new Row that will create a new Company with an Address (The address should be copied from the employee’s address).

## NB! Scoreboard:

0-69 - Tasks are implemented with some gaps
70-89 - Good ( Task 1-3 were implemented )
90-100 - Excellent ( Task 4 task was implemented )
