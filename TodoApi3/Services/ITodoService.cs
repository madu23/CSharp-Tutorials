using TodoApi3.Models;

namespace TodoApi3.Services
{
    public interface ITodoService
    {
        Task<TodoItem> GetTodoItemAsync(long id);
        Task<List<TodoItem>> GetTodoItemsAsync();
    }
}