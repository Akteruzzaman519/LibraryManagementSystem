
using Library.Models.Models;
using Library.Models.ViewModels;
using Library.Services.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Api.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
public class AuthorsController : ControllerBase
{
    private readonly IRepository<Authors> _authorsRepository;
    public AuthorsController(IRepository<Authors> authorsRepository)
    {
        _authorsRepository = authorsRepository;
    }
    // GET: api/<AuthorsController>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var data = await _authorsRepository.GetAllAsync();
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
            var member = await _authorsRepository.GetByIdAsync(id);
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
    public async Task<IActionResult> Post([FromBody] AuthorsDto oAuthorsDto)
    {
        try
        {
            var authorsEntity = new Authors()
            {
                AuthorName = oAuthorsDto.AuthorName,
                AuthorBio = oAuthorsDto.AuthorBio,
            };
            var createMemberResponse = await _authorsRepository.AddAsync(authorsEntity);
            return Ok(createMemberResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // PUT api/<AuthorsController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] AuthorsDto oAuthorsDto)
    {
        try
        {
            var authorsEntity = await _authorsRepository.GetByIdAsync(id);
            if (authorsEntity == null)
            {
                return NotFound();
            }
            authorsEntity.AuthorName = oAuthorsDto.AuthorName;
            authorsEntity.AuthorBio = oAuthorsDto.AuthorBio;
            await _authorsRepository.UpdateAsync(authorsEntity);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // DELETE api/<AuthorsController>/5
    [HttpDelete("{id}"), Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var authorsEntity = await _authorsRepository.GetByIdAsync(id);
            if (authorsEntity == null)
            {
                return NotFound();
            }
            await _authorsRepository.DeleteAsync(authorsEntity);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }


}
