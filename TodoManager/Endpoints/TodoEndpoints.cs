using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TodoManager.Data;
using TodoManager.Models;
using TodoManager.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TodoManager.Endpoints
{
    public static class TodoEndpoints
    {
        public static RouteGroupBuilder MapTodoEndpoints(this RouteGroupBuilder group)
        {

            group.MapGet("/todos", GetAllTodos);
            group.MapPost("/todos", AddTodo);
            group.MapGet("/todos/{id:int}", GetTodoById);
            group.MapPut("/todos/{id:int}", UpdateTodo);
            group.MapDelete("/todos/{id:int}", DeleteTodo);
            group.MapPut("/todos/setPercentage/{id:int}", SetTodoPercentage);
            group.MapPut("/todos/mark/{id:int}", MarkDone);
            group.MapPut("/todos/incoming", IncomingTodos);


            return group;

        }

        public static async Task<IResult> GetAllTodos(ITodoRepository todoRepository)
        {
            var todos = await todoRepository.GetAllTodosAsync();
            if(todos == null)
            {
                Results.NoContent();
            }
            return Results.Ok(todos);
        }


       public static async Task<IResult> AddTodo(TodoDTO dto,IValidator<TodoDTO> validator,ITodoRepository todoRepository)
        {
            

            var validationResult = await ValidationHelper.ValidateAsync(dto, validator);
            if(validationResult != null)
            {
                return validationResult;
            }

          

            var todo = new Todo
            {
                Title = dto.Title,
                Description = dto.Description,
                Deadline = dto.Deadline,
                
            };
            await todoRepository.AddTodoAsync(todo);
            return Results.Created($"todos/{todo.Id}", todo);

        }
        public static async Task<IResult> GetTodoById(int id,ITodoRepository todoRepository)
        {
            var todo = await todoRepository.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(todo);
        }

        public static async Task<IResult> DeleteTodo(int id, ITodoRepository todoRepository)
        {
            var todo = await todoRepository.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return Results.NotFound();
            }
            await todoRepository.DeleteTodo(todo);
            return Results.NoContent();
        }
        public static async Task<IResult> SetTodoPercentage(int id,decimal percentage, ITodoRepository todoRepository)
        {
            var todo = await todoRepository.SetTodoPercentage(id,percentage);
            if (todo == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(todo);
        }
        public static async Task<IResult> IncomingTodos(ITodoRepository todoRepository)
        {
            var incomingTodos = await todoRepository.GetIncomingTodosAsync();
            if(incomingTodos == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(incomingTodos);

        }

        public static async Task<IResult> UpdateTodo(int id,Todo updatedTodo,ITodoRepository todoRepository,IValidator<Todo> validator)
        {

            var validationResult = await ValidationHelper.ValidateAsync(updatedTodo, validator);
            if (validationResult != null)
            {
                return validationResult;
            }

            var todo = await todoRepository.UpdateTodoAsync(id,updatedTodo);
            if (todo == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(todo);
        }

        public static async Task<IResult> MarkDone(int id,  ITodoRepository todoRepository)
        {

            var todo = await todoRepository.MarkDoneAsync(id);
            if (todo == null)
            {
                return Results.NotFound();
             }
            return Results.Ok(todo);
        }
    }
}
