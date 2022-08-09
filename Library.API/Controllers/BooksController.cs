using AutoMapper;
using Library.API.DTOs;
using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

public class BooksController : BaseApiController
{
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Book> _bookRepo;

    public BooksController(IGenericRepository<Book> bookRepo, IMapper mapper, IConfiguration config)
    {
        _bookRepo = bookRepo;
        _mapper = mapper;
        _config = config;
    }

    [HttpGet("[controller]")]
    public async Task<ActionResult<IReadOnlyList<BookOverviewDTO>>> GetBooksByOrder(
        [FromQuery] BookSpecOrderParams specOrderParams)
    {
        var spec = new BooksByOrderSpec(specOrderParams);
        var books = await _bookRepo.ListAsync(spec);
        var bookDTOs = _mapper.Map<IReadOnlyList<Book?>, IReadOnlyList<BookOverviewDTO?>>(books);
        return Ok(bookDTOs);
    }

    [HttpGet("[controller]/{Id}")]
    public async Task<ActionResult<IReadOnlyList<BookOverviewDTO>>> GetBookById(int Id)
    {
        var spec = new BookSpec(Id);
        var book = await _bookRepo.GetEntityWithSpecificationAsync(spec);
        var bookDTO = _mapper.Map<Book?, BookDetailsDTO?>(book);
        return Ok(bookDTO);
    }

    [HttpGet("recommended")]
    public async Task<ActionResult<IReadOnlyList<BookOverviewDTO>>> GetBooksRecommended(
        [FromQuery] BookSpecGenreParams genreParams)
    {
        var spec = new BooksRecommendedByGenreSpec(genreParams);
        var books = await _bookRepo.ListAsync(spec);
        var bookDTOs = _mapper.Map<IReadOnlyList<Book?>, IReadOnlyList<BookOverviewDTO?>>(books);
        return Ok(bookDTOs);
    }

    [HttpDelete("[controller]/{Id:int}")]
    public async Task<ActionResult> DeleteBookById([FromQuery] string secret, int Id)
    {
        if (secret != _config["SecretKey"]) return Unauthorized();
        var book = await _bookRepo.GetByIdAsync(Id);
        if (book == null) return BadRequest();
        await _bookRepo.DeleteAsync(book);
        return Ok();
    }

    [HttpPost("[controller]/save")]
    public async Task<ActionResult<OnlyIdResponseDTO>> SaveBook(BookToInsertDTO bookToInsert)
    {
        var book = _mapper.Map<BookToInsertDTO, Book>(bookToInsert);
        Book response;
        if (bookToInsert.Id == null) response = await _bookRepo.InsertAsync(book);
        else response = await _bookRepo.UpdateAsync(book);
        return Ok(_mapper.Map<Book, OnlyIdResponseDTO>(response));
    }

 
}