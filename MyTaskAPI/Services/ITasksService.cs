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
        Task<List<TypeTask>> GetTypesTask();
        Task<Status> SaveStatus(Status status);
        Task<TypeTask> SaveTypeTask(TypeTask type);
        Task DeleteTypeTask(int id);

        Task<List<ExecutorTask>> GetExecutor();
        Task<ExecutorTask> SaveExecutor(ExecutorTask ex);

    }

}
