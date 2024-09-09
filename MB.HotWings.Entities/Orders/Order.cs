using MB.HotWings.Entities.Customers;
using MB.HotWings.Entities.Meals;
using MB.HotWings.Utils.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MB.HotWings.Entities.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal TotalPrice { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string? Note { get; set; }

        public Location Location { get; set; }

        public List<Meal> Meals { get; set; } = [];
    }
}
