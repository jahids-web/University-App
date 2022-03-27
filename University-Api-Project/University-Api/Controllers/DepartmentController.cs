using DLL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using University_Api.Model;

namespace University_Api.Controllers
{
    public class DepartmentController : MainApiController
    {
        private readonly IDepartmentRepository _departmentReposatiory;

        public DepartmentController(IDepartmentRepository departmentReposatory)
        {
            _departmentReposatiory = departmentReposatory;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Department department)
        {
            return Ok(await _departmentReposatiory.InsertAsync(department));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _departmentReposatiory.GetAllAsync());
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetA(string code)
        {
            return Ok(await _departmentReposatiory.GatAAsync(code));
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> Update(string code, Department department)
        {
            return Ok(await _departmentReposatiory.UpdateAsync(code, department));
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult>Delete(string code)
        {
            return Ok(await _departmentReposatiory.DeleteAsync(code));
        }


    }
}
