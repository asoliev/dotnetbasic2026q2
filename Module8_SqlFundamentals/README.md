# Module 8: SQL Fundamentals

This folder contains the SQL database project for Module 8.

## Contents

- `src/` - the main SQL database project with tables, view, stored procedure, trigger, and post-deployment seed data.
- `Module8_SqlFundamentals.md` - the original assignment statement.

## Project Structure

```text
src/
├── Module8_SqlFundamentals.sqlproj
├── Scripts/
│   └── Script.PostDeployment.sql
├── StoredProcedures/
│   └── InsertEmployeeInfo.sql
├── Tables/
│   ├── Address.sql
│   ├── Company.sql
│   ├── Employee.sql
│   └── Person.sql
├── Triggers/
│   └── EmployeeInsertCreateCompany.sql
└── Views/
	└── EmployeeInfo.sql
```

## Implemented Tasks

1. Tables: `Person`, `Address`, `Employee`, and `Company`
2. View: `EmployeeInfo`
3. Stored procedure: `InsertEmployeeInfo`
4. Trigger: `TR_Employee_Insert_CreateCompany`

## Object Summary

- `Person` stores first and last names for each person.
- `Address` stores address details used by employees and companies.
- `Employee` links a person and an address, plus company and position data.
- `Company` stores company name and its address.
- `EmployeeInfo` combines employee, person, and address data for reporting.
- `InsertEmployeeInfo` inserts the related person, address, and employee rows.
- `EmployeeInsertCreateCompany` creates a company row after an employee insert.
- `Script.PostDeployment.sql` seeds the database with starter data.

## Build

Build the SQL project with:

```bash
dotnet build src/Module8_SqlFundamentals.sqlproj
```

This creates a `.dacpac` in `src/bin/Debug/`.

## Publish

You can publish the generated dacpac with SqlPackage:

```bash
sqlpackage /Action:Publish /SourceFile:src/bin/Debug/Module8_SqlFundamentals.dacpac /TargetServerName:localhost /TargetDatabaseName:Module8_SqlFundamentals
```

If you want to work with the generated SDK-style sample project instead, build it from its own folder:

```bash
dotnet build MyDatabaseProject/MyDatabaseProject.sqlproj
```

## Notes

- The SQL project uses the Microsoft.Build.Sql SDK.
- Seed data is added through the post-deployment script.

### Install SqlPackage CLI

If you would like to use the command-line utility SqlPackage.exe for deploying the `dacpac`, you can obtain it as a dotnet tool.  The tool is available for Windows, macOS, and Linux.

```bash
dotnet tool install -g microsoft.sqlpackage
```
