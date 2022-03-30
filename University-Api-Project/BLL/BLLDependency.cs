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
            service.AddTransient<ICourseService, CourseService>();
            service.AddTransient<ICourseStudentService, CourseStudentService>();

            AllFluentValidationDependency(service);
        }

        private static void AllFluentValidationDependency(IServiceCollection service)
        {
            service.AddTransient<IValidator<DepartmentInsertRequestViewMolel>, DepartmentInsertRequestViewMolelValidator>();
            service.AddTransient<IValidator<StudentInsertRequestViewMolde>, StudentInsertRequestViewMoldeValidator>();
            service.AddTransient<IValidator<CourseInsertRequestViewMolel>, CourseInsertRequestViewMolelValidator>();
            service.AddTransient<IValidator<CourseAssignInsertViewModel>, CourseAssignInsertViewModelValidator>();
        }
    }
}
