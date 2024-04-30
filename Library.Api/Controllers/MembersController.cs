using Library.Models.Models;
using Library.Models.ViewModels;
using Library.Services.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Api.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
public class MembersController : ControllerBase
{
    private readonly IRepository<Members> _membersRepository;
    public MembersController(IRepository<Members> membersRepository)
    {
        _membersRepository = membersRepository;
    }

    // GET: api/<MembersController>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var data = await _membersRepository.GetAllAsync();
            return Ok(data);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // GET api/<MembersController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var member = await _membersRepository.GetByIdAsync(id);
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

    // POST api/<MembersController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MembersDto oMembersDto)
    {
        try
        {
            var membersEntity = new Members()
            {
                FirstName = oMembersDto.FirstName,
                LastName = oMembersDto.LastName,
                Email = oMembersDto.Email,
                PhoneNumber = oMembersDto.PhoneNumber,
                RegistrationDate = Convert.ToDateTime(DateTime.Now)
            };
            var createMemberResponse = await _membersRepository.AddAsync(membersEntity);
            return Ok(createMemberResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // PUT api/<MembersController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] MembersDto oMembersDto)
    {
        try
        {
            var membersEntity = await _membersRepository.GetByIdAsync(id);
            if (membersEntity == null)
            {
                return NotFound();
            }
            membersEntity.FirstName = oMembersDto.FirstName;
            membersEntity.LastName = oMembersDto.LastName;
            membersEntity.Email = oMembersDto.Email;
            membersEntity.PhoneNumber = oMembersDto.PhoneNumber;
            await _membersRepository.UpdateAsync(membersEntity);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // DELETE api/<MembersController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var membersEntity = await _membersRepository.GetByIdAsync(id);
            if (membersEntity == null)
            {
                return NotFound();
            }
            await _membersRepository.DeleteAsync(membersEntity);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}
