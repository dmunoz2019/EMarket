using Core.Entities;

namespace API.Dtos
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int Stock { get; set; }

        public string Brand { get; set; }
        public int ProductBrandId { get; set; }

        public int ProductCategoryId { get; set; }

        public string Category { get; set; }

    }
}
