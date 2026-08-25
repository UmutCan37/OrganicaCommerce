using AutoMapper;
using OrganicaCommerce.Application.CQRS.Admin.Results;
using OrganicaCommerce.Application.CQRS.Cart.Results;
using OrganicaCommerce.Application.CQRS.Categories.Results;
using OrganicaCommerce.Application.CQRS.Orders.Results;
using OrganicaCommerce.Application.CQRS.Products.Results;
using OrganicaCommerce.Contracts.Admin;
using OrganicaCommerce.Contracts.Cart;
using OrganicaCommerce.Contracts.Categories;
using OrganicaCommerce.Contracts.Orders;
using OrganicaCommerce.Contracts.Products;

namespace OrganicaCommerce.WebApi.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetProductListResult, ProductListItemResponse>();
            CreateMap<GetProductByIdResult, ProductDetailResponse>();

            CreateMap<GetCategoryListResult, CategoryResponse>();

            CreateMap<GetCartItemResult, CartItemResponse>();
            CreateMap<GetCartResult, CartResponse>();

            CreateMap<GetOrderListResult, OrderListItemResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<GetOrderItemResult, OrderItemResponse>();
            CreateMap<GetOrderByIdResult, OrderDetailResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            CreateMap<CreateOrderResult, CreateOrderResponse>();

            CreateMap<LowStockProductResult, LowStockProductResponse>();
            CreateMap<GetDashboardStatsResult, DashboardStatsResponse>();
        }
    }
}