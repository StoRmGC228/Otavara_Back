using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GoodService : IGoodService
    {
        private readonly IGoodRepository _goodRepository;
        public GoodService(IGoodRepository goodRepository)
        {
            _goodRepository = goodRepository;
        }
        public async Task<Good> CreateGood(Good good)
        {
            return await _goodRepository.AddAsync(good);
        }

        public async Task DeleteGood(Guid Id)
        {
            var searchedGood = await _goodRepository.GetByIdAsync(Id);
            await _goodRepository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<Good>> GetAllGoods()
        {
            return await _goodRepository.GetAllAsync();
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

        public async Task<Good> GetGoodById(Guid goodId)
        {
            return await _goodRepository.GetByIdAsync(goodId);
        }

        public async Task<Good> UpdateGood(Guid id, Good good)
        {
            var updatedGood = await _goodRepository.GetByIdAsync(id);
            updatedGood.Name = good.Name;
            updatedGood.Description = good.Description; 
            updatedGood.Price = good.Price;
            updatedGood.QuantityInStock = good.QuantityInStock;
            updatedGood.CreatedAt = DateTime.Now;

            return await _goodRepository.UpdateAsync(updatedGood);
        }
    }
}
