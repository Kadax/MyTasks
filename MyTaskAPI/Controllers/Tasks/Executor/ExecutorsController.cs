using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTaskAPI.Model.Tasks;
using MyTaskAPI.Services;

namespace MyTaskAPI.Controllers.Tasks.Executor
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecutorsController : ControllerBase
    {

        private readonly ITasksService _tasksService;

        public ExecutorsController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }


        [HttpGet]
        public async Task<List<ExecutorTask>> get()
        {
            return await _tasksService.GetExecutor();
        }

        [HttpPost]
        public async Task<ExecutorTask> SaveExecutor(ExecutorTask ex)
        {
            return await _tasksService.SaveExecutor(ex);
        }






    }
}
