using System;
using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;

using AR.Model;
using Dapper;

namespace Ar.Services
{
    public class TaskService : ITaskService
    {
        public Task GetTaskByCode(string code)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@taskCode", code, System.Data.DbType.String);
            Task task = DapperSqlHelper.FindOne<Task>("select * from [dbo].[Task] where TaskCode=@taskCode and VersionEndTime is null", paras, false);
            return task;
        }

        public IList<Task> GetTaskList()
        {
            DynamicParameters paras = new DynamicParameters();
            IList<Task> list = DapperSqlHelper.FindToList<Task>(@"select * from [dbo].[Task]  where  VersionEndTime is null ", null, false);
            return list;
        }
    }
}
