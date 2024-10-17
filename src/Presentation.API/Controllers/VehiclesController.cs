namespace Vehicles.API.Controllers;

using Application.DTO;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("[controller]")]
public class VehiclesController : Controller
{
    private readonly ICreateVehiclesService _createVehiclesService;
    private readonly IUpdateVehiclesService _updateVehiclesService;
    private readonly ISearchVehiclesService _searchVehiclesService;
    private readonly IGetByIdVehiclesService _getByIdVehiclesService;

    public VehiclesController(
        ICreateVehiclesService createVehiclesService,
        IUpdateVehiclesService updateVehiclesService,
        ISearchVehiclesService searchVehiclesService,
        IGetByIdVehiclesService getByIdVehiclesService)
    {
        _createVehiclesService = createVehiclesService;
        _updateVehiclesService = updateVehiclesService;
        _searchVehiclesService = searchVehiclesService;
        _getByIdVehiclesService = getByIdVehiclesService;
    }

    [HttpPost(Name = "Create")]
    public async Task<ActionResult> CreateVehiclesAsync([FromBody] Vehicle vehicle)
    {
        await _createVehiclesService.CreateAsync(vehicle);
        var vehicleId = Guid.NewGuid();
        var resourceLocationUri = this.Request?.GetDisplayUrl() + $"/{vehicleId}";
        return this.Created(resourceLocationUri, null);
    }

    [HttpPut(Name = "Update")]
    public async Task<ActionResult> UpdateVehiclesAsync([FromBody] Vehicle vehicle)
    {
        await _updateVehiclesService.UpdateAsync(vehicle);
        var vehicleId = Guid.NewGuid();
        var resourceLocationUri = this.Request?.GetDisplayUrl() + $"/{vehicleId}";
        return this.Created(resourceLocationUri, null);
    }

    [HttpGet, Route("GetById")]
    public async Task<ActionResult> UpdateVehiclesAsync([Required, FromQuery] Guid id)
    {
        var vehicle = await _getByIdVehiclesService.GetByIdAsync(id);
        return this.Ok( vehicle);
    }

    [HttpPost, Route("Search")]
    public async Task<ActionResult<Page<Vehicle>>> SearechVehiclesAsync(
        [FromBody] SearchContext searchContext,
        [FromQuery] int? page = null,
        [FromQuery] int? pageSize = null
        )
    {
        await _searchVehiclesService.SearchAsync(searchContext);
        var resourceLocationUri = this.Request?.GetDisplayUrl() + $"/{123}";
        return this.Created(resourceLocationUri, null);
    }
}