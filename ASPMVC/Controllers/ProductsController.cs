using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMS_ASP_MVC.Models;

namespace WMS_ASP_MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public static string a = string.Empty;



        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Product Product { get; set; }


        public IActionResult Index(string warehouseName)
        {
            

            a = warehouseName;
            ViewBag.message = a;
            return View();

        }

        public IActionResult Create(int? id)
        {
            Product = new Product();
            return View(Product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(Product);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var sql = $"SELECT * FROM dbo.Products WHERE warehouseName = '{a}'";
            return Json(new { data = await _db.Products.FromSqlRaw(sql).ToListAsync() });

        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var productFromDb = await _db.Products.FirstOrDefaultAsync(u => u.id == id);
            if (productFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _db.Products.Remove(productFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete succesful" });



        }
        public IActionResult Update(int id)
        {
            Product = new Product();
            Product = _db.Products.FirstOrDefault(u => u.id == id);
            if (Product == null)
            {
                return NotFound();
            }
            return View(Product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update()
        {
            if (ModelState.IsValid)
            {
                _db.Products.Update(Product);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}