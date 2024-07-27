using BookWeb.DataAccess.Data;
using BookWeb.DataAccess.Repository.IRepository;
using BookWeb.Models;
using BookWeb.Models.ViewModels;
using BookWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public UserController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            List<ApplicationUser> users = _db.ApplicationUsers.Include(u => u.Company).ToList();
            foreach (var user in users)
            {
                if (user.Company == null)
                {
                    user.Company = new() { Name = "" };
                }
            }
            return View(users);
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ApplicationUser> users = _db.ApplicationUsers.Include(u => u.Company).ToList();
            var userRoles = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();
            foreach (var user in users)
            {
                var userId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == userId).Name;
            }
            return Json(new { data = users });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return Json(new { success = false, message = "Error While Locking/Unlocking" });
            }
            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now;
            }
            else
            {
                user.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _db.SaveChanges();
            return Json(new { success = true, message = "Role Managment successful" });
        }
        [HttpGet]
        public IActionResult RoleManagement(string userId)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Id == userId);
            UserVM userVM = new()
            {
                ApplicationUser = user,
                CompanyList = _db.Companies.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                RoleList = _db.Roles.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            return View(userVM);
        }
        [HttpPost]
        public IActionResult RoleManagement(UserVM userVM)
        {
            if (userVM.ApplicationUser == null)
            {
                return Json(new { success = false, message = "Error While Role Managment" });
            }
            var oldRole = _db.Roles.FirstOrDefault(r => r.Id == _db.UserRoles.FirstOrDefault(u => u.UserId == userVM.ApplicationUser.Id).RoleId);
            var newRole = _db.Roles.FirstOrDefault(u => u.Id == userVM.ApplicationUser.Role);
            ApplicationUser applicationUser = _db.ApplicationUsers.FirstOrDefault(u => u.Id == userVM.ApplicationUser.Id);
            // Change the role
            applicationUser.Role = newRole.Id;
            if (newRole.Name == SD.Role_Company)
            {
                applicationUser.CompanyId = userVM.ApplicationUser.CompanyId;
            }
            else
            {
                applicationUser.CompanyId = null;
            }
            _db.SaveChanges();
            _userManager.RemoveFromRoleAsync(applicationUser, oldRole.Name).GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(applicationUser, newRole.Name).GetAwaiter().GetResult();
            //}
            TempData["success"] = "Role Managment successful";
            return RedirectToAction("Index");
            //return Json(new { success = true, message = "Role Managment successful" });
        }

        #endregion
    }
}
