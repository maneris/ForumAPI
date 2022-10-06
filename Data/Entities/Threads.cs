namespace ForumAPI.Data.Entities
{
    public class Threads
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDateTime { get; set; }
        public Topics Topic { get; set; }
    }
}
