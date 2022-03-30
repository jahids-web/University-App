using BLL.Request;
using DLL.IUnitOfWork;
using DLL.Model;
using DLL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using University_Api.Model;
using Utility.Exceptions;
using Utility.Model;

namespace BLL.Services
{
    public interface ICourseStudentService
    {
        Task<object> InsertAsyce(CourseAssignInsertViewModel request);
    }

    public class CourseStudentService : ICourseStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseStudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;     
        }
        public async Task<ApiSussessResponce> InsertAsyce(CourseAssignInsertViewModel request)
        {
            var isStudentAlradyEnroll = await _unitOfWork.CourseStudentRepository.FindSingleAsync(x =>
            x.CourseId == request.CourseId &&
            x.StudentId == request.StudentId);

            if(isStudentAlradyEnroll != null)
            {
                throw new ApplicationValidationExcetion("This Student Already Enroll This Course");
            }
            var courseStudent = new CourseStudent()
            {
                CourseId = request.CourseId,
                StudentId = request.StudentId
            };
            await _unitOfWork.CourseStudentRepository.CreateAsync(courseStudent);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return new ApiSussessResponce()
                {
                    Message = "Student Enroll Succssfull"
                };
            }
            throw new ApplicationValidationExcetion("Somthing  wrong for enrollment");

        }

        
    }
}

