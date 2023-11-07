using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FirstRazorPages.Models;

namespace FirstRazorPages.Data
{
    public class FirstRazorPagesContext : DbContext
    {
        public FirstRazorPagesContext (DbContextOptions<FirstRazorPagesContext> options)
            : base(options)
        {
        }

        public DbSet<FirstRazorPages.Models.Movie> Movie { get; set; } = default!;
    }
}
