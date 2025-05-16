using Microsoft.EntityFrameworkCore;
using TodoApi3.Models;

namespace TodoApi3.Services
{
    public class TodoService(TodoContext context) : ITodoService1
    {
        private readonly TodoContext _context = context;

        public async Task<List<TodoItem>> GetTodoItemsAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }


        public async Task<TodoItem> GetTodoItemAsync(long id)
        {
            return await _context.TodoItems.FindAsync(id);
        }
    }
}
