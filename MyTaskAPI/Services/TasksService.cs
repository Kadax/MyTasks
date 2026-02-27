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

        public async Task<List<MyTask>> GetAllTasks()
        {
            return await _context.Tasks.ToListAsync();
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

        public async Task<MyTask> UpdateTask(MyTask mytask)
        {
            var task = await _context.Tasks.FindAsync(mytask.id);
            if (task == null)
            {
                throw new HttpRequestException(HttpRequestError.InvalidResponse);
            }

            task.updateAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return task;
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

        public async Task<List<TaskStatus>> GetTaskStatus()
        {
            List<TaskStatus> statuses = new List<TaskStatus>();


            return statuses;

        }


    }

}
