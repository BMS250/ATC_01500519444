using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1_Razor.Data;
using WebApplication1_Razor.Models;

namespace WebApplication1_Razor.Pages.Categories
{
    //It can be BindProperties if there is one or more properties (with removing BindProperty below), or write BindProperty over each property
    //[BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category Category { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            _db.Categories.Add(Category);
            _db.SaveChanges();
            TempData["success"] = $"Created Successfully";
            return RedirectToPage("Index");
        }
        // The next function can replace the previous one without Binding
        //public IActionResult OnPost(Category category)
        //{
        //    _db.Categories.Add(category);
        //    _db.SaveChanges();
        //    return RedirectToPage("Index");
        //}
    }
}
