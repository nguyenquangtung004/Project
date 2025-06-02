using System;

namespace ApiSieuThiSach.Models
{
    public class AccountSecurity
    {
        public Guid Id { get; set; }

        // ==== Bảo mật tài khoản ====
        public int FailedLoginAttempts { get; set; } = 0;
        public DateTime? AccountLockedUntil { get; set; }
        public bool IsTwoFactorEnabled { get; set; } = false;
        public string TwoFactorSecret { get; set; }

        // ==== Liên kết với tài khoản ====
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
