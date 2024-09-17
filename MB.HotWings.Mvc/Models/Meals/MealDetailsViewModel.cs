using MB.HotWings.Mvc.Models.Ingredients;
using System.ComponentModel.DataAnnotations.Schema;

namespace MB.HotWings.Mvc.Models.Meals
{
    public class MealDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }

        public List<IngredientViewModel> Ingredients { get; set; } = [];
    }
}
