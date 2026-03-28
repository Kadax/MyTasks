using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTaskAPI.Model.Tasks;
using MyTaskAPI.Services;

namespace MyTaskAPI.Controllers.Tasks.Types
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskTypesController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public TaskTypesController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpGet]
        public async Task<List<TypeTask>> get()
        {
            return await _tasksService.GetTypesTask();
        }

        [HttpPost]
        public async Task<TypeTask> SaveTypeTask(TypeTask type)
        {
            return await _tasksService.SaveTypeTask(type);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeTask(int id)
        {
            await _tasksService.DeleteTypeTask(id);

            return NoContent();
        }

    }
}
