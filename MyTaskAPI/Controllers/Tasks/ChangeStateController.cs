using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTaskAPI.Services;

namespace MyTaskAPI.Controllers.Tasks
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangeStateController : ControllerBase
    {

        private readonly ITasksService _tasksService;

        public ChangeStateController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStateTask(int TaskId, int StateId)
        {

            await _tasksService.ChangeStatusTask(TaskId, StateId);
            return NoContent();

        }




    }
}
