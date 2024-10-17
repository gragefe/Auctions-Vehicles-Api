namespace Application.Services;

using Application.DTO;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Model.Interfaces;
using DomainAbstract = Domain.Model.Abstract;


public class SearchVehiclesService : ISearchVehiclesService
{
    private readonly IMapper _mapper;
    private readonly IRepository _repository;

    public SearchVehiclesService(
        IMapper mapper,
        IRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<List<DomainAbstract.Vehicle>> SearchAsync(SearchContext searchContextDto)
    {
        var searchContext = _mapper.Map<Domain.Model.SearchContext>(searchContextDto);

       return await _repository.SearchAsync(searchContext);
    }
}