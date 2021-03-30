using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrototypeLibrary.Models;

namespace PrototypeLibrary.Data
{
    public class PrototypeLibraryContext : DbContext
    {
        public PrototypeLibraryContext (DbContextOptions<PrototypeLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<PrototypeLibrary.Models.PersonalList> PersonalList { get; set; }

        public DbSet<PrototypeLibrary.Models.AllBooksList> AllBooksList { get; set; }

        public DbSet<PrototypeLibrary.Models.PubliclyAvailableBooks> PubliclyAvailableBooks { get; set; }
        public object PublicBooksTable { get; internal set; }
    }
}
