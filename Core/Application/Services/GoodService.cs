namespace Application.Services;

using System.Threading;
using AutoMapper;
using Domain.DtoEntities;
using Domain.Entities;
using Interfaces;

public class GoodService : BaseService<Good>, IGoodService
{
    private readonly IGoodRepository _goodRepository;

    private readonly IMapper _mapper;

    public GoodService(IGoodRepository goodRepository, IMapper mapper) : base(goodRepository, mapper)
    {
        _goodRepository = goodRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Good>> GetAllSortedByNameAsync(bool ascending)
    {
        return await _goodRepository.GetAllSortedByNameAsync(ascending);
    }

    public async Task<IEnumerable<Good>> GetAllSortedByQuantityAsync(bool ascending)
    {
        return await _goodRepository.GetAllSortedByQuantityAsync(ascending);
    }

    public async Task<IEnumerable<Good>> GetAllSortedByTimeAsync(bool ascending)
    {
        return await _goodRepository.GetAllSortedByTimeAsync(ascending);
    }
}