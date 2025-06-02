using System;

namespace ApiSieuThiSach.Models
{
    public class AccountSession
    {
        public Guid Id { get; set; }

        // ==== Trạng thái phiên ====
        public bool IsOnline { get; set; } = false;
        public DateTime? LastLoginTime { get; set; }
        public DateTime? LastLogoutTime { get; set; }
        public string LastLoginIpAddress { get; set; }
        public string LastLoginDevice { get; set; }

        public DateTime? CurrentSessionStart { get; set; }
        public string CurrentSessionToken { get; set; }

        // ==== Thống kê hoạt động ====
        public int LoginCount { get; set; } = 0;
        public TimeSpan TotalWorkTime { get; set; } = TimeSpan.Zero;
        public DateTime? LastActivityTime { get; set; }

        // ==== Token ====
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }

        // ==== Liên kết với tài khoản ====
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
