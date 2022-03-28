using BLL.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Request
{
    public class DepartmentInsertRequestViewMolel
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
    public class DepartmentInsertRequestViewMolelValidator : AbstractValidator<DepartmentInsertRequestViewMolel>
    {
        private readonly IServiceProvider _serviceProvider;

        public DepartmentInsertRequestViewMolelValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            RuleFor(x => x.Name).NotNull().NotEmpty()
                .MinimumLength(4).MaximumLength(25).MustAsync(NameExists).WithMessage("Name Ecists In Our System");

            RuleFor(x => x.Code).NotNull().NotEmpty()
                .MinimumLength(3).MaximumLength(10).MustAsync(CodeExists).WithMessage("Code Ecists In Our System"); ;
        }

        private async Task<bool> CodeExists(string code, CancellationToken arg2)
        {
            if (string.IsNullOrEmpty(code))
            {
                return true;
            }
            var requiredService = _serviceProvider.GetRequiredService<IDepartmentService>();
            return await requiredService.IsCodeExists(code);
        }

        private async Task<bool> NameExists(string name, CancellationToken arg2)
        {
            if (string.IsNullOrEmpty(name))
            {
                return true;
            }
            var requiredService = _serviceProvider.GetRequiredService<IDepartmentService>();
            return await requiredService.IsCodeExists(name);
        }
    }
}
