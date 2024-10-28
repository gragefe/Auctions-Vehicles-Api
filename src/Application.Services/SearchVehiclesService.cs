namespace Application.Services;

using Application.DTO;
using Application.Services.Interfaces;
using Application.Services.Mappers;
using AutoMapper;
using Domain.Model.Interfaces;
using DomainAbstract = Domain.Model.Abstract;


public class SearchVehiclesService : ISearchVehiclesService
{
    private readonly IRepository _repository;

    public SearchVehiclesService(
        IRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<DomainAbstract.Vehicle>> SearchAsync(SearchContext searchContextDto)
    {
       return await _repository.SearchAsync(searchContextDto.ToDomain());
    }
}