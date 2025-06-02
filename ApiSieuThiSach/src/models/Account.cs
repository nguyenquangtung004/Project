using System;

namespace ApiSieuThiSach.Models
{
    public class Account
    {
        public Guid Id { get; set; }

        // ==== Thông tin xác thực ====
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }               
        public string PhoneNumber { get; set; }

        // ==== Trạng thái ====
        public bool IsActive { get; set; } = true;
        public bool IsEmailVerified { get; set; } = false;
        public bool IsPhoneVerified { get; set; } = false;

        // ==== Hệ thống tracking ====
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        // ==== Liên kết đến các bảng phụ ====
        public Guid? UserProfileId { get; set; }
        public Guid? AccountSecurityId { get; set; }
        public Guid? AccountSessionId { get; set; }

        public virtual UserProfile UserProfile { get; set; }
        public virtual AccountSecurity AccountSecurity { get; set; }
        public virtual AccountSession AccountSession { get; set; }
    }
}
