using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookWeb.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required] 
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        [Required] 
        public string ISBN { get; set; }
        [Display(Name = "Price List")]
        [Range(1, 1000)]
        [Required]
        public double PriceList { get; set; }
        [Display(Name = "Price for 1-50")]
        [Range(1, 1000)]
        [Required]
        public double Price { get; set; }
        [Display(Name = "Price for 51-100")]
        [Range(1, 1000)]
        [Required]
        public double Price50 { get; set; }
        [Display(Name = "Price for 100+")]
        [Range(1, 1000)]
        [Required]
        public double Price100 { get; set; }

        //[Display(Name = "Category ID")]
        public int CId { get; set; }
        [ForeignKey("CId")]
        [ValidateNever]
        public Category Category { get; set; }

        [ValidateNever]
        public List<ProductImage> ProductImages { get; set; }
    }
}
