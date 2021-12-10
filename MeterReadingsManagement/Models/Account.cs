using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeterReadingsManagement.Models
{
    public class Account
    {
        [Key]
        [Display(Name = "Account Id")]
        public int AccountId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
