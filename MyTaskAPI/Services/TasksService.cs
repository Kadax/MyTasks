using Microsoft.EntityFrameworkCore;
using MyTaskAPI.Model;
using MyTaskAPI.Model.Tasks;

namespace MyTaskAPI.Services
{

    public class TasksService : ITasksService
    {
        private readonly ApplicationDbContext _context;

        public TasksService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MyTask>> GetAllTasks(bool all= false)
        {
            if (all)
                return await _context.Tasks.ToListAsync();
            else
                return await _context.Tasks.Where(i => i.isArchive != true).ToListAsync();

        }

        public async Task<MyTask> GetTaskById(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<MyTask> CreateTask(MyTask newTask)
        {
            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();
            return newTask;
        }

        public async Task ChangeStatusTask(int TaskId, int StateId)
        {
            var task = await _context.Tasks.FindAsync(TaskId);
            if (task == null)
            {
                throw new HttpRequestException("Task not found");
            }

            task.statusId = StateId;

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
            var task = await _context.Tasks.FindAsync(mytask.id);
            if (task == null)
            {
                throw new HttpRequestException(HttpRequestError.InvalidResponse);
            }

            task.updateAt = DateTime.Now;

            task.description = mytask.description;
            task.title = mytask.title;
            task.deadline = mytask.deadline;
            task.plannedTime = mytask.plannedTime;

            task.executorId = mytask.executorId;
            task.statusId = mytask.statusId;

            await _context.SaveChangesAsync();

            return task;
        }

        public async Task ArchiveTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                throw new HttpRequestException("Task not found");
            }

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

            return statuses;

        }

        public async Task<int> AddTimeSpent(int taskId, int duration)
        {
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
            
            await _context.TimeSpents.AddAsync(v);
            await _context.SaveChangesAsync();

            var times = await _context.TimeSpents.Where(i => i.taskId == taskId).ToListAsync();

            task.totalTime = times.Sum(i => i.time);

            await _context.SaveChangesAsync();

            return task.totalTime;

        }

    }

}
