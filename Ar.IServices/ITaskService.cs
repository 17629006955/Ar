using AR.Model;
using System.Collections.Generic;

namespace Ar.IServices
{
    /// <summary>
    /// 任务列表
    /// </summary>
    public interface ITaskService
    {
       Task GetTaskByCode(string code);
        IList<Task> GetTaskList();

    }
}
