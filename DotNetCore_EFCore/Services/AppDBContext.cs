
using DotNetCore_EFCore_CQRS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace DotNetCore_EFCore_CQRS.Services
{
    public class AppDBContext:DbContext
    {


        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        { 
        
        }

        public DbSet<Employee> employee { get; set; }
        public DbSet<Department> department { get; set; }
        public DbSet<Designation> designation { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // When you have mismatch in table name
            //modelBuilder.Entity<Employee>().ToTable("tblEmployee");


            // When you have mismatch in table name and columns and keys

            modelBuilder.Entity<Employee>(entity =>
            {

                entity.ToTable("tblEmployee");
                entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd();

                entity.Property(e => e.EName).HasColumnName("EName").HasMaxLength(64);
                entity.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired();
                entity.HasIndex(e => e.EName).IsUnique().HasDatabaseName("UX_Name");
                entity.Property<DateTime>("UpdatedDate");
                entity.Property<bool>("IsDeleted");
                // 🔥 SHADOW PROPERTIES (MUST BE DECLARED)
                entity.Property<string?>("CreatedBy");
                entity.Property<DateTime?>("UpdatedDate");
                entity.Property<bool>("IsDeleted");
                // 🔥 OPTIONAL: soft delete filter

                entity.HasQueryFilter(e => !EF.Property<bool>(e, "IsDeleted"));

                entity.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Designation)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DesignationId)
                .OnDelete(DeleteBehavior.SetNull);

            });


            modelBuilder.Entity<Department>(entity => entity.ToTable("tblDepartment"));
            modelBuilder.Entity<Designation>(entity => entity.ToTable("tblDesignations"));



        }
        //not recomanded ignore below and config in program.cs with appsettings.json
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer("connection string");
        //}
    }
}
