using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTaskAPI.Model.Tasks;
using MyTaskAPI.Services;

namespace MyTaskAPI.Controllers.Tasks
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTasksController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public OrderTasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpPost]
        public async Task<IActionResult> OrderTasks(List<MyTaskOrderDTO> tasks)
        {
            await _tasksService.OrderTasks(tasks);

            return NoContent();

        }
    }
}
