using System.ComponentModel.DataAnnotations.Schema;

namespace MB.HotWings.Mvc.AppSettings
{
    public class HotWingsSettings
    {
        [Column(TypeName = "decimal(6,2)")]
        public decimal ProfitMargin { get; set; }
    }
}
