using AutoMapper;

namespace ProductManagement
{
    /// <summary>
    /// 定义映射关系表
    /// </summary>
    public class ProductManagementApplicationAutoMapperProfile : Profile
    {
        public ProductManagementApplicationAutoMapperProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}