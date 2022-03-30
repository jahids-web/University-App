using DLL.DataContext;
using DLL.IUnitOfWork;
using DLL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DLL
{
    public static class DLLDependency
    {
        public static void AllDependency(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("ConnectDatabase")));


            //Repository Depandency
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();

            //services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();


        }
    }
}
