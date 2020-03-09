using AR.Model;
using System.Collections.Generic;

namespace Ar.IServices
{
    public interface IUserTaskService
    {
        UserTaskshow GetUserTaskByCode(string UserTaskCode);
        IList<UserTaskshow> GetUserTaskList(string userCode);
        bool UpdateUserTask(string Code, int IsComplete);
        bool InsertUserTask(string userTask,string type);
    }
}
