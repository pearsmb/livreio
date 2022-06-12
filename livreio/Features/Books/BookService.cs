using AutoMapper;
using livreio.Features.Books;
using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;
using Google.Apis.Services;
using livreio.API;
using livreio.Data;
using livreio.Domain;
using livreio.Features.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace livreio.Features.Books;

public class BookService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _dbContext;
    private readonly IUserAccessor _userAccessor;
    private readonly BooksService _booksService;

    
    public BookService(IConfiguration configuration, IMapper mapper, ApplicationDbContext dbContext, IUserAccessor userAccessor)
    {
        _configuration = configuration;
        _mapper = mapper;
        _dbContext = dbContext;
        _userAccessor = userAccessor;


        // google books service
        _booksService = new BooksService(new BaseClientService.Initializer
        {
            ApplicationName = "livreio",
            ApiKey = _configuration.GetSection("Google:BooksApiKey").Value
        });
    }

    /// <summary>
    /// Fetches a list of books from the Google API.
    /// </summary>
    /// <param name="query">Book title to search by</param>
    /// <returns></returns>
    public async Task<List<BookDto>> SearchBooksByName(string query)
    {
        var request = _booksService.Volumes.List(query);

        // filtering the request to only fetch 4 books
        request.MaxResults = 4;

        var result = await request.ExecuteAsync();
        
        
        List<BookDto> books = new List<BookDto>();
        
        foreach (var book in result.Items)
        {
            books.Add(_mapper.Map<BookDto>(book));
            
        }
        
        return books;
    }


    /// <summary>
    /// Returns a BookDto by volumeId
    /// </summary>
    /// <param name="volumeId">the google books VolumeID</param>
    /// <returns>BookDto representing the volume</returns>
    public async Task<BookDto> SearchBookById(string volumeId)
    {
        
        var book = await _booksService.Volumes.Get(volumeId).ExecuteAsync();

        return _mapper.Map<BookDto>(book.VolumeInfo);
        
    }

    
    
    
    public async Task<BookDto> AddBookToFavourites(BookDto book)
    {

        var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
            x.UserName == _userAccessor.GetUserName());

        var fb = _mapper.Map<Book>(book);

        var favouriteBook = new AppUser_FavouriteBooks
        {
            BookId = fb.BookId,
            Book = fb,
            AppUserId = user.Id,
            AppUser = user
        };

        if (user.FavouriteBooks == null)
        {
            user.FavouriteBooks = new List<AppUser_FavouriteBooks>();
        }
        
        user.FavouriteBooks.Add(favouriteBook);

        _dbContext.Update(user);
        
        await _dbContext.SaveChangesAsync();
        
        return _mapper.Map<BookDto>(user.FavouriteBooks.Last().Book);

    }
    
    public async Task<List<Book>> GetFavouriteBooks()
    {
        
        var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
            x.UserName == _userAccessor.GetUserName());
        
        return _dbContext.FavouriteBooks
            .Where(x => x.AppUserId == user.Id)
            .Select(x => x.Book).ToList();

    }
    
    





}