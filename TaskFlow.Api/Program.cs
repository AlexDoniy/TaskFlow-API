using Application.Installers;
using Infrastructure.Installers;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
services.AddOpenApi();

services.InstallEntityFramework(configuration);
services.InstallIUnitOfWork();
services.InstallRepositories();

services.InstallServices();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();