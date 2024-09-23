using MB.HotWings.Entities.Customers;
using MB.HotWings.Entities.Meals;
using MB.HotWings.Utils.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace MB.HotWings.Mvc.Models.Orders
{
    public class OrderCreateUpdateViewModel
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }

        public string? Note { get; set; }

        public Location Location { get; set; }

        public int CustomerId { get; set; }
        public List<int> MealIds { get; set; } = [];

        //----------------------- LookUps -----------------------

        [ValidateNever]
        public SelectList CustomersLookUp { get; set; }

        [ValidateNever]
        public MultiSelectList MealsLookUp { get; set; }  
    }
}
