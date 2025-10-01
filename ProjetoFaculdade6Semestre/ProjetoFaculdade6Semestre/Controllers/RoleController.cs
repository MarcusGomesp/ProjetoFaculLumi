using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoFaculdade6Semestre.Interface;
using ProjetoFaculdade6Semestre.Model.CadastroLumi;

namespace ProjetoFaculdade6Semestre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRole _context;

        public RoleController(IRole context)
        {
            _context = context;
        }

        //Get: api/Role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var role = await _context.ListToAsync();
            return Ok(role);
        }

        //Get : api/Role/Id

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoleID(int id)
        {
            var role = await _context.ListPorIdAsync(id);

            if (role == null)
                return NotFound();

            return Ok(role);
        }

        //Post: api/Roles/adicionar
        [HttpPost("adicionar")]
        public async Task<ActionResult<Role>> PostRole([FromBody] Role role)
        {
            try
            {
                var result = await _context.AdicionarAsync(role);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest($"Error ao carregar Role: {ex.Message}");
            }
        }


        // DELETE: api/Role/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleAsync([FromRoute] int id)
        {
            var success = await _context.DeletarAsync(id);

            if (!success)
                return NotFound($"Role with ID {id} not found.");


            return NoContent();
        }


    }
}