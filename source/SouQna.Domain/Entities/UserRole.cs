namespace SouQna.Domain.Entities
{
    public class UserRole
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid RoleId { get; private set; }

        public User User { get; private set; }
        public Role Role { get; private set; }

        private UserRole()
        {
            User = null!;
            Role = null!;
        }

        private UserRole(Guid id, Guid userId, Guid roleId)
        {
            Id = id;
            UserId = userId;
            RoleId = roleId;
            User = null!;
            Role = null!;
        }
    }
}