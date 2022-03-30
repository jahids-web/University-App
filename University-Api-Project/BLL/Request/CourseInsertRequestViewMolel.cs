using BLL.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Request
{
    public class CourseInsertRequestViewMolel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Credit { get; set; }
    }
    public class CourseInsertRequestViewMolelValidator : AbstractValidator<CourseInsertRequestViewMolel>
    {
        private readonly IServiceProvider _serviceProvider;

        public CourseInsertRequestViewMolelValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            RuleFor(x => x.Name).NotNull().NotEmpty()
                .MinimumLength(4).MaximumLength(25).MustAsync(NameExists).WithMessage("Name Exists In Our System");

            RuleFor(x => x.Code).NotNull().NotEmpty()
                .MinimumLength(3).MaximumLength(10).MustAsync(CodeExists).WithMessage("Code Exists In Our System");

            RuleFor(x => x.Credit).NotNull().NotEmpty();
              
        }

        private async Task<bool> CodeExists(string code, CancellationToken arg2)
        {
            if (string.IsNullOrEmpty(code))
            {
                return true;
            }
            var requiredService = _serviceProvider.GetRequiredService<ICourseService>();
            return await requiredService.IsCodeExists(code);
        }

        private async Task<bool> NameExists(string name, CancellationToken arg2)
        {
            if (string.IsNullOrEmpty(name))
            {
                return true;
            }
            var requiredService = _serviceProvider.GetRequiredService<ICourseService>();
            return await requiredService.IsCodeExists(name);
        }
    }
}
