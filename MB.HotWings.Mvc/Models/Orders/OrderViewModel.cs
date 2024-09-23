﻿using MB.HotWings.Entities.Customers;
using MB.HotWings.Entities.Meals;
using MB.HotWings.Utils.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MB.HotWings.Mvc.Models.Orders
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal TotalPrice { get; set; }

        public string CustomerFullName { get; set; }

        public Location Location { get; set; }
    }
}
