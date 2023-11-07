using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FirstRazorPages.Pages.Movie
{
    public class IndexModel : PageModel
    {
        private readonly FirstRazorPages.Data.FirstRazorPagesContext _context;

        public IndexModel(FirstRazorPages.Data.FirstRazorPagesContext context)
        {
            _context = context;
        }

        public IList<FirstRazorPages.Models.Movie> Movie { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Movie != null)
            {
                Movie = await _context.Movie.ToListAsync();
            }
        }
    }
}
