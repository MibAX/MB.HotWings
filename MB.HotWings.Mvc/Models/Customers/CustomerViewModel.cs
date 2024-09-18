using MB.HotWings.Utils.Enums;
using System.ComponentModel.DataAnnotations;

namespace MB.HotWings.Mvc.Models.Customers
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
