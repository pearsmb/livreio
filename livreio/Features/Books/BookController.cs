using System.Security.Claims;
using AutoMapper;
using livreio.API;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Google.Apis.Services;
using livreio.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace livreio.Features.Books;

[ApiController]
[Route("api/")]
public class BookController : ControllerBase
{
    
    private readonly ILogger<BookController> _logger;

    private readonly UserManager<AppUser> _userManager;
    private readonly BookService _bookService;

    public BookController(ILogger<BookController> logger, UserManager<AppUser> userManager, BookService bookService)
    {
        _logger = logger;
        _userManager = userManager;
        _bookService = bookService;
    }
    
    [HttpGet("[controller]/{query}" ,Name = "searchbooksbytitle")]
    public async Task<ActionResult<List<BookDto>>> Get(string query)
    {
        
        var books = await _bookService.SearchBooksByName(query);

        return books.Any() ? Ok(books) : BadRequest("No books matched the search query.");
        
    }

    
    
    [HttpPost("User/{userName}/favouritebooks")]
    [HttpPost("[controller]/favouritebooks")]
    public async Task<ActionResult<BookDto>> AddToFavourites(BookDto book)
    {
        return await _bookService.AddBookToFavourites(book);
    }
    
    [HttpGet("User/{userName}/favouritebooks")]
    [HttpGet("[controller]/favouritebooks")]
    public async Task<ActionResult<List<Book>>> GetFavouriteBooks()
    {
        return await _bookService.GetFavouriteBooks();

    }
    
    

}