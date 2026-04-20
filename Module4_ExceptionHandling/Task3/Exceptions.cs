using System;

namespace Task3
{
    public class InvalidUserIdException : Exception
    {
        public InvalidUserIdException() : base("Invalid userId") { }
    }

    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User not found") { }
    }

    public class TaskAlreadyExistsException : Exception
    {
        public TaskAlreadyExistsException() : base("The task already exists") { }
    }
}
