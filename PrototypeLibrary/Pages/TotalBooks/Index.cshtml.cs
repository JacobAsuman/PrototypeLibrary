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
    public class IndexModel : PageModel
    {
        private readonly PrototypeLibrary.Data.PrototypeLibraryContext _context;

        public IndexModel(PrototypeLibrary.Data.PrototypeLibraryContext context)
        {
            _context = context;
        }

        public IList<AllBooksList> AllBooksList { get;set; }

        public async Task OnGetAsync()
        {
            AllBooksList = await _context.AllBooksList.ToListAsync();
        }
    }
}
