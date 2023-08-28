using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace HealthHub2.Context
{
    public class HealthHubContext : DbContext
    {
        //public HealthHubContext() : base("HealthHub2")
        //{
        //}

        // DbSets
        public DbSet<Models.Doctor> Doctors { get; set; }
        public DbSet<Models.Patient> Patients { get; set; }
        public DbSet<Models.Rating> Ratings { get; set; }
        public DbSet<Models.ServiceType> ServiceTypes { get; set; }
        public DbSet<Models.MedicalImage> MedicalImages { get; set; }
        public DbSet<Models.Appointment> Appointments { get; set; }
        public DbSet<Models.GeoLocation> GeoLocations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Remove the pluralizing table name convention
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
