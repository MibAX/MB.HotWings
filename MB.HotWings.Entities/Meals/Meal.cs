using MB.HotWings.Entities.Ingredients;
using MB.HotWings.Entities.Orders;
using System.ComponentModel.DataAnnotations.Schema;

namespace MB.HotWings.Entities.Meals
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }

        public List<Ingredient> Ingredients { get; set; } = [];

        public List<Order> Orders { get; set; }
    }
}
