using MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ApiSieuThiSach.models
{
    // [BsonIgnoreExtraElements]
    public class Book
    {
        [BsonId] 
        [BsonRepresentation(BsonType.ObjectId)] 
        public string? _idBooks { get; set; }

        [BsonElement("DisplayBookId")]
        [Required(ErrorMessage = "Mã hiển thị sách là bắt buộc")]
        [StringLength(50, ErrorMessage = "Mã hiển thị sách không quá 50 ký tự.")]
        public string DisplayBookId { get; set; } = null!;

        [BsonElement("Title")]
        [Required(ErrorMessage = "Tiêu đề sách là bắt buộc. ")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Tiêu đề từ 1 đến 200 kí tự")]
        public string Title { get; set; } = null;

        [BsonElement("Subtitle")]
        [StringLength(300, ErrorMessage = "Phụ đề không quá 300 kí tự")]
        public string Subtitle { get; set; }

        [BsonElement("AuthorMongoIds")] 
        [Required(ErrorMessage = "Ít nhất một ID tác giả là bắt buộc.")]
        public List<string> AuthorMongoIds { get; set; } = new List<string>();

        [BsonElement("Publisher")]
        public String? Publisher { get; set; }

        [BsonElement("PublisherDate")]
        public DateTime? PublisherDate { get; set; }

        [BsonElement("Description")]
        [Required(ErrorMessage = "Mô tả là bắt buộc")]
        public String? Description { get; set; }

        [BsonElement("ISBN10")]
        [RegularExpression(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$", ErrorMessage = "ISBN-10 không hợp lệ.")]
        public String? ISBN10 { get; set; }

        [BsonElement("ISBN13")]
        [Required(ErrorMessage = "ISBN 13 là bắt buộc")]
        [RegularExpression(@"^(?=(?:\D*\d){13}$)(?:97[89][-\s]?)?(?:[\d]{1,5}[-\s]?){2}[\d]{1,7}[-\s]?[\dX]$", ErrorMessage = "ISBN-13 không hợp lệ.")]
        public string ISBN13 { get; set; } = null!;

        [BsonElement("PageCount")]
        [Range(1, int.MaxValue, ErrorMessage = "Số trang phải lớn hơn 0")]
        public int? PageCount { get; set; }

        [BsonElement("Categories")]
        public List<string> Categories { get; set; } = new List<string>();

        [BsonElement("AverageRating")]
        [Range(0, 5, ErrorMessage = "Đánh giá từ 0 đến 5")]
        public double? AverageRating { get; set; }

        [BsonElement("RatingsCount")]
        public int? RatingsCount { get; set; }

        [BsonElement("ThumbnailUrl")]
        [Url(ErrorMessage = "URL ảnh bìa không hợp lệ.")]
        public string? ThumbnailUrl { get; set; }

        [BsonElement("Language")]
        [Required(ErrorMessage = "Ngôn ngữ là bắt buộc.")]
        public string Language { get; set; } = null!;

        [BsonElement("Format")] 
        public string? Format { get; set; }

        [BsonElement("Price")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0.")]
        public decimal? Price { get; set; }

        [BsonElement("CurrencyCode")] 
        public string? CurrencyCode { get; set; }

        [BsonElement("Availability")]
        public string? Availability { get; set; }

        [BsonElement("StockQuantity")]
        public int? StockQuantity { get; set; }

        [BsonElement("Dimensions")] 
        public BookDimensions? Dimensions { get; set; }

        [BsonElement("Weight")] 
        public double? Weight { get; set; }

        [BsonElement("Edition")]
        public string? Edition { get; set; } 

        [BsonElement("Series")]
        public BookSeriesInfo? Series { get; set; } 

        [BsonIgnoreIfNull] 
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonIgnoreIfNull]
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public class BookSeriesInfo
    {
        public double? Height { get; set; } 
        public double? Width { get; set; }  
        public double? Thickness { get; set; } 
    }

    public class BookDimensions
    {
        public string Name { get; set; } = null!;
        public int? VolumeNumber { get; set; }
    }
    
    public class EmbeddedAuthorInfo
    {
        [BsonRepresentation(BsonType.ObjectId)] 
        public string id { get; set; } = null!;

        public string authorsId { get; set; } = null!;
        public string FullName { get; set; } = null!; 
    }
}