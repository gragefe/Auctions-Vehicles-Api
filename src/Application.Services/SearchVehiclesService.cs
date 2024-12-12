namespace Application.Services;

using Application.DTO;
using Application.Services.Interfaces;
using Application.Services.Mappers;
using AutoMapper;
using Domain.Model.Interfaces;
using DomainAbstract = Domain.Model.Abstract;


public class SearchVehiclesService(IRepository repository) : ISearchVehiclesService
{
    public async Task<List<DomainAbstract.Vehicle>> SearchAsync(SearchContext searchContextDto)
    {
       return await repository.SearchAsync(searchContextDto.ToDomain());
    }
}