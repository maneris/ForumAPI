using ForumAPI.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace ForumAPI.Data.Entities
{
    public class Threads : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDateTime { get; set; }
        public Topics? Topic { get; set; }
        public int? TopicId { get; set; }
        [Required]
        public string UserId { get; set; }

        public ForumRestUser User { get; set; }
    }
}
