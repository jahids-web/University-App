using DLL.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using University_Api.Model;

namespace DLL.Repositories
{
    public interface IStudentRepository: IRepositoryBase<Student>
    {
        
      
    }
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
