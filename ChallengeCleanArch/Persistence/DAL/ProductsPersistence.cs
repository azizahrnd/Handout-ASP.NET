using Domain.Entities;
using Domain.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.DAL
{
    public class ProductsPersistence : IProductsPersistence
    {
        private readonly AppDbContext _context;

        public ProductsPersistence(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Products>> GetRecords()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Products> GetRecord(int id)
        {
            return await _context.Products.Where(x => x.ProductID == id).FirstAsync();
        }

        public async Task<int> Insert(Products entity)
        {
            _context.Products.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(Products entity)
        {
            var product = await _context.Products.FindAsync(entity.ProductID);
            product.ProductName = entity.ProductName;
            product.Price = entity.Price;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }
    }
}
