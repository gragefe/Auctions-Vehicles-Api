using Application.Services;
using Application.Services.Mappers;
using Data.SqlServer;
using Infrastructure.Crosscutting.Validations;
using Presentation.API.DependencyInjection;
using Vehicles.API.DependencyInjection;
using Vehicles.API.Validations;
using SqlRepository = Data.SqlServer.Mappers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Repositories.AddRepositories(builder);
VehicleServices.AddServices(builder);
Gateways.AddGateways(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();