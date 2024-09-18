using MB.HotWings.Utils.Enums;
using System.ComponentModel.DataAnnotations;

namespace MB.HotWings.Mvc.Models.Customers
{
    public class CustomerDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }
        public Gender Gender { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        //public List<OrderViewModel> Orders { get; set; } = [];
    }
}
