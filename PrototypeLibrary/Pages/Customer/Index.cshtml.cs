using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using PrototypeLibrary.Models;

namespace PrototypeLibrary.Pages.Customer
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<LoanedBook> Books { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTitle { get; set; }
        [BindProperty]
        public List<bool> isDelete { get; set; }

        public List<LoanedBook> BookToDelete { get; set; }

        public List<PersonalList> LoanedBook { get; set; }

        public List<PersonalList> LoanedBookTitle { get; set; }
        public void OnGet()
        {
            // Connect to Database
            DBConnection dbstring = new DBConnection();
            string DBConnection = dbstring.DbString();
            SqlConnection conn = new SqlConnection(DBConnection);
            conn.Open();
            int userSessionID = 123;
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;

                //SQL Query
                Books = new List<LoanedBook>();
                command.CommandText = @"SELECT * FROM MemberBook WHERE UsernameId = @uID";
                command.Parameters.AddWithValue("@uID", userSessionID);
                
                if (!string.IsNullOrEmpty(SearchTitle))
                {
                    command.CommandText += " WHERE (BookTitle LIKE '%' + @BookTitle) OR (BookTitle LIKE @BookTitle + '%') OR (BookTitle LIKE '%' + @BookTitle + '%')";
                    command.Parameters.AddWithValue("@BookTitle", (SearchTitle));
                }
                
                isDelete = new List<bool> { false };

                SqlDataReader reader = command.ExecuteReader(); //read records

                LoanedBook = new List<PersonalList>();
                while (reader.Read())
                {
                    PersonalList record = new PersonalList();
                    record.BookID = reader.GetInt32(2);
                    record.DateLoaned = reader.GetString(3);
                    Console.WriteLine(record.BookID);
                    Console.WriteLine(record.DateLoaned);
                    LoanedBook.Add(record);
                }
                reader.Close();
            }
            List<int> BookId = new List<int>();
            for (int i = 0; i < LoanedBook.Count; i++)
            {
                int Id = LoanedBook[i].BookID;
                BookId.Add(Id);
            }
            BookId = BookId.Distinct().ToList();

            DBConnection dbstring2 = new DBConnection();
            string DBConnection2 = dbstring.DbString();
            SqlConnection conn2 = new SqlConnection(DBConnection);
            conn2.Open();
         

                //SQL Query
                for(int i = 0; i<BookId.Count; i++)
                {

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = conn2;
                        Books = new List<LoanedBook>();
                        command.CommandText = @"SELECT * FROM PersonalList WHERE BookID = @BKID";
                        command.Parameters.AddWithValue("@BKID", BookId[i]);
                        SqlDataReader reader = command.ExecuteReader();
                        LoanedBookTitle = new List<PersonalList>();
                        while (reader.Read())
                        {
                            PersonalList record = new PersonalList();
                            record.BookID = reader.GetInt32(1);
                            record.BookTitle = reader.GetString(2);
                            record.Author = reader.GetString(3);
                            record.Genre = reader.GetString(4);
                            Console.WriteLine(record.BookID);
                            Console.WriteLine(record.BookTitle);
                            Console.WriteLine(record.Author);
                            Console.WriteLine(record.Genre);
                            LoanedBook.Add(record);
                        }
                        reader.Close();
                    }  
            }

                isDelete = new List<bool> { };

            for (int i = 0; i < Books.Count; i++)
            {
                if (isDelete[i] == true)
                {
                    BookToDelete.Add(Books[i]);
                }

            }
        }

        public IActionResult OnPost()
        {
            Console.WriteLine("OnPost");
            for (int i = 0; i < BookToDelete.Count(); i++)
            {
                Console.WriteLine("BookID: " + BookToDelete[i].BookTitle);
            }

            DBConnection dbstring = new DBConnection();
            string DBConnection = dbstring.DbString();
            SqlConnection conn = new SqlConnection(DBConnection);
            conn.Open();

            for (int i = 0; i < BookToDelete.Count; i++)
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = @"DELETE FROM MemberBook WHERE LoanedBookId = BookToDelete[i].Id";
                }
            }

            return RedirectToPage("/Index");
        }
    }
}
