namespace Vehicles.API.DependencyInjection;

using Application.Services.Interfaces;
using VehiclesServices = Application.Services;

public static class VehicleServices
{
    public static void AddServices(WebApplicationBuilder builder)
    {
        builder.Services
              .AddScoped<ICreateVehiclesService, VehiclesServices.CreateVehiclesService>();
        builder.Services
              .AddScoped<IUpdateVehiclesService, VehiclesServices.UpdateVehiclesService>();
        builder.Services
              .AddScoped<IGetByIdVehiclesService, VehiclesServices.GetByIdVehiclesService>();
        builder.Services
              .AddScoped<ISearchVehiclesService, VehiclesServices.SearchVehiclesService>();
    }
}