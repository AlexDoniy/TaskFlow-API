using System.Text.Json.Serialization;
using Application.Installers;
using Infrastructure.Installers;
using TaskFlow.Api.ExceptionsHandlers;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
services.AddOpenApi();

services.InstallEntityFramework(configuration);
services.InstallIUnitOfWork();
services.InstallRepositories();

services.InstallValidators();
services.InstallServices();

services.AddExceptionHandler<ValidationExceptionHandler>();
services.AddExceptionHandler<AppExceptionHandler>();
services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();