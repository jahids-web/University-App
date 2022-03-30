using BLL.Request;
using DLL.IUnitOfWork;
using DLL.Repositories;
using System.Collections.Generic;
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
        Task<Department> UpdateAsync(string code, DepartmentInsertRequestViewMolel adepartment);
        Task<Department> DeleteAsync(string code);
        Task<bool> IsCodeExists(string code);
        Task<bool> IsNameExists(string name);

        Task<bool> IsIdExists(int id);
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Department> InsertAsync(DepartmentInsertRequestViewMolel request)
        {
            Department aDepartment = new Department();
            aDepartment.Code = request.Code;
            aDepartment.Name = request.Name;
            await _unitOfWork.DepartmentRepository.CreateAsync(aDepartment);
            if(await _unitOfWork.SaveCompletedAsync())
            {
                return aDepartment;
            }
           
            throw new ApplicationValidationExcetion("Department Insert Have Some Problem");
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _unitOfWork.DepartmentRepository.GetList();
        }

        public async Task<Department> GetAAsync(string code)
        {
            var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.Code == code);
            if (department == null)
            {
                throw new ApplicationValidationExcetion("Department Not Found");
            }
            return department;
        }
        public async Task<Department> UpdateAsync(string code, DepartmentInsertRequestViewMolel adepartment)
        {
            var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x=>x.Code == code);
            if (department == null)
            {
                throw new ApplicationValidationExcetion("Department Not Found");
            }
            if (!string.IsNullOrWhiteSpace(adepartment.Code))
            {
                var existsAlreadyCode = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.Code == adepartment.Code);
                if (existsAlreadyCode != null)
                {
                    throw new ApplicationValidationExcetion("Your Updated Code Alredy Present System");
                }
                department.Code = adepartment.Code;
            }
            if (!string.IsNullOrWhiteSpace(adepartment.Name))
            {
                var existsAlreadyCode = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.Name == adepartment.Name);
                if (existsAlreadyCode != null)
                {
                    throw new ApplicationValidationExcetion("Your Updated Name Alredy Present System");
                }
                department.Name = adepartment.Name;
            }
            _unitOfWork.DepartmentRepository.Update(department);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return department;
            }
            throw new ApplicationValidationExcetion("Department Not Found");
        }
        public async Task<Department> DeleteAsync(string code)
        {
            var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.Code == code);
            if (department == null){
                throw new ApplicationValidationExcetion("Department Not Found");
            }
            _unitOfWork.DepartmentRepository.Delete(department);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return department;
            }
            throw new ApplicationValidationExcetion("In Update Have Some Problem");
        }

        public async Task<bool> IsCodeExists(string code)
        {
            var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.Code == code);
            if (department == null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsNameExists(string name)
        {
            var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.Name == name);
            if (department == null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsIdExists(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.DepartmentId == id);
            if (department == null)
            {
                return true;
            }
            return false;
        }
    }
}
