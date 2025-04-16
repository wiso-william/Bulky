using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class CategoryModel
    {
        // You can add [Key] to define something as the primary key
        // ASP infers that "Id" is the primary key
        // CategoryModelId would aslo be automatically infered to be the id
        public int Id { get; set; }
        [Required] // Not null SQL
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}
