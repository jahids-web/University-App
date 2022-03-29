using DLL.DataContext;
using DLL.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using University_Api.Model;

namespace DLL.Repositories
{
    public interface ICourseRepository : IRepositoryBase<Course>
    {
      
    }
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
