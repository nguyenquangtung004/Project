using System;
using System.Collections.Generic;

namespace ApiSieuThiSach.Models
{
    public class Admin
    {
        public Guid Id { get; set; } // _idAdmin

        // Liên kết tài khoản hệ thống
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }

        // Phân quyền
        public string Role { get; set; }
        public int PermissionLevel { get; set; }
        public List<string> Permissions { get; set; } = new();

        // Quản lý nội bộ
        public Guid? ManagerAdminId { get; set; }
        public List<Guid> SubordinateAdminIds { get; set; } = new();

        // Ghi chú / mô tả
        public string Notes { get; set; }
        public string Description { get; set; }

        // Tracking hệ thống
        public DateTime AdminProfileCreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime AdminProfileUpdatedAt { get; set; } = DateTime.UtcNow;
        public string AdminProfileUpdatedBy { get; set; }

        // Liên kết tới các bảng phụ
        public Guid? AdminJobInfoId { get; set; }
        public Guid? AdminBranchAssignmentId { get; set; }
        public Guid? AdminPerformanceMetricsId { get; set; }

        public virtual AdminJobInfo JobInfo { get; set; }
        public virtual AdminBranchAssignment BranchAssignment { get; set; }
        public virtual AdminPerformanceMetrics PerformanceMetrics { get; set; }
    }
}
