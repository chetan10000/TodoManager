using FluentValidation;
using System.Globalization;

namespace TodoManager.Models
{
    public class TodoValidator:AbstractValidator<Todo>
    {
        public TodoValidator() 
        {
            RuleFor(Todo => Todo.Title).NotNull().MinimumLength(5);
            RuleFor(Todo => Todo.Description).NotNull().MinimumLength(10).WithMessage("Description should atleast 10 char long");
            RuleFor(Todo => Todo.Deadline).Must(date => date > DateTime.Now).WithMessage("Deadline must be in ISO format (yyyy-MM-dd)");
            RuleFor(Todo => Todo.Percentage).InclusiveBetween(0,100).WithMessage("Percentage should be between 0 to 100 ");
        }

      
    }

    public class TodoDTOValidator : AbstractValidator<TodoDTO>
    {
        public TodoDTOValidator()
        {
            RuleFor(Todo => Todo.Title).NotNull().MinimumLength(5);
            RuleFor(Todo => Todo.Description).NotNull().MinimumLength(10).WithMessage("Description should atleast 10 char long");
            RuleFor(Todo => Todo.Deadline).Must(date => date > DateTime.Now).WithMessage("Deadline should be of future time , Deadline must be in ISO format (yyyy-MM-dd)");
            
        }
        
    }


}
