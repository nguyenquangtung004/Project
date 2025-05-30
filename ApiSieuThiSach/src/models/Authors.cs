using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ApiSieuThiSach.models 
{
    public class Author 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _idAuthors { get; set; } 

        [BsonElement("DisplayAuthorId")] 
        [Required(ErrorMessage = "Mã hiển thị của tác giả không được bỏ trống")]
        [StringLength(50, ErrorMessage = "Mã hiển thị tác giả không quá 50 ký tự.")]
        public string DisplayAuthorId { get; set; } = null!; 

        [BsonElement("FullName")]
        [Required(ErrorMessage = "Họ và tên tác giả là bắt buộc.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Họ và tên phải từ 2 đến 150 ký tự.")]
        public string FullName { get; set; } = null!;

        [BsonElement("PenName")]
        [StringLength(150, ErrorMessage = "Bút danh không quá 150 ký tự.")]
        public string? PenName { get; set; }

        [BsonElement("DateOfBirth")]
        public DateTime? DateOfBirth { get; set; }

        [BsonElement("DateOfDeath")]
        public DateTime? DateOfDeath { get; set; }

        [BsonElement("Nationality")]
        [StringLength(100, ErrorMessage = "Quốc tịch không quá 100 ký tự.")]
        public string? Nationality { get; set; }

        [BsonElement("Biography")]
        [StringLength(5000, ErrorMessage = "Tiểu sử không quá 5000 ký tự.")]
        public string? Biography { get; set; }

        [BsonElement("ProfileImageUrl")]
        [Url(ErrorMessage = "URL ảnh đại diện không hợp lệ.")]
        public string? ProfileImageUrl { get; set; }

        [BsonElement("Website")]
        [Url(ErrorMessage = "URL trang web cá nhân không hợp lệ.")]
        public string? Website { get; set; }

        [BsonElement("SocialMediaLinks")]
        public List<SocialMediaLink>? SocialMediaLinks { get; set; }

        [BsonElement("NotableWorks")]
        public List<string>? NotableWorks { get; set; }

        [BsonElement("Genres")]
        public List<string>? Genres { get; set; }

        [BsonElement("Awards")]
        public List<Award>? Awards { get; set; }

        [BsonIgnoreIfNull]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonIgnoreIfNull]
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    
    public class SocialMediaLink
    {
        public string PlatformName { get; set; } = null!;

        [Url(ErrorMessage = "URL liên kết không hợp lệ.")]
        public string Url { get; set; } = null!;
    }

    public class Award
    {
        public string AwardName { get; set; } = null!;
        public int? YearReceived { get; set; }
        public string? AwardingBody { get; set; }
    }
}