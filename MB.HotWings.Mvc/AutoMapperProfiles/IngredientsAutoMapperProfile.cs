using AutoMapper;
using MB.HotWings.Entities.Ingredients;
using MB.HotWings.Mvc.Models.Ingredients;

namespace MB.HotWings.Mvc.AutoMapperProfiles
{
    public class IngredientsAutoMapperProfile : Profile
    {
        public IngredientsAutoMapperProfile()
        {
            CreateMap<Ingredient, IngredientViewModel>();
            CreateMap<Ingredient, IngredientDetailsViewModel>();
            CreateMap<CreateUpdateIngredientViewModel, Ingredient>().ReverseMap();
        }
    }
}
