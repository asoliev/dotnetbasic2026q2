using System;
using Task3.DoNotChange;

namespace Task3;

public class UserTaskController(UserTaskService taskService)
{
    public bool AddTaskForUser(int userId, string description, IResponseModel model)
    {
        try
        {
            var task = new UserTask(description);
            taskService.AddTaskForUser(userId, task);
            return true;
        }
        catch (Exception ex) when (ex is InvalidUserIdException or UserNotFoundException or TaskAlreadyExistsException)
        {
            model.AddAttribute("action_result", ex.Message);
            return false;
        }
    }
}
