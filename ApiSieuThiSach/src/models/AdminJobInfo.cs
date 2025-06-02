public class AdminJobInfo
{
    public Guid Id { get; set; }

    public string Department { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
    public DateTime HiredDate { get; set; }

    // Liên kết ngược
    public Guid AdminId { get; set; }
    public virtual Admin Admin { get; set; }
}
