using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace TravelGuide.Pages.Account
{
    public class Edit : PageModel
    {
        private readonly DataBase.Access.PersonContext db;

        public Edit(DataBase.Access.PersonContext _db)
        {
            db = _db;
        }

        [BindProperty] public EditedInfo GetInfo { get; set; }
        [BindProperty] public Person Person { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            Person = await db.Person.FirstOrDefaultAsync(p => p.Id == id);

            return Page();
        }

        public async Task<IActionResult> OnPost(int? id)
        {
            Person = await db.Person.FirstOrDefaultAsync(u => u.Id == id);
            Person.PhoneNumber = GetInfo.PhoneNumber;
            db.Attach(Person).State = EntityState.Modified;
            await db.SaveChangesAsync();

            await Authentication.Authenticate(Person, HttpContext);

            return RedirectToPage("/Account/Profile", new {id = Person.Id});
        }
    }

    public class EditedInfo
    {
        [EmailAddress(ErrorMessage = "Некорректный Email-адрес")]
        [Required(ErrorMessage = "Не указан Email")]
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$", ErrorMessage = "Слишком простой пароль")]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        [Required(ErrorMessage = "Не указан пароль")]
        public string? PasswordConfirm { get; set; }
    }
}