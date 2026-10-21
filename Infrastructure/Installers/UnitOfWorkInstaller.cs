using Application.Abstractions.Interfaces;
using Infrastructure.Persistence.MsSQL;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Installers;

public static class UnitOfWorkInstaller
{
    public static void InstallIUnitOfWork (this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());
    }    
}
