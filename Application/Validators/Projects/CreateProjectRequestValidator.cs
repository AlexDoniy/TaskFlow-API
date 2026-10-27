using Application.Dtos.Projects;
using FluentValidation;

namespace Application.Validators.Projects;

public class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectRequestValidator()
    {
        RuleFor(project => project.Name).NotEmpty().MaximumLength(200);
        RuleFor(project => project.Description).MaximumLength(2000);
    }
}