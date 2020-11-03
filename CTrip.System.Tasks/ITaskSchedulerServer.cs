using CTrip.System.Model;
using CTrip.System.Model.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CTrip.System.Tasks
{
	public interface ITaskSchedulerServer
	{
		Task<ApiResult<string>> StartTaskScheduleAsync();

		Task<ApiResult<string>> StopTaskScheduleAsync();

		Task<ApiResult<string>> AddTaskScheduleAsync(Sys_TasksQz tasksQz);

		Task<ApiResult<string>> PauseTaskScheduleAsync(Sys_TasksQz tasksQz);

		Task<ApiResult<string>> ResumeTaskScheduleAsync(Sys_TasksQz tasksQz);

		Task<ApiResult<string>> DeleteTaskScheduleAsync(Sys_TasksQz tasksQz);
	}
}
