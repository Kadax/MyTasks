using MyTaskAPI.Model.Tasks;

namespace MyTaskAPI.Services
{

    public interface ITasksService
    {
        Task<List<MyTask>> GetAllTasks();
        Task<MyTask> GetTaskById(int id);

        Task<MyTask> CreateTask(MyTask newTask);

        Task<MyTask> UpdateTask(MyTask task);
        Task DeleteTask(int id);


        Task<List<TaskStatus>> GetTaskStatus();




    }

}
