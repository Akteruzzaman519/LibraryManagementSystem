using Library.Models.Models;
using Library.Models.ViewModels;
using Library.Services.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BooksController : ControllerBase
{
    private readonly IRepository<Books> _booksRepository;
    public BooksController(IRepository<Books> booksRepository)
    {
        _booksRepository = booksRepository;
    }

    // GET: api/<BooksController>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var data = await _booksRepository.GetAllAsync();
            return Ok(data);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // GET api/<BooksController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var member = await _booksRepository.GetByIdAsync(id);
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

    // POST api/<BooksController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BooksDto oBooksDto)
    {
        try
        {
            var booksEntity = new Books()
            {
                AuthorID = oBooksDto.AuthorID,
                Title = oBooksDto.Title,
                ISBN = oBooksDto.ISBN,
                PublishedDate = Convert.ToDateTime(oBooksDto.PublishedDate),
                TotalCopies = oBooksDto.TotalCopies,
                AvailableCopies = oBooksDto.AvailableCopies,
            };
            var createMemberResponse = await _booksRepository.AddAsync(booksEntity);
            return Ok(createMemberResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // PUT api/<BooksController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] BooksDto oBooksDto)
    {
        try
        {
            var booksEntity = await _booksRepository.GetByIdAsync(id);
            if (booksEntity == null)
            {
                return NotFound();
            }
            booksEntity.AuthorID = oBooksDto.AuthorID;
            booksEntity.Title = oBooksDto.Title;
            booksEntity.ISBN = oBooksDto.ISBN;
            booksEntity.PublishedDate = Convert.ToDateTime(oBooksDto.PublishedDate);
            booksEntity.TotalCopies = oBooksDto.TotalCopies;
            booksEntity.AvailableCopies = oBooksDto.AvailableCopies;
            await _booksRepository.UpdateAsync(booksEntity);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // DELETE api/<BooksController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var booksEntity = await _booksRepository.GetByIdAsync(id);
            if (booksEntity == null)
            {
                return NotFound();
            }
            await _booksRepository.DeleteAsync(booksEntity);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}
