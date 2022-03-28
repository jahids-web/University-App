using BLL.Request;
using DLL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using University_Api.Model;
using Utility.Exceptions;

namespace BLL.Services
{
    public interface IDepartmentService
    {
        Task<Department> InsertAsync(DepartmentInsertRequestViewMolel request);
        Task<List<Department>> GetAllAsync();
        Task<Department> GetAAsync(string code);
        Task<Department> UpdateAsync(string code, Department department);
        Task<Department> DeleteAsync(string code);
        Task<bool> IsCodeExists(string code);
        Task<bool> IsNameExists(string name);
      
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<Department> InsertAsync(DepartmentInsertRequestViewMolel request)
        {
            Department aDepartment = new Department();
            aDepartment.Code = request.Code;
            aDepartment.Name = request.Name;
            return await _departmentRepository.InsertAsync(aDepartment);
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _departmentRepository.GetAllAsync();
        }

        public async Task<Department> GetAAsync(string code)
        {
            var department = await _departmentRepository.GetAAsync(code);
            if(department == null)
            {
                throw new ApplicationValidationExcetion("Department Not Found");
            }
            return department;
        }
        public async Task<Department> UpdateAsync(string code, Department adepartment)
        {
            var department = await _departmentRepository.GetAAsync(code);
            if (department == null)
            {
                throw new ApplicationValidationExcetion("Department Not Found");
            }
            if (!string.IsNullOrWhiteSpace(adepartment.Code))
            {
                var existsAlreadyCode = await _departmentRepository.FindByCode(adepartment.Code);
                if (existsAlreadyCode != null)
                {
                    throw new ApplicationValidationExcetion("Your Updated Code Alredy Present System");
                }
                department.Code = adepartment.Code;
            }
            if (!string.IsNullOrWhiteSpace(adepartment.Name))
            {
                var existsAlreadyCode = await _departmentRepository.FindByName(adepartment.Name);
                if (existsAlreadyCode != null)
                {
                    throw new ApplicationValidationExcetion("Your Updated Name Alredy Present System");
                }
                department.Name = adepartment.Name;
            }
            if(await _departmentRepository.UpdateAsync(department))
            {
                return department;
            }
            throw new ApplicationValidationExcetion("Department Not Found");
        }
        public async Task<Department> DeleteAsync(string code)
        {
            var department = await _departmentRepository.GetAAsync(code);
            if(department == null){
                throw new ApplicationValidationExcetion("Department Not Found");
            }
            if( await _departmentRepository.DeleteAsync(department)){
                return department;
            }
            throw new ApplicationValidationExcetion("In Update Have Some Problem");
        }

        public async Task<bool> IsCodeExists(string code)
        {
            var department = await _departmentRepository.FindByCode(code);
            if(department == null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsNameExists(string name)
        {
            var department = await _departmentRepository.FindByName(name);
            if (department == null)
            {
                return true;
            }
            return false;
        }
    }
}
