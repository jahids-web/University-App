using DLL.DataContext;
using DLL.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using University_Api.Model;

namespace DLL.Repositories
{
    public interface ICourseStudentRepository : IRepositoryBase<CourseStudent>
    {
      
    }
    public class CourseStudentRepository : RepositoryBase<CourseStudent>, ICourseStudentRepository
    {
        public CourseStudentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
