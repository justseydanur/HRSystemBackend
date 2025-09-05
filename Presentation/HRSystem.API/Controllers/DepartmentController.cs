using HRSystem.Application.DTOs.Departments;
using HRSystem.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        //[Authorize] // sadece login olanlar
        public async Task<ActionResult<List<ResultDepartmentDTO>>> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id:int}")]
        //[Authorize]
        public async Task<ActionResult<ResultDepartmentDTO>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        //Authorize(Roles = "Admin")] // istersen rolsüz bırak
        public async Task<ActionResult<int>> Create([FromBody] CreateDepartmentDTO dto)
        {
            var newId = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newId }, newId);
        }

        [HttpPut("{id:int}")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDepartmentDTO dto)
        {
            if (id != dto.Id) return BadRequest("Id uyuşmuyor.");
            var ok = await _service.UpdateAsync(dto);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
