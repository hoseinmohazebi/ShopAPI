using Core.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastucture.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context,ILoggerFactory loggerFactory)
        {
			var t = context.productTypes.ToList();

			try
			{
				if (!context.productBrands.Any())
				{
					var brandsData = 
						File.ReadAllText("../Infrastucture/Data/SeedData/brands.json");
					var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
					foreach (var item in brands)
					{
						context.productBrands.Add(item);
					}
					await context.SaveChangesAsync();
				}

				if (!context.productTypes.Any())
				{
					var productsData =
						File.ReadAllText("../Infrastucture/Data/SeedData/types.json");
					var types = JsonSerializer.Deserialize<List<ProductType>>(productsData);
					foreach (var item in types)
					{
						context.productTypes.Add(item);
					}
					await context.SaveChangesAsync();
				}

				if (!context.Products.Any())
				{
					var productsData =
						File.ReadAllText("../Infrastucture/Data/SeedData/products.json");
					var brands = JsonSerializer.Deserialize<List<Product>>(productsData);
					foreach (var item in brands)
					{
						context.Products.Add(item);
					}
					await context.SaveChangesAsync();
				}
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<StoreContext>();
				logger.LogError(ex.Message);
			}
        }
    }
}