using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MasterCRUDOperation.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
    }
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }

        // Foreign key 
        [Display(Name = "Product")]
        public virtual int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Category Products { get; set; }   
    }

}
