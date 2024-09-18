using AutoMapper;
using MB.HotWings.Entities.Customers;
using MB.HotWings.Mvc.Models.Customers;

namespace MB.HotWings.Mvc.AutoMapperProfiles
{
    public class CustomerAutoMapperProfile : Profile
    {
        public CustomerAutoMapperProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<Customer, CustomerDetailsViewModel>();

            CreateMap<CreateUpdateCustomerViewModel, Customer>().ReverseMap();
        }
    }
}
