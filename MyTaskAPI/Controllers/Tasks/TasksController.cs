using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTaskAPI.Migrations;
using MyTaskAPI.Model;
using MyTaskAPI.Model.Tasks;
using MyTaskAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyTaskAPI.Controllers.Tasks
{                       
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        // GET: api/<TasksController>
        [HttpGet]
        public async Task<List<MyTask>> GetMyTasks()
        {
            return await _tasksService.GetAllTasks();
        }

        // GET api/<Tasks>/5
        [HttpGet("{id}")]
        public async Task<MyTask> GetMyTask(int id)
        {
            var task = await _tasksService.GetTaskById(id);
            if (task == null)
            {
                throw new HttpRequestException(HttpRequestError.InvalidResponse);
            }
            return task;
        }

        [HttpPost]
        public async Task<MyTask> CreateMyTask(MyTask newTask)
        {
            var task = await _tasksService.CreateTask(newTask);
            return task;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMyTask(int id, MyTask updatedTask)
        {
            if (id != updatedTask.id)
            {
                return BadRequest();
            }

            var result = await _tasksService.UpdateTask(updatedTask);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMyTask(int id)
        {
            await _tasksService.DeleteTask(id);
            return NoContent();
        }

    }
}
