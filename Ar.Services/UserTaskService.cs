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
        public UserTaskshow GetUserTaskByCode(string UserTaskCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@UserTaskCode", UserTaskCode, System.Data.DbType.String);
            UserTaskshow task = DapperSqlHelper.FindOne<UserTaskshow>(@"select u.* ,t.TasKName,t.TaskTarget,t.Reward,t.Integral from [dbo].[UserTask] u join [dbo].[Task] t on t.TaskCode=u.TaskCode
where u.UserTaskCode=@UserTaskCode
", paras, false);
            return task;
        }

        public IList<UserTaskshow> GetUserTaskList(string userCode)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@userCode", userCode, System.Data.DbType.String);
            IList<UserTaskshow> list = DapperSqlHelper.FindToList<UserTaskshow>(@"select u.* ,t.TasKName,t.TaskTarget,t.Reward,t.Integral from [dbo].[UserTask] u join [dbo].[Task] t on t.TaskCode=u.TaskCode where  u.userCode=@userCode", paras, false);
            return list;
        }

        public bool UpdateUserTask(string UserTaskCode, int IsComplete)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@UserTaskCode", UserTaskCode, System.Data.DbType.String);
            paras.Add("@IsComplete", IsComplete, System.Data.DbType.Int32);
            string sql = "update [dbo].[UserTask] set IsComplete=@IsComplete ,TaskEndTime=getDate() where UserTaskCode=@UserTaskCode";
            DapperSqlHelper.ExcuteNonQuery<UserTask>(sql, paras, false);
            return true;
        }

        public bool InsertUserTask(string userCode,string type)
        {
            DynamicParameters paras = new DynamicParameters();
            UserTask userTask = new UserTask();
            if (string.IsNullOrEmpty(userTask.UserTaskCode))
            {
                userTask.UserTaskCode = GetMaxCode();
                userTask.UserCode = userCode;
                userTask.TaskCode = type;
                userTask.TaskStartTime = DateTime.Now;
                userTask.TaskEndTime = DateTime.MaxValue;
                userTask.IsComplete = false;
            }
            paras.Add("@UserTaskCode", userTask.UserTaskCode, System.Data.DbType.String);
            paras.Add("@TaskCode", userTask.TaskCode, System.Data.DbType.String);
            paras.Add("@IsComplete", userTask.IsComplete, System.Data.DbType.Boolean);
            paras.Add("@TaskStartTime", userTask.TaskStartTime, System.Data.DbType.String);
            paras.Add("@TaskEndTime", userTask.TaskEndTime, System.Data.DbType.String);
            paras.Add("@UserCode", userTask.UserCode, System.Data.DbType.String);
            int count = DapperSqlHelper.ExcuteNonQuery<UserTask>(@"INSERT INTO [dbo].[UserTask]
            ([UserTaskCode],[TaskCode] ,[IsComplete]  ,[TaskStartTime] ,
            [TaskEndTime],[UserCode]) 
            VALUES  ( @UserTaskCode, 
                      @TaskCode, 
                      @IsComplete, 
                      @TaskStartTime, 
                      @TaskEndTime, 
                      @UserCode
                      )", paras, false);
            if (count > 0)
            {
                return true;
            } else
            {
                return false;
            }
        }
        public string GetMaxCode()
        {
            var task = DapperSqlHelper.FindOne<UserTask>("SELECT MAX(UserTaskCode) UserTaskCode FROM [dbo].[UserTask]", null, false);
            var code = task != null ? Convert.ToInt32(task.UserTaskCode) + 1 : 1;
            return code.ToString();
        }
    }
}
