using DLL.Model;
using DLL.Model.Interfaces;
using GSF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using University_Api.Model;

namespace DLL.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }


        private const string IsDeletedProperty = "IsDeleted";

        private static readonly MethodInfo _propertyMethod = typeof(EF)
            .GetMethod(nameof(EF.Property), BindingFlags.Static | BindingFlags.Public).MakeGenericMethod(typeof(bool));
        private static LambdaExpression GetIsDeletedRestriction(Type type)
        {
            var parm = Expression.Parameter(type, "it");
            var prop = Expression.Call(_propertyMethod, parm, Expression.Constant(IsDeletedProperty));
            var condition = Expression.MakeBinary(ExpressionType.Equal, prop, Expression.Constant(false));
            var lambda = Expression.Lambda(condition, parm);
            return lambda;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entity.ClrType) == true)
                {
                    entity.AddProperty(IsDeletedProperty, typeof(bool));
                    modelBuilder.Entity(entity.ClrType).HasQueryFilter(GetIsDeletedRestriction(entity.ClrType));

                }
            }
            modelBuilder.Entity<CourseStudent>()
           .HasKey(bc => new { bc.CourseId, bc.StudentId });
            modelBuilder.Entity<CourseStudent>()
                .HasOne(bc => bc.Couse)
                .WithMany(b => b.CourseStudents)
                .HasForeignKey(bc => bc.CourseId);
            modelBuilder.Entity<CourseStudent>()
                .HasOne(bc => bc.Student)
                .WithMany(c => c.CourseStudents)
                .HasForeignKey(bc => bc.StudentId);
            base.OnModelCreating(modelBuilder);
           
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSavingData();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void OnBeforeSavingData()
        {
            var entries = ChangeTracker.Entries()
              .Where(e => e.State != EntityState.Detached && e.State != EntityState.Unchanged);
            foreach (var entry in entries)
            {
                if (entry.Entity is ITrackable trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Deleted:
                            entry.Property(IsDeletedProperty).CurrentValue = true;
                            entry.State = EntityState.Modified;
                            break;
                    }
                }

            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
