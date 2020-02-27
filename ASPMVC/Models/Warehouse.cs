using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WMS_ASP_MVC.Models
{
    public class Warehouse
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string warehouseName { get; set; }
        [Required]
        public int companyID { get; set; }

    }
}
