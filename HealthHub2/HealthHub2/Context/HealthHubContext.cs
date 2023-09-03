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
        public DbSet<Models.Doctor> Doctor { get; set; }
        public DbSet<Models.Patient> Patient { get; set; }
        public DbSet<Models.Rating> Rating { get; set; }
        public DbSet<Models.MedicalImage> MedicalImage { get; set; }
        public DbSet<Models.Appointment> Appointment { get; set; }
        public DbSet<Models.GeoLocation> GeoLocation { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Remove the pluralizing table name convention
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
