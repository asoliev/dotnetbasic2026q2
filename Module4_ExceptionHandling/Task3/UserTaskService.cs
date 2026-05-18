using System;
using System.Collections.Generic;
using System.Linq;
using Task3.DoNotChange;

namespace Task3;

public class UserTaskService(IUserDao userDao)
{
    public void AddTaskForUser(int userId, UserTask task)
    {
        if (userId < 0)
            throw new InvalidUserIdException();

        IUser user = userDao.GetUser(userId);
        if (user == null)
            throw new UserNotFoundException();

        IList<UserTask> tasks = user.Tasks;
        if (tasks.Any(t => string.Equals(task.Description, t.Description, StringComparison.OrdinalIgnoreCase)))
            throw new TaskAlreadyExistsException();

        tasks.Add(task);
    }
}
