using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FirstRazorPages.Pages.Movie
{
    public class DetailsModel : PageModel
    {
        private readonly FirstRazorPages.Data.FirstRazorPagesContext _context;

        public DetailsModel(FirstRazorPages.Data.FirstRazorPagesContext context)
        {
            _context = context;
        }

        public FirstRazorPages.Models.Movie Movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                Movie = movie;
            }
            return Page();
        }
    }
}
