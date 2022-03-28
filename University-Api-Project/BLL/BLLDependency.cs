using BLL.Request;
using BLL.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BLL
{
    public static class BLLDependency
    {
        public static void AllDependency (IServiceCollection service, IConfiguration configuration)
        {
            service.AddTransient<IDepartmentService, DepartmentService>();
            service.AddTransient<IStudentService, StudentService>();

            AllFluentValidationDependency(service);
        }

        private static void AllFluentValidationDependency(IServiceCollection service)
        {
            service.AddTransient<IValidator<DepartmentInsertRequestViewMolel>, DepartmentInsertRequestViewMolelValidator>();
        }
    }
}
