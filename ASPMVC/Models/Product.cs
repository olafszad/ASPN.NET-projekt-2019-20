using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WMS_ASP_MVC.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string warehouseName { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        
        [Required]
        public int Quantity { get; set; }
      
    }
}
