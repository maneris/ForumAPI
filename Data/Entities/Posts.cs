namespace ForumAPI.Data.Entities
{
    public class Posts
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public Threads Thread { get; set; }
    }
}
