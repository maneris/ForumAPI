namespace ForumAPI.Data.Entities
{
    public class Posts
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int ThreadId { get; set; }   
        //public Thread Thread { get; set; }
        public int TopicId { get; set; }
        //public Topics Topic { get; set; }
    }
}
