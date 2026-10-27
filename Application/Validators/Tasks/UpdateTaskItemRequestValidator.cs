using Application.Dtos.Tasks;
using FluentValidation;

namespace Application.Validators.Tasks;

public class UpdateTaskItemRequestValidator : AbstractValidator<UpdateTaskItemRequest>
{
    public UpdateTaskItemRequestValidator()
    {
        RuleFor(task => task.Title).NotEmpty().MaximumLength(200);
        RuleFor(task => task.Description).MaximumLength(2000);
    }
}