using AutoMapper;
using MB.HotWings.Entities.Orders;
using MB.HotWings.Mvc.Models.Orders;

namespace MB.HotWings.Mvc.AutoMapperProfiles
{
    public class OrderAutoMapperProfile : Profile
    {
        public OrderAutoMapperProfile()
        {
            CreateMap<Order, OrderViewModel>();
            CreateMap<Order, OrderDetailsViewModel>();

            CreateMap<OrderCreateUpdateViewModel, Order>().ReverseMap();
        }
    }
}
