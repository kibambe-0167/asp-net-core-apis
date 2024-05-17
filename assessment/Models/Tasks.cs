namespace assessment.Models
{
    public class Tasks
    {
        //[Required]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Assignee { get; set; }
        public DateTime DueDate { get; set; }

        public Tasks()
        {
            DueDate = DateTime.Now;
            ID = 0;
            Title = "";
            Description = "";
            Assignee = string.Empty;
        }
    }
}
