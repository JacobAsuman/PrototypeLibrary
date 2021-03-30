using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrototypeLibrary.Data;
using PrototypeLibrary.Models;

namespace PrototypeLibrary.Pages.BooksAccessToAll
{
    public class CreateModel : PageModel
    {
        private readonly PrototypeLibrary.Data.PrototypeLibraryContext _context;

        public CreateModel(PrototypeLibrary.Data.PrototypeLibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PersonalList PersonalList { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PersonalList.Add(PersonalList);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
