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
    public async Task<ActionResult> CreateAsync([FromBody] Vehicle vehicle)
    {
        var vehicleId = await _createVehiclesService.CreateAsync(vehicle);
        var resourceLocationUri = this.Request?.GetDisplayUrl() + $"/{vehicleId}";
        return this.Created(resourceLocationUri, vehicleId);
    }

    [HttpPut(Name = "Update")]
    public async Task<ActionResult> UpdateAsync([FromBody] Vehicle vehicle)
    {
        var vehicleId = await _updateVehiclesService.UpdateAsync(vehicle);
        var resourceLocationUri = this.Request?.GetDisplayUrl() + $"/{vehicleId}";
        return this.Created(resourceLocationUri, vehicleId);
    }

    [HttpGet, Route("GetById")]
    public async Task<ActionResult> GetByIdAsync([Required, FromQuery] Guid id)
    {
        return this.Ok(await _getByIdVehiclesService.GetByIdAsync(id));
    }

    [HttpPost, Route("Search")]
    public async Task<ActionResult<Page<Vehicle>>> SearechAsync(
        [FromBody] SearchContext searchContext,
        [FromQuery] int? page = null,
        [FromQuery] int? pageSize = null
        )
    {
        return this.Ok(await _searchVehiclesService.SearchAsync(searchContext));
    }
}