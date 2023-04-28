using System.ComponentModel.DataAnnotations;

namespace FreakyFashion_EF_Core.Models
{
    class Category
    {

        public Category(int id, string name)
        : this(name)
        {
            Id = id;
            
        }

        public Category(string name)
        {
            Name = name;
        }

        public int Id { get; protected set; }
        [MaxLength(50)]
        public string Name { get; protected set; }
        public IList<Product> Products { get; protected set; } = new List<Product>();
    }
}
