using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication1_Razor.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        //[Required]
        [MinLength(3)]
        [MaxLength(25)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [Range(1, 50)]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
