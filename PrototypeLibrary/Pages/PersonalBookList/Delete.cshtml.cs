using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PrototypeLibrary.Data;
using PrototypeLibrary.Models;

namespace PrototypeLibrary.Pages.PersonalBookList
{
    public class DeleteModel : PageModel
    {
        private readonly PrototypeLibrary.Data.PrototypeLibraryContext _context;

        public DeleteModel(PrototypeLibrary.Data.PrototypeLibraryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PersonalList PersonalList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PersonalList = await _context.PersonalList.FirstOrDefaultAsync(m => m.Id == id);

            if (PersonalList == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PersonalList = await _context.PersonalList.FindAsync(id);

            if (PersonalList != null)
            {
                _context.PersonalList.Remove(PersonalList);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
