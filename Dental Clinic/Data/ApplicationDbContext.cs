using Dental_Clinic.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dental_Clinic.Data
{
    public class ApplicationDbContext : DbContext
    {
       
        public DbSet<EmployeeModel> employees { get; set; } = null!;
        public DbSet<EntryFormModel> ListOfRecords { get; set; } = null!;
        // public DbSet<ProcedureModel> ListOfProcedure { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
