using MB.HotWings.Entities.Orders;
using MB.HotWings.Utils.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MB.HotWings.Entities.Customers
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public List<Order> Orders { get; set; } = [];


        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [NotMapped]
        public int Age { 
            get
            {
                return DateTime.Now.Year - DateOfBirth.Year;
            }
        }
    }
}
