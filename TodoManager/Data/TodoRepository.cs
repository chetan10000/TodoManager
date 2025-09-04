using Microsoft.EntityFrameworkCore;
using TodoManager.Models;

namespace TodoManager.Data
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _context;

        public TodoRepository(ApplicationDbContext context)
        {
            _context =  context ?? throw new ArgumentNullException(nameof(context)); ;
        }
        public async Task AddTodoAsync(Todo todo)
        {
            await _context.AddAsync(todo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTodo(Todo todo)
        {

                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
            
            
        }

        public async Task<IEnumerable<Todo>> GetAllTodosAsync()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<Dictionary<string,List<Todo>>?> GetIncomingTodosAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            var endOfWeek = today.AddDays(7 - (int)today.DayOfWeek);

            var incomingTodos = await  _context.Todos.AsNoTracking().Where(todo => !todo.IsDone && todo.Deadline.Date >= today && todo.Deadline.Date <= endOfWeek) .OrderBy(todo=>todo.Deadline).ToListAsync();
            return new Dictionary<string, List<Todo>>
            {
                ["today"] = incomingTodos.Where(todo => todo.Deadline.Date == today).ToList(),
                ["tomorrow"] = incomingTodos.Where(todo => todo.Deadline.Date == tomorrow).ToList(),
                ["currentWeek"] = incomingTodos.Where(todo=>todo.Deadline.Date> tomorrow).ToList()
            };

        }

        public async Task<Todo?> GetTodoByIdAsync(int id)
        {
            return await _context.Todos.FindAsync(id);
        }

        public async Task<Todo?> MarkDoneAsync(int id)
        {
            var todotask = await _context.Todos.FindAsync(id);
            if(todotask!=null)
            {
                todotask.Percentage = 100;
                todotask.IsDone = true;
                _context.Todos.Update(todotask);
                await _context.SaveChangesAsync();
                return todotask;
            }
            return null;
        }

        public async Task<Todo?> SetTodoPercentage(int id ,decimal percentage)
        {
            var todotask = await _context.Todos.FindAsync(id);
            if(todotask != null)
            {
                todotask.Percentage = percentage;
                await _context.SaveChangesAsync();
                return todotask;

            }
            return null;
        }

        public async Task<Todo?> UpdateTodoAsync(int id, Todo updatedTodo)
        {
            var todotask = await _context.Todos.FindAsync(id);
            if (todotask != null)
            {
                todotask.Title = updatedTodo.Title;
                todotask.Description = updatedTodo.Description;
                todotask.Percentage = updatedTodo.Percentage;
                todotask.IsDone = updatedTodo.IsDone;
                todotask.Deadline = updatedTodo.Deadline;
                await _context.SaveChangesAsync();
                return todotask;

            }
            return null;
           
        }
    }
}
