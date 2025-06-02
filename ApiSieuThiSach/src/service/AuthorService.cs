using ApiSieuThiSach.Config;
using ApiSieuThiSach.models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ApiSieuThiSach.sevice
{
    public class AuthorService
    {
        private readonly IMongoCollection<Author> _authorsCollection;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(IOptions<MongoDbSettings> mongoDbSetting, ILogger<AuthorService> logger)
        {
            _logger = logger;
            try
            {
                var mongoClient = new MongoClient(mongoDbSetting.Value.ConnectionString);
                var mongoDatabase = mongoClient.GetDatabase(mongoDbSetting.Value.DatabaseName);
                _authorsCollection = mongoDatabase.GetCollection<Author>(mongoDbSetting.Value.AuthorsCollectionName);
                _logger.LogInformation("AuthService: Kết nối thành công đến collection Authors");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi kết nối với mongoDb hoặc lấy collection");
                throw;
            }

        }

        public async Task<bool> AuthorExistsAsync(string AuthorMongoIds)
        {
            if (string.IsNullOrEmpty(AuthorMongoIds))
            {
                _logger.LogWarning("AuthorExistsAsync: authorMongoids là null hoặc rỗng");
                return false;
            }

            if (!ObjectId.TryParse(AuthorMongoIds, out _))
            {
                _logger.LogWarning("AuthorExistsAsync: ID tác giả không hợp lệ (không phải ObjectId): {AuthorId}", AuthorMongoIds);
                return false;
            }

            try
            {
                // Giả sử model Author của bạn có thuộc tính _idAuthors được đánh dấu [BsonId]
                var filter = Builders<Author>.Filter.Eq(a => a._idAuthors, AuthorMongoIds);
                var count = await _authorsCollection.CountDocumentsAsync(filter);
                return count > 0; // Trả về true nếu tìm thấy ít nhất 1 document, ngược lại false
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AuthorExistsAsync: Lỗi khi kiểm tra sự tồn tại của tác giả với ID: {AuthorId}", AuthorMongoIds);
                return false;
            }
        }

        public async Task<Author?> CreateAuthorAsync(Author newAuthor)
        {

            try
            {
                newAuthor.CreatedAt = DateTime.UtcNow;
                newAuthor.UpdatedAt = DateTime.UtcNow;

                await _authorsCollection.InsertOneAsync(newAuthor);

                _logger.LogInformation("Đã tạo thành công tác giả  với DisplayAuthorId:{DisplayAuthorId}", newAuthor.DisplayAuthorId, newAuthor._idAuthors);
                return newAuthor;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi ghi MongoDB khi tạo tác giả mới với DisplayAuthorId {DisplayAuthorId}: {ErrorMessage}", newAuthor.DisplayAuthorId, ex.Message);
                return null;
            }

        }

        public async Task<List<Author>> GetAllAuthorAsync()
        {
            try
            {
                _logger.LogInformation("Đang lấy danh sách tác giả của bộ sách...");
                var authors = await _authorsCollection.Find(Builders<Author>.Filter.Empty).ToListAsync();
                return authors;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách tác giả");
                return new List<Author>();
            }
        }
    }
}