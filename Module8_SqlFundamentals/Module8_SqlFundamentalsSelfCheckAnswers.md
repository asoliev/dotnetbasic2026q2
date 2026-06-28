# Module 8: SQL Fundamentals Self-Check Answers

## 1. What is the difference between primary and secondary keys? How many primary keys are available for the table? How many secondary keys are available for the table?

A primary key uniquely identifies each row in a table. It cannot contain `NULL` values, and each table can have only one primary key constraint. A primary key may be made of one column or multiple columns.

A secondary key is any additional key or indexed column used to search, sort, or relate data. Unlike a primary key, a table can have many secondary keys.

In this module, each table has 1 primary key. Secondary keys depend on the table design. For example, `Employee` has 2 secondary indexes on `AddressId` and `PersonId`.

## 2. What is the difference between clustered and non-clustered indexes?

A clustered index defines the physical order of rows in the table. Because of that, a table can have only one clustered index.

A non-clustered index is a separate structure that stores indexed column values and row references. It does not change the physical order of the table, and a table can have many non-clustered indexes.

In this project, the `Employee` table has a clustered primary key on `Id` and non-clustered indexes on `AddressId` and `PersonId`.

## 3. What is a transaction?

A transaction is a group of database operations that are treated as one unit of work. Either all changes are committed, or all changes are rolled back.

Transactions help keep data consistent and reliable. They follow the ACID principles:

- Atomicity: all operations succeed or none of them do.
- Consistency: the database remains valid.
- Isolation: concurrent transactions do not interfere with each other.
- Durability: committed changes are saved permanently.

Example:

```sql
BEGIN TRANSACTION;

INSERT INTO dbo.Person (FirstName, LastName)
VALUES (N'John', N'Doe');

COMMIT TRANSACTION;
```

## 4. What is a trigger? How to create a trigger?

A trigger is a special database object that runs automatically when an event happens on a table or view, such as `INSERT`, `UPDATE`, or `DELETE`.

Triggers are commonly used for auditing, enforcing rules, or automatically creating related data.

Basic syntax:

```sql
CREATE TRIGGER dbo.TR_TableName_Insert
ON dbo.TableName
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- trigger logic here
END;
```

In this module, the trigger on `Employee` creates a related `Company` row after a new employee is inserted.

## 5. What is the difference between INNER JOIN, RIGHT JOIN and LEFT JOIN?

`INNER JOIN` returns only the rows that match in both tables.

`LEFT JOIN` returns all rows from the left table and matching rows from the right table. If there is no match, the right-side columns are `NULL`.

`RIGHT JOIN` returns all rows from the right table and matching rows from the left table. If there is no match, the left-side columns are `NULL`.

Example:

```sql
SELECT *
FROM dbo.Employee e
INNER JOIN dbo.Person p ON e.PersonId = p.Id;
```

## 6. What is a stored procedure? How to create a SP?

A stored procedure is a saved SQL program that can accept parameters and execute one or more SQL statements.

Stored procedures are useful for reusing logic, reducing duplicated SQL, and controlling access to data.

Basic syntax:

```sql
CREATE PROCEDURE dbo.InsertEmployee
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Person (FirstName, LastName)
    VALUES (@FirstName, @LastName);
END;
```

In this module, the stored procedure is used to insert employee-related data into multiple tables.

## 7. What is a view? How to create a view? What is the difference between view and table?

A view is a saved `SELECT` query that behaves like a virtual table. It usually does not store data itself; it shows data from one or more tables.

Basic syntax:

```sql
CREATE VIEW dbo.EmployeeInfo AS
SELECT e.Id, e.EmployeeName
FROM dbo.Employee e;
```

Difference between a view and a table:

- A table stores actual data.
- A view stores a query definition, not usually the data itself.
- A table is used for persistent storage.
- A view is used for simplified reading, reporting, and data abstraction.

In this module, the `EmployeeInfo` view combines employee, person, address, and company information for easier reading.
