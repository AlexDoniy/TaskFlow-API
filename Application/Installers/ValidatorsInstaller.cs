using Application.Dtos.Projects;
using Application.Dtos.Tasks;
using Application.Validators.Projects;
using Application.Validators.Tasks;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Installers;

public static class ValidatorsInstaller
{
    public static void InstallValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateProjectRequest>, CreateProjectRequestValidator>();
        services.AddScoped<IValidator<UpdateProjectRequest>, UpdateProjectRequestValidator>();
        
        services.AddScoped<IValidator<CreateTaskItemRequest>, CreateTaskItemRequestValidator>();
        services.AddScoped<IValidator<UpdateTaskItemRequest>, UpdateTaskItemRequestValidator>();
        services.AddScoped<IValidator<UpdateTaskItemStatusRequest>, UpdateTaskItemStatusRequestValidator>();
        services.AddScoped<IValidator<GetTaskItemsRequest>, GetTaskItemsRequestValidator>();
    }
}

