using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Google.Apis.Services;
using Microsoft.AspNetCore.Authorization;

namespace livreio.Features.Books;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    
    private readonly ILogger<BookController> _logger;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public BookController(ILogger<BookController> logger, IMapper mapper, IConfiguration configuration)
    {
        _logger = logger;
        _mapper = mapper;
        _configuration = configuration;
    }
    
    [Authorize]
    [HttpGet("{query}" ,Name = "SearchBooksByTitle")]
    public async Task<ActionResult<List<BookDto>>> Get(string query)
    {
        
        var service = new BooksService(new BaseClientService.Initializer
        {
            ApplicationName = "livreio",
            ApiKey = _configuration.GetSection("Google:BooksApiKey").Value
        });

        var result = await service.Volumes.List(query).ExecuteAsync();

        List<BookDto> books = new List<BookDto>();
        
        foreach (var book in result.Items)
        {
            books.Add(_mapper.Map<BookDto>(book.VolumeInfo));
        }
        
        return books;
        
    }
}