using Application.Dtos.Tasks;
using FluentValidation;

namespace Application.Validators.Tasks;

public class CreateTaskItemRequestValidator : AbstractValidator<CreateTaskItemRequest>
{
    public CreateTaskItemRequestValidator()
    {
        RuleFor(task => task.Title).NotEmpty().MaximumLength(200);
        RuleFor(task => task.Description).MaximumLength(2000);
    }
}