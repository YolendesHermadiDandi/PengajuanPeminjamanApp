using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _roleRepository.GetAll();

            if (!result.Any())
            {
                return NotFound("Tidak ditemukan");
            }

            var data = result.Select(x => (RoleDto)x);

            return Ok(new ResponseOkHandler<IEnumerable<RoleDto>>(data));
        }
    }
}
