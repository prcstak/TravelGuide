using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TravelGuide.Pages
{
    public class Test : PageModel
    {
        public void OnGet()
        {
            
        }

        public IActionResult OnGetTest()
        {
            return Content("test");
        }
    }
}