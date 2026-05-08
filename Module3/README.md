# Module 3 - File System Visitor

This module contains a file system visitor library, a small CLI demo, and unit tests.

## Projects

- `Module3.FSVisitorLib` - `FileSystemVisitor` implementation with events, filtering, exclude, and abort behavior.
- `Module3.FSVisitorCLI` - simple console demo for running the visitor.
- `Module3.FSVisitorLib.Tests` - xUnit tests for different visitor modes.

## Run tests

```bash
dotnet test Module3.slnx
```

## Why tests do not use mocks

For these tests, using a real temporary directory is preferable to mocks because:

- `FileSystemVisitor` behavior depends on real `Directory.GetDirectories` / `Directory.GetFiles` traversal.
- Event flow (`DirectoryFound`, `FileFound`, filtered events, `Abort`, `Exclude`) is validated end-to-end.
- Temporary folders keep tests isolated while staying close to production behavior.

The helper `TemporaryFileSystem` in `Module3.FSVisitorLib.Tests/FileSystemVisitorTests.cs` creates and cleans test folders automatically.

## When mocks may be useful

Mocks can still be considered if tests need to simulate I/O failures, permission errors, or very large trees that are slow to construct on disk.
