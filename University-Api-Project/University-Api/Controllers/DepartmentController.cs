using BLL.Request;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using University_Api.Model;

namespace University_Api.Controllers
{
    public class DepartmentController : MainApiController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(DepartmentInsertRequestViewMolel request)
        {
            return Ok(await _departmentService.InsertAsync(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _departmentService.GetAllAsync());
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetA(string code)
        {
            return Ok(await _departmentService.GetAAsync(code));
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> Update(string code, Department department)
        {
            return Ok(await _departmentService.UpdateAsync(code, department));
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult>Delete(string code)
        {
            return Ok(await _departmentService.DeleteAsync(code));
        }


    }
}
