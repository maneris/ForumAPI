using ForumAPI.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace ForumAPI.Data.Entities
{
    public class Posts : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public Threads? Thread { get; set; }
        public int? ThreadsId { get; set; }
        [Required]
        public string UserId { get; set; }

        public ForumRestUser User { get; set; }
    }
}
