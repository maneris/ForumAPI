
namespace ForumAPI.Data
{
        public record TopicDto(int Id, string Title, string Description, DateTime CreationDate);
        public record CreateTopicDto(string Title, string Description);
        public record UpdateTopicDto(string Description);
        public record ThreadDto(int Id, string Title, string Description, DateTime CreationDate);
        public record CreateThreadDto(string Title, string Description);
        public record UpdateThreadDto(string Description);
        public record PostDto(int Id, string Description, DateTime CreationDate);
        public record CreatePostDto( string Description);
        public record UpdatePostDto(string Description);
    }
