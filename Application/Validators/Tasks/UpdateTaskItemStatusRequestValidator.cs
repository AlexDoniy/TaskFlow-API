using Application.Dtos.Tasks;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.Tasks;

public class UpdateTaskItemStatusRequestValidator : AbstractValidator<UpdateTaskItemStatusRequest>
{
    public UpdateTaskItemStatusRequestValidator()
    {
        RuleFor(task => task.Status).IsInEnum();
    }
}