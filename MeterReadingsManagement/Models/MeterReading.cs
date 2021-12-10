using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MeterReadingsManagement.Models
{
    public class MeterReading
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Account Id")]
        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public Account Customer { get; set; }

        [Display(Name = "Meter Reading Date")]
        public DateTime MeterReadingDateTime { get; set; }

        [Display(Name = "Meter Reading Value")]
        public int MeterReadValue { get; set; }
    }
}
