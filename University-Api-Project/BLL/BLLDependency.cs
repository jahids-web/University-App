using BLL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class BLLDependency
    {
        public static void AllDependency (IServiceCollection service, IConfiguration configuration)
        {
            service.AddTransient<IDepartmentService, DepartmentService>();
            service.AddTransient<IStudentService, StudentService>();
        }
    }
}
