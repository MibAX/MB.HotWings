using MB.HotWings.Entities.Meals;
using System.ComponentModel.DataAnnotations.Schema;

namespace MB.HotWings.Mvc.Models.Ingredients
{
    public class CreateUpdateIngredientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
    }
}
