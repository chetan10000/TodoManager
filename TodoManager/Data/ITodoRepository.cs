using TodoManager.Models;

namespace TodoManager.Data
{
    public interface ITodoRepository
    {
        public Task<IEnumerable<Todo>> GetAllTodosAsync();
        public Task<Todo?> GetTodoByIdAsync(int id);
        public Task<Dictionary<string, List<Todo>>?> GetIncomingTodosAsync();
        public Task AddTodoAsync(Todo todo);

        public Task<Todo?> SetTodoPercentage(int id , decimal percentage);
        public Task DeleteTodo(Todo task);
        public Task<Todo?> UpdateTodoAsync(int id ,Todo task);
        public Task<Todo?> MarkDoneAsync(int id);

    }
    /*
      . Get All Todo’s
      • Get Specific Todo
      • Get Incoming ToDo (for today/next day/current week)
      • Create Todo
      • Update Todo
      • Set Todo percent complete
      • Delete Todo
      • Mark Todo as Done
     */
}
