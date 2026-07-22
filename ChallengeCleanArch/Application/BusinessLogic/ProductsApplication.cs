using Domain.Entities;
using Domain.Interfaces.Application;
using Domain.Interfaces.Persistence;

namespace Application.BusinessLogic
{
    public class ProductsApplication : IProductsApplication
    {
        private readonly IProductsPersistence _persistence;

        public ProductsApplication(IProductsPersistence persistence)
        {
            _persistence = persistence;
        }

        public async Task<List<Products>> GetRecords()
        {
            return await _persistence.GetRecords();
        }

        public async Task<Products> GetRecord(int id)
        {
            return await _persistence.GetRecord(id);
        }

        public async Task<int> Insert(Products entity)
        {
            return await _persistence.Insert(entity);
        }

        public async Task<int> Update(Products entity)
        {
            return await _persistence.Update(entity);
        }

        public async Task<int> Delete(int id)
        {
            return await _persistence.Delete(id);
        }
    }
}
