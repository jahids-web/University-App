using BLL.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Request
{
    public class StudentInsertRequestViewMolde
    {
        public string Name { get; set; }    
        public string Email { get; set; }
        public int DepartmentId { get; set; }
    }


    public class StudentInsertRequestViewMoldeValidator : AbstractValidator<StudentInsertRequestViewMolde>
    {
        private readonly IServiceProvider _serviceProvider;

        public StudentInsertRequestViewMoldeValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            RuleFor(x => x.Name).NotNull().NotEmpty()
                .MinimumLength(4).MaximumLength(50);

            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress()
                .MustAsync(EmailExists).WithMessage("Email Exists In Our System");

            RuleFor(x => x.DepartmentId).GreaterThan(0)
              .MustAsync(DepartmentExists).WithMessage("Department Not Exists In Our System");
        }

   
        private async Task<bool> EmailExists(string email, CancellationToken arg2)
        {
            if (string.IsNullOrEmpty(email))
            {
                return true;
            }
            var requiredService = _serviceProvider.GetRequiredService<IStudentService>();
            return await requiredService.EmailExists(email);
        }

        private async Task<bool> DepartmentExists(int id, CancellationToken arg2)
        {
            if(id == 0)
            {
                return true;
            }
            var requiredService = _serviceProvider.GetRequiredService<IDepartmentService>();
            return ! await requiredService.IsIdExists(id);
        }
    }
}
