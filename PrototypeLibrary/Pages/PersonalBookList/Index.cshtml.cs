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
    public class IndexModel : PageModel
    {
        private readonly PrototypeLibrary.Data.PrototypeLibraryContext _context;

        public IndexModel(PrototypeLibrary.Data.PrototypeLibraryContext context)
        {
            _context = context;
        }

        public IList<PersonalList> PersonalList { get;set; }

        public async Task OnGetAsync()
        {
            PersonalList = await _context.PersonalList.ToListAsync();
        }
        [BindProperty(SupportsGet = true)]
        public string loanBook { get; set; }
    }
}
