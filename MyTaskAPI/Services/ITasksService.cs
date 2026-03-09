using MyTaskAPI.Model.Tasks;

namespace MyTaskAPI.Services
{

    public interface ITasksService
    {
        Task<List<MyTask>> GetAllTasks(bool all = false);
        Task<MyTask> GetTaskById(int id);

        Task<MyTask> CreateTask(MyTask newTask);

        Task<MyTask> UpdateTask(MyTask task);
        Task DeleteTask(int id);
        Task ArchiveTask(int id);

        Task<List<Status>> GetTaskStatus();

        Task ChangeStatusTask(int TaskId, int StateId);
        Task OrderTasks(List<MyTaskOrderDTO> tasks);

        Task<int> AddTimeSpent(int taskId, int duration);




    }

}
