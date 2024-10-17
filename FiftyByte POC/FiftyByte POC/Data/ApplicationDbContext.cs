using FiftyByte_POC.Models;
using FiftyByte_POC.Models.leave_management;
using Microsoft.EntityFrameworkCore;

namespace FiftyByte_POC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<CompanyLogo> CompanyLogos { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Translation> Translations { get; set; }
        public DbSet<LeaveApplication> LeaveApplications { get; set; }
        public DbSet<LeavePolicy> LeavePolicies { get; set; }
        public DbSet<CostCenter> CostCenters { get; set; }
    }
}
