using Application.Dtos.Tasks;
using FluentValidation;

namespace Application.Validators.Tasks;

public class GetTaskItemsRequestValidator : AbstractValidator<GetTaskItemsRequest>
{
    public GetTaskItemsRequestValidator()
    {
        RuleFor(request => request.Page).InclusiveBetween(1, 1_000_000);
        RuleFor(request => request.PageSize).InclusiveBetween(1, 100);
    }
}

