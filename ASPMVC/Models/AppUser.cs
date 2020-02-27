using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMS_ASP_MVC.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string FristName { get; set; }
        public string LastName { get; set; }
        public int companyID { get; set; }
    }
}
