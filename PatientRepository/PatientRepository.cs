using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PatientLibrary;
using System;

namespace PatientRepository
{
    public class PatientDBContext : DbContext
    {


        public PatientDBContext(DbContextOptions<PatientDBContext> options) : base(options) { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-4PN31K6;Initial Catalog=HospitalManagementDB;Integrated Security=True;");

        //    }
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            
            modelBuilder.Entity<PatientAddress>().ToTable("tblPatient");
            modelBuilder.Entity<PatientProblem>().ToTable("tblPatientProblem");
            modelBuilder.Entity<Treatment>().ToTable("tblPatientTreatment");
        }

        public DbSet<PatientAddress> patients { set; get; }

    }
}
