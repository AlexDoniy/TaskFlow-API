using Application.Dtos.Tasks;
using FluentValidation;

namespace Application.Validators.Projects;

public class GetProjectsRequestValidator : AbstractValidator<GetProjectsRequest>
{
    public GetProjectsRequestValidator()
    {
        RuleFor(request => request.Page).InclusiveBetween(1, 1_000_000);
        RuleFor(request => request.PageSize).InclusiveBetween(1, 100);
    }
}