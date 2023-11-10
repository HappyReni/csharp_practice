using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FirstRazorPages.Pages.Movie
{
    public class IndexModel : PageModel
    {
        private readonly FirstRazorPages.Data.FirstRazorPagesContext _context;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public IndexModel(FirstRazorPages.Data.FirstRazorPagesContext context)
        {
            _context = context;
        }

        public IList<FirstRazorPages.Models.Movie> Movie { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var movies = from m in _context.Movie
                         select m;

            if(!String.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(m => m.Title.Contains(SearchString));
            }

            Movie = await movies.AsQueryable().ToListAsync();
        }
    }
}
