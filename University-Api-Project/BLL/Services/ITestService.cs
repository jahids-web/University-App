using DLL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using University_Api.Model;

namespace BLL.Services
{
    public interface ITestService
    {
        Task InsertData();
    }

    public class TestService : ITestService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public TestService(IStudentRepository studentRepository, IDepartmentRepository departmentRepository)
        {
            _studentRepository = studentRepository;
            _departmentRepository = departmentRepository;
        }
        public async Task InsertData()
        {
            var department = new Department()
            {
                Code = "arts",
                Name = "art department"
            };
            var student = new Student()
            {
                Email = "art@gmail.com",
                Name = "mr arts"
            };

            await _departmentRepository.CreateAsync(department);
            await _studentRepository.CreateAsync(student);


        }
    }
}
