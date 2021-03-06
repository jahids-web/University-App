using BLL.Request;
using DLL.IUnitOfWork;
using DLL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using University_Api.Model;
using Utility.Exceptions;

namespace BLL.Services
{
    public interface IStudentService
    {
        Task<Student> InsertAsync(StudentInsertRequestViewMolde studentRequest);
        Task<List<Student>> GetAllAsync();
        Task<Student> GetAAsync(string email);
        Task<Student> UpdateAsync(string email, Student student);
        Task<Student> DeleteAsync(string email);

        Task<bool> EmailExists(string email);

        Task<bool> IsIdExists(int studentId);
    }

    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Student> InsertAsync(StudentInsertRequestViewMolde studentRequest)
        {
            var student = new Student()
            {
                Email = studentRequest.Email,
                Name = studentRequest.Name,
                DepartmentId = studentRequest.DepartmentId
            };
            await _unitOfWork.StudentRepository.CreateAsync(student);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return student;
            }
            throw new ApplicationValidationExcetion("Insert Have Some Problem");
        }
        public async Task<List<Student>> GetAllAsync()
        {
            return await _unitOfWork.StudentRepository.GetList();
        }
        public async Task<Student> GetAAsync(string email)
        {
            return await _unitOfWork.StudentRepository.FindSingleAsync(x =>x.Email == email);
        }
        public async Task<Student> UpdateAsync(string email, Student student)
        {
            var dbStudent = await _unitOfWork.StudentRepository.FindSingleAsync(x => x.Email == email);
            if(dbStudent == null)
            {
                throw new AccessViolationException("Student Not Found");
            }

            dbStudent.Name = student.Name;
            dbStudent.Email = student.Email;
            _unitOfWork.StudentRepository.Update(dbStudent);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return dbStudent;
            }
            throw new ApplicationValidationExcetion("Update Has Some Issue");
        }
        public async Task<Student> DeleteAsync(string email)
        {
            var dbStudent = await _unitOfWork.StudentRepository.FindSingleAsync(x => x.Email == email);
            if (dbStudent == null)
            {
                throw new AccessViolationException("Student Not Found");
            }
            _unitOfWork.StudentRepository.Delete(dbStudent);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return dbStudent;
            }
            throw new ApplicationValidationExcetion("Delete Has Some Issue");
        }

        public async Task<bool> EmailExists(string email)
        {
            var student = await _unitOfWork.StudentRepository.FindSingleAsync(x => x.Email == email);
            if(student == null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsIdExists(int id)
        {
            var student = await _unitOfWork.StudentRepository.FindSingleAsync(x => x.StudentId == id);
            if (student == null)
            {
                return true;
            }
            return false;
        }

    }
}
