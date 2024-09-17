using System.ComponentModel.DataAnnotations.Schema;

namespace MB.HotWings.Mvc.Models.Meals
{
    public class MealViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
    }
}
