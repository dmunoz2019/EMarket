using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogic.Data.Load
{
    public class LoadDbContextData
    {
        public static async Task LoadDataAsync(MarketDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandsData = System.IO.File.ReadAllText("../BusinessLogic/Data/Load/ProductBrand.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    foreach (var brand in brands)
                    {
                        context.ProductBrands.Add(brand);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.ProductCategories.Any())
                {
                    var categoriesData = System.IO.File.ReadAllText("../BusinessLogic/Data/Load/ProductCategory.json");
                    var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
                    foreach (var category in categories)
                    {
                        context.ProductCategories.Add(category);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData = System.IO.File.ReadAllText("../BusinessLogic/Data/Load/Product.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var product in products)
                    {
                        context.Products.Add(product);
                    }
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<LoadDbContextData>();
                logger.LogError(ex.Message);
            }
            

        }
            }
        }
