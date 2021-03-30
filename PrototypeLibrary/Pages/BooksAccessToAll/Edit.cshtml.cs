using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrototypeLibrary.Data;
using PrototypeLibrary.Models;

namespace PrototypeLibrary.Pages.BooksAccessToAll
{
    public class EditModel : PageModel
    {
        private readonly PrototypeLibrary.Data.PrototypeLibraryContext _context;

        public EditModel(PrototypeLibrary.Data.PrototypeLibraryContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PersonalList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalListExists(PersonalList.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PersonalListExists(int id)
        {
            return _context.PersonalList.Any(e => e.Id == id);
        }
    }
}
