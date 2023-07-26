using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace project1.Models
{
    public partial class AuthenticationConfig : DbContext
    {
        public AuthenticationConfig()
            : base("name=AuthenticationConfig")
        {
        }

        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<RolesUsuario> RolesUsuario { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>()
                .Property(e => e.Description)
                .IsUnicode(false);
        }
    }
}
