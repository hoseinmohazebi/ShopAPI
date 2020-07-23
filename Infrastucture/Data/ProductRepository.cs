
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastucture.Data
{
    public class ProductRepository : Igenericrepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Product>> GetPrductsAsync()
        {
            return await _context.Products
            .Include(t => t.productType)
            .Include(t => t.ProductBrand)
            .ToListAsync();
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
            .Include(t => t.productType)
            .Include(t => t.ProductBrand)
            .FirstOrDefaultAsync(t=>t.Id == id);
        }
        public async Task<IReadOnlyList<ProductType>> GetPrductTypesAsync()
        {
            return await _context.productTypes.ToListAsync();

        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.productBrands.ToListAsync();
        }


    }
}