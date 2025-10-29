using ECommerceG02.Domian.Contracts;
using ECommerceG02.Domian.Models.Products;
using ECommerceG02.Presistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerceG02.Presistence.Seed
{
    public class DataSeed : IDataSeed
    {
        private readonly StoreDbContext _context;

        public DataSeed(StoreDbContext context)
        {
            this._context = context;
        }
        public void DataSeeding()
        {
            if (_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }

            if (!_context.ProductBrands.Any())
            {
                var productbrandData = File.ReadAllText(@"..\Infrastructure\ECommerceG02.Presistence\Data\brands.json");
                var product_brand = JsonSerializer.Deserialize<List<ProductBrand>>(productbrandData);

                if (product_brand != null && product_brand.Any())
                {
                    _context.ProductBrands.AddRange(product_brand);
                }
            }

            if (!_context.ProductTypes.Any())
            {
                var producttypeData = File.ReadAllText(@"..\Infrastructure\ECommerceG02.Presistence\Data\types.json");
                var product_type = JsonSerializer.Deserialize<List<ProductType>>(producttypeData);

                if (product_type != null && product_type.Any())
                {
                    _context.ProductTypes.AddRange(product_type);
                }
            }

            if (!_context.Products.Any())
            {
                var productData = File.ReadAllText(@"..\Infrastructure\ECommerceG02.Presistence\Data\products.json");
                var productD = JsonSerializer.Deserialize<List<Product>>(productData);

                if (productD != null && productD.Any())
                {
                    _context.Products.AddRange(productD);
                }
            }
            _context.SaveChanges();

        }
    }
}
