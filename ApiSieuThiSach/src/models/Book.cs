using MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ApiSieuThiSach.models
{
    public class Book
    {
        [BsonId] //IMPORTANT: Đánh dấu đây là id mongoDb
        [BsonRepresentation(BsonType.ObjectId)] //Lưu trữ dưới dạng ObjectId trong db, nhưng là string trong C#
        public string? id { get; set; }

        [BsonElement("BookId")]
        [Required(ErrorMessage ="Số id thứ tự sách là bắt buộc")]
        public string? idBook { get; set;}
        
        [BsonElement("Title")]
        [Required(ErrorMessage = "Tiêu đề sách là bắt buộc. ")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Tiêu đề từ 1 đến 200 kí tự")]
        public string Title { get; set; } = null; //Null để tránh warning nullable , sẽ được validate bởi required

        [BsonElement("Subtitle")]
        [StringLength(300, ErrorMessage = "Phụ đề không quá 300 kí tự")]
        public string Subtitle { get; set; }

        [Required(ErrorMessage = "Tác giả sách là bắt buộc")]
        public List<String> Authors { get; set; } = new List<string>();

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

        [BsonElement("Format")] // Ví dụ: "Bìa cứng", "Bìa mềm", "Ebook"
        public string? Format { get; set; }

        [BsonElement("Price")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0.")]
        public decimal? Price { get; set; }

        [BsonElement("CurrencyCode")] // Ví dụ: "VND", "USD"
        public string? CurrencyCode { get; set; }

        [BsonElement("Availability")] // Ví dụ: "Còn hàng", "Hết hàng", "Đặt trước"
        public string? Availability { get; set; }

        [BsonElement("StockQuantity")]
        public int? StockQuantity { get; set; }

        [BsonElement("Dimensions")] // Kích thước vật lý
        public BookDimensions? Dimensions { get; set; }
        
         [BsonElement("Weight")] // Cân nặng (ví dụ: tính bằng gram)
        public double? Weight { get; set; }

        [BsonElement("Edition")]
        public string? Edition { get; set; } // Lần tái bản

        [BsonElement("Series")]
        public BookSeriesInfo? Series { get; set; } // Thông tin bộ sách nếu có

        [BsonIgnoreIfNull] // Không lưu vào DB nếu giá trị là null
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonIgnoreIfNull]
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public class BookSeriesInfo
    {
        public double? Height { get; set; } // cm
        public double? Width { get; set; }  // cm
        public double? Thickness { get; set; } // cm
    }

    public class BookDimensions
    {
        public string Name { get; set; } = null!;
        public int? VolumeNumber { get; set; }
    }
}