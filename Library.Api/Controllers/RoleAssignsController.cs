using Library.Models.Models;
using Library.Models.ViewModels;
using Library.Services.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController,Authorize]
    public class RoleAssignsController : ControllerBase
    {
        private readonly IRepository<RoleAssigns> _roleAssignsRepository;
        public RoleAssignsController(IRepository<RoleAssigns> roleAssignsRepository)
        {
            _roleAssignsRepository = roleAssignsRepository;
        }
        // GET: api/<RoleAssignsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _roleAssignsRepository.GetAllAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // GET api/<RoleAssignsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var member = await _roleAssignsRepository.GetByIdAsync(id);
                if (member == null)
                {
                    return NotFound();
                }
                return Ok(member);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // POST api/<RoleAssignsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoleAssignsDto oRoleAssignsDto)
        {
            try
            {
                var roleAssignsEntity = new RoleAssigns()
                {
                    RoleId = oRoleAssignsDto.RoleId,
                    UserId = oRoleAssignsDto.UserId,
                };
                var createMemberResponse = await _roleAssignsRepository.AddAsync(roleAssignsEntity);
                return Ok(createMemberResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // PUT api/<RoleAssignsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RoleAssignsDto oRoleAssignsDto)
        {
            try
            {
                var roleAssignsEntity = await _roleAssignsRepository.GetByIdAsync(id);
                if (roleAssignsEntity == null)
                {
                    return NotFound();
                }
                roleAssignsEntity.RoleId = oRoleAssignsDto.RoleId;
                roleAssignsEntity.UserId = oRoleAssignsDto.UserId;
                await _roleAssignsRepository.UpdateAsync(roleAssignsEntity);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // DELETE api/<RoleAssignsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var roleAssignsEntity = await _roleAssignsRepository.GetByIdAsync(id);
                if (roleAssignsEntity == null)
                {
                    return NotFound();
                }
                await _roleAssignsRepository.DeleteAsync(roleAssignsEntity);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
