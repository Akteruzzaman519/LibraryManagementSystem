using Library.Models.Models;
using Library.Models.ViewModels;
using Library.Services.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
public class UsersController : ControllerBase
{
    private readonly IRepository<Users> _usersRepository;
    public UsersController(IRepository<Users> authorsRepository)
    {
        _usersRepository = authorsRepository;
    }

    // GET: api/<UsersController>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var data = await _usersRepository.GetAllAsync();
            return Ok(data);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var member = await _usersRepository.GetByIdAsync(id);
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

    // POST api/<UsersController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UsersDto oUsersDto)
    {
        try
        {
            var usersEntity = new Users()
            {
                UserName = oUsersDto.UserName,
                Password = oUsersDto.Password,
            };
            var createMemberResponse = await _usersRepository.AddAsync(usersEntity);
            return Ok(createMemberResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UsersDto oUsersDto)
    {
        try
        {
            var usersEntity = await _usersRepository.GetByIdAsync(id);
            if (usersEntity == null)
            {
                return NotFound();
            }
            usersEntity.UserName = oUsersDto.UserName;
            usersEntity.Password = oUsersDto.Password;
            await _usersRepository.UpdateAsync(usersEntity);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // DELETE api/<UsersController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var usersEntity = await _usersRepository.GetByIdAsync(id);
            if (usersEntity == null)
            {
                return NotFound();
            }
            await _usersRepository.DeleteAsync(usersEntity);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}
