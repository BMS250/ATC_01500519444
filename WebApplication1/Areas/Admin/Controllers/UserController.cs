using BookWeb.DataAccess.Data;
using BookWeb.DataAccess.Repository.IRepository;
using BookWeb.Models;
using BookWeb.Models.ViewModels;
using BookWeb.Utility;
using Microsoft.AspNetCore.Authorization;
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
        public UserController(ApplicationDbContext db)
        {
            _db = db;
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
                Name = user.Name,
                ApplicationUserId = userId,
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
            var currentRole = _db.Roles.FirstOrDefault(u => u.Name == userVM.ApplicationUser.Role);
            if (userVM.ApplicationUser.Role != null)
            {
                if (userVM.ApplicationUser.Role == SD.Role_Customer)
                {
                    _db.UserRoles.FirstOrDefault(u => u.UserId == userVM.ApplicationUserId).RoleId = currentRole.Id; // 103
                }
                _db.UserRoles.FirstOrDefault(u => u.UserId == userVM.ApplicationUserId).RoleId = currentRole.Id; // 105
            }
            _db.UserRoles.FirstOrDefault(u => u.UserId == userVM.ApplicationUserId).RoleId = currentRole.Id; // 107
            //if (userVM.ApplicationUser.Role == SD.Role_Company)
            //{
            //    _db.Users.FirstOrDefault(u => u.Id == userVM.ApplicationUserId).
            //    userVM.ApplicationUser.CompanyId = _db.Companies.FirstOrDefault(u => u.Name == userVM.ApplicationUser.Company.Name).Id;
            //}
            //else
            //{
            //    _db.Users.FirstOrDefault(u => u.Id == userVM.ApplicationUserId).CompanyId
            //    userVM.ApplicationUser.CompanyId = null;
            //}


            _db.SaveChanges();
            return Json(new { success = true, message = "Role Managment successful" });
        }

        #endregion
    }
}
