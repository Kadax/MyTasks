using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTaskAPI.Services;

namespace MyTaskAPI.Controllers.Reports
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public ReportsController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }



    }
}
