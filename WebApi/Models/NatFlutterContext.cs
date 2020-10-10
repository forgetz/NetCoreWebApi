using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace WebApi.Models
{
    public partial class NatFlutterContext : DbContext
    {
        private IConfiguration _config;

        public NatFlutterContext(IConfiguration config)
        {
            _config = config;
        }

        public NatFlutterContext(IConfiguration config, DbContextOptions<NatFlutterContext> options)
            : base(options)
        {
            _config = config;
        }

        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //                optionsBuilder.UseSqlServer("Data source=10.10.7.35;Initial Catalog=NatFlutter;User ID=saranpong;Password=saranpong;");
                optionsBuilder.UseSqlServer(_config["ConnectionStrings:NatFlutterContext"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shop>(entity =>
            {
                entity.ToTable("SHOP");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .HasColumnName("address");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("(N'system')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("(getdate())")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(100)
                    .HasColumnName("image_url");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDelete)
                    .HasColumnName("is_delete")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Latitude)
                    .HasMaxLength(50)
                    .HasColumnName("latitude");

                entity.Property(e => e.Longitude)
                    .HasMaxLength(50)
                    .HasColumnName("longitude");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(50)
                    .HasColumnName("phone_no");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(100)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("(N'system')");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date")
                    .HasDefaultValueSql("(getdate())")
                    .HasAnnotation("Relational:ColumnType", "datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("(N'system')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("(getdate())")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Hash)
                    .HasMaxLength(100)
                    .HasColumnName("hash");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDelete)
                    .HasColumnName("is_delete")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .HasColumnName("type");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(100)
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("(N'system')");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date")
                    .HasDefaultValueSql("(getdate())")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
