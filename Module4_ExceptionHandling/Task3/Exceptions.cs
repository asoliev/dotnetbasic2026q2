using System;

namespace Task3;

public abstract class UserTaskException(string message) : Exception(message);

public class InvalidUserIdException() : UserTaskException("Invalid userId");
public class UserNotFoundException() : UserTaskException("User not found");
public class TaskAlreadyExistsException() : UserTaskException("The task already exists");
