using Library.Models.Models;
using Library.Models.ViewModels;
using Library.Services.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

[Route("api/[controller]")]
[ApiController,Authorize]
public class RolesController : ControllerBase
{
    private readonly IRepository<Roles> _rolesRepository;
    public RolesController(IRepository<Roles> rolesRepository)
    {
        _rolesRepository = rolesRepository;
    }
    // GET: api/<AuthorsController>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var data = await _rolesRepository.GetAllAsync();
            return Ok(data);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // GET api/<AuthorsController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var member = await _rolesRepository.GetByIdAsync(id);
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

    // POST api/<AuthorsController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RolesDto oRolesDto)
    {
        try
        {
            var rolesEntity = new Roles()
            {
                RoleName = oRolesDto.RoleName,
                RoleActiveStatus = oRolesDto.RoleActiveStatus,
            };
            var createMemberResponse = await _rolesRepository.AddAsync(rolesEntity);
            return Ok(createMemberResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // PUT api/<AuthorsController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] RolesDto oRolesDto)
    {
        try
        {
            var rolesEntity = await _rolesRepository.GetByIdAsync(id);
            if (rolesEntity == null)
            {
                return NotFound();
            }
            rolesEntity.RoleName = oRolesDto.RoleName;
            rolesEntity.RoleActiveStatus = oRolesDto.RoleActiveStatus;
            await _rolesRepository.UpdateAsync(rolesEntity);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // DELETE api/<AuthorsController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var rolesEntity = await _rolesRepository.GetByIdAsync(id);
            if (rolesEntity == null)
            {
                return NotFound();
            }
            await _rolesRepository.DeleteAsync(rolesEntity);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}
