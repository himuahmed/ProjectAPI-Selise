using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectAPI_Selise.Models;

namespace ProjectAPI_Selise.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options): base(options) {}

        public DbSet<BookModel> Books { get; set; }

    }
}
