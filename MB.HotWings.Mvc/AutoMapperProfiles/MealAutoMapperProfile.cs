using AutoMapper;
using MB.HotWings.Entities.Meals;
using MB.HotWings.Mvc.Models.Meals;

namespace MB.HotWings.Mvc.AutoMapperProfiles
{
    public class MealAutoMapperProfile : Profile
    {
        public MealAutoMapperProfile()
        {
            CreateMap<Meal, MealViewModel>();
            CreateMap<Meal, MealDetailsViewModel>();
            
            CreateMap<CreateUpdateMealViewModel, Meal>();

            CreateMap<Meal, CreateUpdateMealViewModel>()
                .ForMember(createUpdateMealViewModel =>
                            createUpdateMealViewModel.IngredientIds,
                                opts =>
                                    opts.MapFrom(meal => meal.Ingredients.Select(meal => meal.Id)));
        }
    }
}
