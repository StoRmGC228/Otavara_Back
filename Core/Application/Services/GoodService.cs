using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GoodService :BaseService<Good>, IGoodService
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
}
