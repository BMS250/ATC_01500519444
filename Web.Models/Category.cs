using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookWeb.Models
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
