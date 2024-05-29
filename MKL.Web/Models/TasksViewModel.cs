namespace MKL.Web.Models
{
    public class TasksViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public double Progress { get; set; }
        public Guid ProjectId { get; set; }
    }
}
