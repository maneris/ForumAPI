namespace ForumAPI.Auth.Model
{
    public static class ForumRoles
    {
        public const string Admin = nameof(Admin);
        public const string AuthForumUser = nameof(AuthForumUser);
        public const string AnonGuest = nameof(AnonGuest);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, AuthForumUser, AnonGuest };
        public static readonly IReadOnlyCollection<string> RegisteredUsers = new[] {  AuthForumUser, AnonGuest };
        public static readonly IReadOnlyCollection<string> AnonUsers = new[] {  AnonGuest };
    }
}
