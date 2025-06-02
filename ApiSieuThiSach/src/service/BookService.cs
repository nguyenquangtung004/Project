using ApiSieuThiSach.Config;
using ApiSieuThiSach.models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiSieuThiSach.sevice
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _booksCollection;
        private readonly AuthorService _authorService;
        private readonly ILogger<BookService> _logger;

        public BookService(IOptions<MongoDbSettings> mongoDbSettings, AuthorService authorService, ILogger<BookService> logger)
        {
            _logger = logger;
            _authorService = authorService;
            try
            {
                var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
                var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);

                _booksCollection = mongoDatabase.GetCollection<Book>(mongoDbSettings.Value.BooksCollectionName);
                _logger.LogInformation("Kết nối thành công giữa mongoDb và Bảng Books");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi kết nối giữa mongoDb và bảng books");
                throw;
            }
        }

        public async Task<Book?> CreateAsync(Book newBook)
        {
            if (newBook.AuthorMongoIds == null || !newBook.AuthorMongoIds.Any())
            {
                _logger.LogWarning("Không có ID tác giả nào được cung cấp cho sách : {Title}", newBook.Title);
                return null;
            }

            foreach (var item in newBook.AuthorMongoIds)
            {
                var authorExists = await _authorService.AuthorExistsAsync(item);
                if (!authorExists)
                {
                    _logger.LogWarning("Không tìm thấy tác giả với id : {AuthorId} cho sách :{Title}", item, newBook.Title);
                    return null;
                }

            }
            try
            {
                newBook.CreatedAt = DateTime.UtcNow;
                newBook.UpdatedAt = DateTime.UtcNow;

                await _booksCollection.InsertOneAsync(newBook);

                _logger.LogInformation("Đã tạo thành công với sách với id sách là :{BookId}", newBook.DisplayBookId);
                return newBook;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo sách mới với tiêu đề {Title}", newBook.Title);
                return null;
            }
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            try
            {
                _logger.LogInformation("Đang lấy danh sách bộ sách...");
                var books = await _booksCollection.Find(Builders<Book>.Filter.Empty).ToListAsync();
                return books;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách sách");
                return new List<Book>();
            }
        }


    }
}