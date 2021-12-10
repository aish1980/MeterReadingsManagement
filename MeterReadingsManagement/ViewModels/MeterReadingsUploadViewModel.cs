using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeterReadingsManagement.ViewModels
{
    public class MeterReadingsUploadViewModel
    {
        [Display(Name = "Meter Readings File")]
        public IFormFile CSVFile { get; set; }
    }
}
