using System;
using System.Collections.Generic;
using Ar.IServices;
using Ar.Repository;

using AR.Model;
using Dapper;

namespace Ar.Services
{
    public class UserTaskService : IUserTaskService
    {
        public UserTask GetUserTaskByCode(string orderCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@orderCode", orderCode, System.Data.DbType.String);
            UserTask task = DapperSqlHelper.FindOne<UserTask>("select * from [dbo].[UserTask] where OrderCode=@orderCode", paras, false);
            return task;
        }

        public IList<UserTask> GetUserTaskList(string userCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@userCode", userCode, System.Data.DbType.String);
            IList<UserTask> list = DapperSqlHelper.FindToList<UserTask>(@"select * from [dbo].[UserTask]  where  userCode=@userCode", paras, false);
            return list;
        }

        public bool UpdateUserTask(string orderCode,int IsComplete)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@OrderCode", orderCode, System.Data.DbType.String);
            paras.Add("@IsComplete", IsComplete, System.Data.DbType.Int32);
            string sql = "update [dbo].[UserTask] set IsComplete=@IsComplete ,TaskEndTime=getDate() where OrderCode=@OrderCode";
            DapperSqlHelper.ExcuteNonQuery<UserTask>(sql, paras, false);
            return true;
        }

        public bool InsertUserTask(UserTask userTask)
        {
            DynamicParameters paras = new DynamicParameters();
            if (string.IsNullOrEmpty(userTask.OrderCode))
            {
                userTask.OrderCode = GetMaxCode();
            }
            paras.Add("@OrderCode", userTask.OrderCode, System.Data.DbType.String);
            paras.Add("@TaskCode", userTask.TaskCode, System.Data.DbType.String);
            paras.Add("@IsComplete", userTask.IsComplete, System.Data.DbType.String);
            paras.Add("@TaskStartTime", userTask.TaskStartTime, System.Data.DbType.String);
            paras.Add("@TaskEndTime", userTask.TaskEndTime, System.Data.DbType.String);
            paras.Add("@UserCode", userTask.UserCode, System.Data.DbType.String);
            User userone = DapperSqlHelper.FindOne<User>(@"INSERT INTO [dbo].[UserTask]
            ([OrderCode],[TaskCode] ,[IsComplete]  ,[TaskStartTime] ,
            [TaskEndTime],[UserCode]) 
            VALUES  ( OrderCode=@OrderCode, 
                      IsComplete=@IsComplete, 
                      TaskStartTime=@TaskStartTime, 
                      TaskEndTime=@TaskEndTime, 
                      UserCode=@UserCode, 
                      )", paras, false);
            return true;
        }
        public string GetMaxCode()
        {
            var task = DapperSqlHelper.FindOne<UserTask>("SELECT MAX(OrderCode) OrderCode FROM [dbo].[UserTask]", null, false);
            var code = task != null ? Convert.ToInt32(task.OrderCode) + 1 : 1;
            return code.ToString();
        }
    }
}
