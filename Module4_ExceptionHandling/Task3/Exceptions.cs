using System;

namespace Task3;

public class InvalidUserIdException() : Exception("Invalid userId");
public class UserNotFoundException() : Exception("User not found");
public class TaskAlreadyExistsException() : Exception("The task already exists");
