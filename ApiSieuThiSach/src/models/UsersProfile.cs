using System;
namespace ApiSieuThiSach.Models
{
    public class UsersProfile
{
    public string? _idUsersProfile { get; set; }
    public string DisplayUserProfileId { get; set; } = null!;
    //Thông tin cá nhân
    public string FullName { get; set; } = null!;
    public string AvatarUrl { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public DateTime? DateOfBirth { get; set; }
    //Cài đặt hệ thống
    public string PreferredLanguage { get; set; } = null!;// Mã ISO: "en", "vi", v.v.
    public string TimeZone { get; set; } = null!;// Ví dụ: "Asia/Ho_Chi_Minh"
                                                 //Cài đặt cá nhân hoá
    public string PersonalSettings { get; set; } // JSON string hoặc object riêng nếu cần
                                                 // Liên kết với tài khoản
    public Guid AccountId { get; set; }
    public virtual Account Account { get; set; } // Navigation property
                                                 // Tùy chọn: Ngày tạo, cập nhật
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}
}