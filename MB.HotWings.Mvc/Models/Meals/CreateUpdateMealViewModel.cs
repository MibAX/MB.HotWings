using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MB.HotWings.Mvc.Models.Meals
{
    public class CreateUpdateMealViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Ingredients")]
        public List<int> IngredientIds { get; set; } = [];

        // ########### Lookups NOT for creating

        [ValidateNever]
        public MultiSelectList IngredientLookup { get; set; }
    }
}
