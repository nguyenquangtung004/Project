namespace ApiSieuThiSach.models
{
    public class AdminBranchAssignment
    {
        public Guid Id { get; set; }

        public string BranchCode { get; set; }
        public string BranchName { get; set; }

        public Guid AdminId { get; set; }
        public virtual Admin Admin { get; set; }
    }

}
