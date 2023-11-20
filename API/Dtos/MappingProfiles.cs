using AutoMapper;

namespace API.Dtos
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Core.Entities.Product, ProductDTO>()
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.ProductCategory.Name));

            CreateMap<Core.Entities.Address, AddressDTO>().ReverseMap();
        }
    }
}
