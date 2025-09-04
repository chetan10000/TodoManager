namespace TodoManager.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Deadline { get; set;}
        public decimal Percentage { get; set; } = 0;
        public bool IsDone { get; set; } = false;
    }

    public class TodoDTO
    {
       
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Deadline { get; set; }
      
    }
}
