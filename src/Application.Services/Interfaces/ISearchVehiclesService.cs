namespace Application.Services.Interfaces;

using Application.DTO;
using System.Collections.Generic;
using DomainAbstract = Domain.Model.Abstract;

public interface ISearchVehiclesService
{
    Task<List<DomainAbstract.Vehicle>> SearchAsync(SearchContext searchContextDto);
}