using AR.Model;
using System.Collections.Generic;

namespace Ar.IServices
{
    public interface IUserTaskService
    {
        UserTask GetUserTaskByCode(string orderCode);
        IList<UserTask> GetUserTaskList(string userCode);
        bool UpdateUserTask(string orderCode, int IsComplete);
        bool InsertUserTask(UserTask userTask);
    }
}
