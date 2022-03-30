using BLL.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using University_Api.Model;

namespace BLL.Request
{
    public class CourseAssignInsertViewModel
    {
        public int StudentId { get; set; }   
        public int CourseId { get; set; }
    }

    public class CourseAssignInsertViewModelValidator : AbstractValidator<CourseAssignInsertViewModel>
    {
        private readonly IServiceProvider _serviceProvider;

        public CourseAssignInsertViewModelValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            RuleFor(x => x.StudentId).NotNull().NotEmpty().GreaterThan(0)
                .MustAsync(studentIdExists).WithMessage("Srudent id Exists In Our System");

            RuleFor(x => x.CourseId).NotNull().NotEmpty().GreaterThan(0)
                .MustAsync(courseIdExists).WithMessage("Course Exists In Our System");
        }

     
        private async Task<bool> studentIdExists(int studentId, CancellationToken arg2)
        {
            var requiredService = _serviceProvider.GetRequiredService<IStudentService>();
            return await requiredService.IsIdExists(studentId);
        }

        private async Task<bool> courseIdExists(int courseId, CancellationToken arg2)
        {
           
            var requiredService = _serviceProvider.GetRequiredService<ICourseService>();
            return await requiredService.IsIdExists(courseId);
        }
    }
}
