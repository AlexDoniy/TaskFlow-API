using Infrasructure.Persistence.MsSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Installers;

public static class EntiyFrameworkInstaller
{

    public static void InstallEntityFramework(this IServiceCollection services, IConfiguration configuration)
    {
        var cs = configuration.GetConnectionString("Default")
                 ?? throw new InvalidOperationException("Connection string 'Default' is not configured.");
        
        services.AddDbContext<AppDbContext>(options => options
            .UseSqlServer(cs));
    }    
}
