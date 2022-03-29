using BLL.Request;
using DLL.Model;
using DLL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using University_Api.Model;
using Utility.Exceptions;

namespace BLL.Services
{
    public interface ICourseService
    {
        Task<Course> InsertAsync(CourseInsertRequestViewMolel request);
        Task<List<Course>> GetAllAsync();
        Task<Course> GetAAsync(string code);
        Task<Course> UpdateAsync(string code, Course aCourse);
        Task<Course> DeleteAsync(string code);
        Task<bool> IsCodeExists(string code);
        Task<bool> IsNameExists(string name);

      
    }

    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<Course> InsertAsync(CourseInsertRequestViewMolel request)
        {
            var course = new Course();
            course.Code = request.Code;
            course.Name = request.Name;
            course.Credit = request.Credit;
            await _courseRepository.CreateAsync(course);
            if(await _courseRepository.SaveCompletedAsync())
            {
                return course;
            }
           
            throw new ApplicationValidationExcetion("Department Insert Have Some Problem");
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await _courseRepository.GetList();
        }

        public async Task<Course> GetAAsync(string code)
        {
            var course = await _courseRepository.FindSingleAsync(x => x.Code == code);
            if (course == null)
            {
                throw new ApplicationValidationExcetion("Department Not Found");
            }
            return course;
        }
        public async Task<Course> UpdateAsync(string code, Course aCourse)
        {
            var course = await _courseRepository.FindSingleAsync(x=>x.Code == code);
            if (course == null)
            {
                throw new ApplicationValidationExcetion("Department Not Found");
            }
            if (!string.IsNullOrWhiteSpace(aCourse.Code))
            {
                var existsAlreadyCode = await _courseRepository.FindSingleAsync(x => x.Code == aCourse.Code);
                if (existsAlreadyCode != null)
                {
                    throw new ApplicationValidationExcetion("Your Updated Code Alredy Present System");
                }
                course.Code = aCourse.Code;
            }
            if (!string.IsNullOrWhiteSpace(aCourse.Name))
            {
                var existsAlreadyCode = await _courseRepository.FindSingleAsync(x => x.Name == aCourse.Name);
                if (existsAlreadyCode != null)
                {
                    throw new ApplicationValidationExcetion("Your Updated Name Alredy Present System");
                }
                course.Name = aCourse.Name;
            }
            _courseRepository.Update(course);
            if (await _courseRepository.SaveCompletedAsync())
            {
                return course;
            }
            throw new ApplicationValidationExcetion("Course Not Found");
        }
        public async Task<Course> DeleteAsync(string code)
        {
            var course = await _courseRepository.FindSingleAsync(x => x.Code == code);
            if (course == null){
                throw new ApplicationValidationExcetion("Course Not Found");
            }
            _courseRepository.Delete(course);
            if (await _courseRepository.SaveCompletedAsync())
            {
                return course;
            }
            throw new ApplicationValidationExcetion("In Update Have Some Problem");
        }

        public async Task<bool> IsCodeExists(string code)
        {
            var department = await _courseRepository.FindSingleAsync(x => x.Code == code);
            if (department == null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsNameExists(string name)
        {
            var department = await _courseRepository.FindSingleAsync(x => x.Name == name);
            if (department == null)
            {
                return true;
            }
            return false;
        }

      
    }
}
