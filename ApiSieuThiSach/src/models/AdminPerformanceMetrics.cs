namespace ApiSieuThiSach.models
{
    public class AdminPerformanceMetrics
    {
        public Guid Id { get; set; }

        public int TotalOrdersManaged { get; set; }
        public int TotalCustomersManaged { get; set; }
        public decimal TotalRevenueManaged { get; set; }

        public Guid AdminId { get; set; }
        public virtual Admin Admin { get; set; }
    }    
}

