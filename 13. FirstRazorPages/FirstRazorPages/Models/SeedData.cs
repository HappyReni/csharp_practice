using FirstRazorPages.Data;
using Microsoft.EntityFrameworkCore;

namespace FirstRazorPages.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FirstRazorPagesContext(serviceProvider.GetRequiredService<DbContextOptions<FirstRazorPagesContext>>()))
            {
                if(context == null || context.Movie == null) 
                {
                    throw new ArgumentNullException("Null FirstRazorPageContext.");
                }

                if (context.Movie.Any())
                {
                    return; // DB has been seeded
                }

                context.Movie.AddRange(
                    new Movie
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        Price = 8000
                    },

                    new Movie
                    {
                        Title = "Ghostbusters ",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = "Comedy",
                        Price = 2000
                    },

                    new Movie
                    {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Price = 3000
                    },

                    new Movie
                    {
                        Title = "Rio Bravo",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = "Western",
                        Price = 1000
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
