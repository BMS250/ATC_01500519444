using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1_Razor.Data;
using WebApplication1_Razor.Models;

namespace WebApplication1_Razor.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext db)
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
            _db.Remove(Category);
            _db.SaveChanges();
            TempData["success"] = $"Deleted Successfully";
            return RedirectToPage("Index");
        }

    }
}
