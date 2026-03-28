using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTaskAPI.Model.Tasks;
using MyTaskAPI.Services;

namespace MyTaskAPI.Controllers.Tasks
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TimeSpentController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public TimeSpentController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpPost]
        public async Task<int> Add(TimeSpentDTO timeSpent)
        {
            var t = await _tasksService.AddTimeSpent(timeSpent.taskId, timeSpent.duration);

            return t;

        }
    }
}
