using MB.HotWings.Utils.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MB.HotWings.Mvc.Models.Customers
{
    public class CreateUpdateCustomerViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public Gender Gender { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        // === DISPLAY ONLY not for create

        [ValidateNever]
        [Display(Name = "Name")]
        public string FullName { get; set; }
    }
}
