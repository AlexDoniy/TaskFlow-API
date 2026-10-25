using Application.Abstractions.Interfaces.Repositories;
using Application.Abstractions.Interfaces.Services;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Installers;

public static class ServicesInstaller
{
    public static void InstallServices(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ITaskItemService, TaskItemService>();

    }    
}
