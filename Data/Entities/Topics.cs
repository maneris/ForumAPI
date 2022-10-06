namespace ForumAPI.Data.Entities
{
    public class Topics
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime CreationDateTime    { get; set; }
    }
}
