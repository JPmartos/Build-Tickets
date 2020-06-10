using BuildTicket.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace BuildTicket.DataAccess.EntityFramework
{
    public partial class dbBuildTicketContext : DbContext
    {
        public dbBuildTicketContext()
        {

        }

        public dbBuildTicketContext(DbContextOptions<dbBuildTicketContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Ticket> Ticket { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.Date_Ocurrence)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.Priority)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnType("int");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnType("int");

                entity.Property(e => e.Date_Change)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnType("bit");


            });

        }
    }
}
