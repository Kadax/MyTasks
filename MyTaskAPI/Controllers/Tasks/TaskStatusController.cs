using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTaskAPI.Model.Tasks;
using MyTaskAPI.Services;

namespace MyTaskAPI.Controllers.Tasks
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskStatusController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public TaskStatusController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpGet]
        public async Task<List<Status>> GetStatuses()
        {
            return await _tasksService.GetTaskStatus();
        }

        [HttpPut]
        public async Task<Status> SaveStatus(Status status)
        {
            return await _tasksService.SaveStatus(status);
        }

    }
}
