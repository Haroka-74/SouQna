namespace SouQna.Domain.Entities
{
    public class Role
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        private readonly List<UserRole> _userRoles = [];
        public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

        private Role()
        {
            Name = string.Empty;
        }

        private Role(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}