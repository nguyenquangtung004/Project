using ApiSieuThiSach.Config;
using ApiSieuThiSach.models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiSieuThiSach.sevice
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _booksCollection;
        private readonly ILogger<BookService> _logger;

        public BookService(IOptions<MongoDbSettings> mongoDbSettings, ILogger<BookService> logger)
        {
            _logger = logger;
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
    }    
}