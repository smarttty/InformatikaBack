using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TestODataBackend.Models
{
    public partial class testContext : DbContext
    {
        public testContext()
        {
        }

        public testContext(DbContextOptions<testContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=test;Username=postgres;Password=P@ssw0rd");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp")
                .HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Primarykey)
                    .HasName("Order_pkey");

                entity.ToTable("Order");

                entity.Property(e => e.Primarykey)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("primarykey");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.UserPrimarykey).HasColumnName("user_primarykey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserPrimarykey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Order_user_primarykey_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e._Primarykey)
                    .HasName("User_pkey");

                entity.ToTable("User");

                entity.Property(e => e._Primarykey)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("primarykey");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("login");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
