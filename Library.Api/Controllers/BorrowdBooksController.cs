using Library.Models.Models;
using Library.Models.ViewModels;
using Library.Services.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Api.Controllers;

[Route("api/[controller]")]
[ApiController,Authorize]
public class BorrowdBooksController : ControllerBase
{
    private readonly IRepository<Books> _booksRepository;
    private readonly IRepository<BorrowdBooks> _borrowdBooksRepository;
    public BorrowdBooksController(IRepository<BorrowdBooks> borrowdBooksRepository, IRepository<Books> booksRepository)
    {
        _borrowdBooksRepository = borrowdBooksRepository;
        _booksRepository = booksRepository;
    }

    // GET: api/<BorrowdBooksController>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var data = await _borrowdBooksRepository.GetAllAsync();
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
            var member = await _borrowdBooksRepository.GetByIdAsync(id);
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
    public async Task<IActionResult> Post([FromBody] BorrowdBooksDto oBorrowdBooksDto)
    {
        try
        {
            var book = await _booksRepository.GetByIdAsync(oBorrowdBooksDto.BookID);
            if (book == null)
            {
                throw new Exception("Book not Found");
            }
            else
            {
                book.AvailableCopies = book.AvailableCopies - 1;
                await _booksRepository.UpdateAsync(book);
            }

            var borrowdBooksEntity = new BorrowdBooks()
            {
                BookID = oBorrowdBooksDto.BookID,
                MemberID = oBorrowdBooksDto.MemberID,
                BorrowDate= Convert.ToDateTime(DateTime.Now),
                ReturnDate = Convert.ToDateTime(oBorrowdBooksDto.ReturnDate),
                Status = oBorrowdBooksDto.Status,
            };
            var createMemberResponse = await _borrowdBooksRepository.AddAsync(borrowdBooksEntity);
            return Ok(createMemberResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // PUT api/<BooksController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] BorrowdBooksDto oBorrowdBooksDto)
    {
        try
        {
            var borrowdBooksEntity = await _borrowdBooksRepository.GetByIdAsync(id);
            if (borrowdBooksEntity == null)
            {
                return NotFound();
            }
            borrowdBooksEntity.BookID = oBorrowdBooksDto.BookID;
            borrowdBooksEntity.MemberID = oBorrowdBooksDto.MemberID;
            borrowdBooksEntity.ReturnDate = Convert.ToDateTime(oBorrowdBooksDto.ReturnDate);
            borrowdBooksEntity.Status = oBorrowdBooksDto.Status;
            await _borrowdBooksRepository.UpdateAsync(borrowdBooksEntity);

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
            var borrowdBooksEntity = await _borrowdBooksRepository.GetByIdAsync(id);
            if (borrowdBooksEntity == null)
            {
                return NotFound();
            }
            await _borrowdBooksRepository.DeleteAsync(borrowdBooksEntity);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}
