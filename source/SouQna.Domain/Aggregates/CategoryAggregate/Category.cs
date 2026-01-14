using SouQna.Domain.Common;

namespace SouQna.Domain.Aggregates.CategoryAggregate
{
    public class Category
    {
        private readonly List<Category> _subcategories = [];

        public Guid Id { get; private set; }
        public Guid? ParentId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Category? Parent { get; private set; }
        public IReadOnlyCollection<Category> Subcategories => _subcategories.AsReadOnly();

        private Category()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

        private Category(
            Guid id,
            Guid? parentId,
            string name,
            string description,
            DateTime createdAt
        )
        {
            Id = id;
            ParentId = parentId;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
        }

        public static Category Create(Guid? parentId, string name, string description)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNullOrEmpty(description, nameof(description));

            return new Category(
                Guid.NewGuid(),
                parentId,
                name,
                description,
                DateTime.UtcNow
            );
        }

        public void Update(string name, string description)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNullOrEmpty(description, nameof(description));

            Name = name;
            Description = description;
        }

        public bool HasSubcategories() => _subcategories.Count != 0;
    }
}