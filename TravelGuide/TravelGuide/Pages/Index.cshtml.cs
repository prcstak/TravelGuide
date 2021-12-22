using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TravelGuide.Pages
{
    public class Index : PageModel
    {
        

        [Authorize]
        public void OnGet()
        {
            
        }
    }
}