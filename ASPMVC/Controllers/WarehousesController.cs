using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMS_ASP_MVC.Models;
using System.Data.SqlClient;

namespace WMS_ASP_MVC.Controllers
{
    public class WarehousesController : Controller
    {
 
        private readonly ApplicationDbContext _db;
        private readonly UserManager<AppUser> _mg;
      

        public WarehousesController(ApplicationDbContext db, UserManager<AppUser> mg)
        {
            _db = db;
            _mg = mg;
        }
        [BindProperty]
        public Warehouse Warehouse { get; set; }

        public IActionResult Index()
        {
           
            return View();
            
        }

        public IActionResult Create(int? id)
        {
            Warehouse = new Warehouse();
            return View(Warehouse);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            if (ModelState.IsValid)
            {
                _db.Warehouses.Add(Warehouse);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
            
        }

        public IActionResult Update(int id)
        {
            Warehouse = new Warehouse();
            Warehouse = _db.Warehouses.FirstOrDefault(u => u.id == id);
            if (Warehouse == null)
            {
                return NotFound();
            }
            return View(Warehouse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update()
        {
            if (ModelState.IsValid)
            {
                _db.Warehouses.Update(Warehouse);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region API Calls

  

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userid = _mg.GetUserId(HttpContext.User);
            AppUser user = _mg.FindByIdAsync(userid).Result;
            var userInRole = await _mg.IsInRoleAsync(user, "Admin");
            if(userInRole == true)
            {
                return Json(new { data = await _db.Warehouses.ToListAsync() });
            }
            else
            {
                var sql = $"SELECT * FROM dbo.Warehouses WHERE companyID = {user.companyID}";
                return Json(new { data = await _db.Warehouses.FromSqlRaw(sql).ToListAsync() });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var warehouseFromDb = await _db.Warehouses.FirstOrDefaultAsync(u => u.id == id);
            if (warehouseFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _db.Warehouses.Remove(warehouseFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete succesful" });

        }
        #endregion
    }
}