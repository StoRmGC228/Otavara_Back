namespace Application.Services;

using Domain.Entities;
using Interfaces;

public class GoodService : BaseService<Good>, IGoodService
{
    private readonly IGoodRepository _goodRepository;

    public GoodService(IGoodRepository goodRepository) : base(goodRepository)
    {
        _goodRepository = goodRepository;
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