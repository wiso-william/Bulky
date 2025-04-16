using System.ComponentModel;
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
        [DisplayName("Category Name")]
        [MaxLength(30, ErrorMessage ="The name can't be more than 30 chars")] //Validator
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100, ErrorMessage = "Numero da 1 a 100")] // Validator
        public int DisplayOrder { get; set; }
    }
}
