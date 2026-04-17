using Microsoft.EntityFrameworkCore;
using MyTaskAPI.Model;
using MyTaskAPI.Model.Tasks;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace MyTaskAPI.Services
{
    public class TasksService : ITasksService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TasksService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<MyTask>> GetAllTasks(bool all= false)
        {
            if (all)
                return await _context.Tasks.ToListAsync();
            else
            {
                var t = await _context.Tasks.Where(i => i.isArchive != true).ToListAsync();
                return t;
            }
        }

        public async Task<MyTask> GetTaskById(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<MyTask> CreateTask(MyTask newTask)
        {
            ClaimsPrincipal userPrincipal = _httpContextAccessor.HttpContext?.User;
            var userid = userPrincipal.Claims.FirstOrDefault(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            newTask.autorId = userid;
            newTask.modifiedId = userid;

            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();
            return newTask;
        }

        public async Task ChangeStatusTask(int TaskId, int StateId)
        {
            ClaimsPrincipal userPrincipal = _httpContextAccessor.HttpContext?.User;
            var userid = userPrincipal.Claims.FirstOrDefault(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;


            var task = await _context.Tasks.FindAsync(TaskId);
            if (task == null)
            {
                throw new HttpRequestException("Task not found");
            }

            task.statusId = StateId;
            task.modifiedId = userid;

            task.updateAt = DateTime.Now;

            await _context.SaveChangesAsync();

        }

        public async Task OrderTasks(List<MyTaskOrderDTO> tasks)
        {
            foreach (var task in tasks)
            {
                var t = await _context.Tasks.FindAsync(task.taskId);

                if (t != null)
                {
                    t.orderNumber = task.order;
                }
            }

            await _context.SaveChangesAsync();

        }

        public async Task<MyTask> UpdateTask(MyTask mytask)
        {

            ClaimsPrincipal userPrincipal = _httpContextAccessor.HttpContext?.User;
            var userid = userPrincipal.Claims.FirstOrDefault(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            var task = await _context.Tasks.FindAsync(mytask.id);
            if (task == null)
            {
                throw new HttpRequestException(HttpRequestError.InvalidResponse);
            }

            task.updateAt = DateTime.Now;
            task.modifiedId = userid;

            task.description = mytask.description;
            task.title = mytask.title;
            task.deadline = mytask.deadline;
            task.plannedTime = mytask.plannedTime;

            task.executorId = mytask.executorId;
            task.statusId = mytask.statusId;

            task.isFixed = mytask.isFixed;
            task.taskTypesId = mytask.taskTypesId;

            await _context.SaveChangesAsync();

            return task;
        }

        public async Task ArchiveTask(int id)
        {
            ClaimsPrincipal userPrincipal = _httpContextAccessor.HttpContext?.User;
            var userid = userPrincipal.Claims.FirstOrDefault(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;


            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                throw new HttpRequestException("Task not found");
            }

            task.modifiedId = userid;

            task.isArchive = true;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                throw new HttpRequestException("Task not found");
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Status>> GetTaskStatus()
        {
            List<Status> statuses = new List<Status>();

            statuses = await _context.TaskStatuses.ToListAsync();

            statuses = statuses.OrderBy(i => i.orderNumber).ToList();

            return statuses;
        }

        public async Task<Status> SaveStatus(Status status)
        {
            var s = await _context.TaskStatuses.FindAsync(status.id);
            if (s == null)
            {
                throw new HttpRequestException(HttpRequestError.InvalidResponse);
            }

            s.name = status.name;
            s.orderNumber = status.orderNumber;
            s.isHidden = status.isHidden;

            await _context.SaveChangesAsync();

            return s;
        }

        public async Task<TypeTask> SaveTypeTask(TypeTask type)
        {
            var s = await _context.TypeTasks.FindAsync(type.id);
            if (s == null)
            {
                if(type.id != 0)
                    throw new HttpRequestException(HttpRequestError.InvalidResponse);
                else
                {
                    s = new TypeTask()
                    {
                       name = type.name
                    };
                    await _context.TypeTasks.AddAsync(s);
                }
            }

            s.name = type.name;
            s.color = type.color;

            await _context.SaveChangesAsync();

            return s;
        }

        public async Task DeleteTypeTask(int id)
        {
            var t = await _context.TypeTasks.FindAsync(id);
            if (t == null)
            {
                throw new HttpRequestException("Type not found");
            }

            _context.TypeTasks.Remove(t);
            await _context.SaveChangesAsync();
        }

        public async Task<int> AddTimeSpent(int taskId, int duration)
        {
            ClaimsPrincipal userPrincipal = _httpContextAccessor.HttpContext?.User;
            var userid = userPrincipal.Claims.FirstOrDefault(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null)
            {
                throw new HttpRequestException(HttpRequestError.InvalidResponse);
            }

            var v = new TimeSpent()
            {
               taskId = taskId,
               time = duration
            };


            task.modifiedId = userid;


            await _context.TimeSpents.AddAsync(v);
            await _context.SaveChangesAsync();

            var times = await _context.TimeSpents.Where(i => i.taskId == taskId).ToListAsync();

            task.totalTime = times.Sum(i => i.time);

            await _context.SaveChangesAsync();

            return task.totalTime;

        }

        public async Task<List<TypeTask>> GetTypesTask()
        {
            var types = await _context.TypeTasks.ToListAsync();

            return types;
        }

    }
}

