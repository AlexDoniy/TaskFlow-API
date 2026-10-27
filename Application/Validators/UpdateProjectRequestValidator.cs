using Application.Dtos.Projects;
using FluentValidation;

namespace Application.Validators;

public class UpdateProjectRequestValidator : AbstractValidator<UpdateProjectRequest>
{
    public UpdateProjectRequestValidator()
    {
        RuleFor(project => project.Name).NotEmpty().MaximumLength(200);
        RuleFor(project => project.Description).MaximumLength(2000);
    }
}