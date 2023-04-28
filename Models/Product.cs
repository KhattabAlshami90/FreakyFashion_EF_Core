using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace FreakyFashion_EF_Core.Models
{
    [Index(nameof(Number), IsUnique = true)]
    class Product
    {
        public Product(int id, string number, string name, string? description, decimal price)
        : this(number, name, description, price)
        {
            Id = id;
        }



        public Product(string number, string name, string? description, decimal price)
        {
            Number = number;
            Name = name;
            Description = description;
            Price = price;
        }

        public int Id { get; set; }
        [MaxLength(50)]
        public string Number { get;  set; }
        [MaxLength(50)]
        public string Name { get;  set; }

        public string? Description { get;  set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get;  set; }
        public IList<Category> Categories { get; protected set; } = new List<Category>(); 
    }
}
