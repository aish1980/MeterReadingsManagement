using MeterReadingsManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeterReadingsManagement.Common
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options)
        : base(options)
        {
        }

        public DbSet<MeterReading> MeterReadings { get; set; }


    }
}
