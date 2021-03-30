using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PrototypeLibrary.Data;
using PrototypeLibrary.Models;

namespace PrototypeLibrary.Pages.BooksAccessToAll
{
    public class IndexModel : PageModel
    {
        private readonly PrototypeLibrary.Data.PrototypeLibraryContext _context;

        public IndexModel(PrototypeLibrary.Data.PrototypeLibraryContext context)
        {
            _context = context;
        }

        public IList<PersonalList> PersonalList { get; set; }

        public async Task OnGetAsync()
        {
            PersonalList = await _context.PersonalList.ToListAsync();
        }
        [BindProperty(SupportsGet =true)]
        public string searchTitle { get; set; }
        public List<PersonalList> BooksList { get; set; }
        public void OnGet()
        {
            string DbConnection = @"Data Source=(Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PrototypeLibraryContext-82337a75-df2c-4595-837a-7e5d54982d28;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM PersonalList";
                    if (!string.IsNullOrEmpty(searchTitle))
                {
                    command.CommandText += " WHERE (BookTitle LIKE '%' + @searchField) OR (BookTitle LIKE @searchField + '%')";
                    command.Parameters.AddWithValue("@BookTitle", (searchTitle));
                }

                SqlDataReader reader = command.ExecuteReader();

                
            }


        }
    }
}
