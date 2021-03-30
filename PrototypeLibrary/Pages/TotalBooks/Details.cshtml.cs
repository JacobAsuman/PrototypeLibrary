using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PrototypeLibrary.Data;
using PrototypeLibrary.Models;

namespace PrototypeLibrary.Pages.TotalBooks
{
    public class DetailsModel : PageModel
    {
        private readonly PrototypeLibrary.Data.PrototypeLibraryContext _context;

        public DetailsModel(PrototypeLibrary.Data.PrototypeLibraryContext context)
        {
            _context = context;
        }

        public AllBooksList AllBooksList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AllBooksList = await _context.AllBooksList.FirstOrDefaultAsync(m => m.Id == id);

            if (AllBooksList == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
