using DLL.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using University_Api.Model;

namespace DLL.Repositories
{
    public interface IDepartmentRepository:IRepositoryBase<Department>
    {
      
    }
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
