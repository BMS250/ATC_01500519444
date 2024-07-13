using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1_Razor.Data;
using WebApplication1_Razor.Models;

namespace WebApplication1_Razor.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category Category { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? id)
        {
            if (id != null && id != 0) 
            {
                Category = _db.Categories.Find(id);
            }
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Update(Category);
                _db.SaveChanges();
                TempData["success"] = $"Edited Successfully";
                return RedirectToPage("Index");
            }
            TempData["error"] = $"Editing Failed";
            return Page();
        }
    }
}
