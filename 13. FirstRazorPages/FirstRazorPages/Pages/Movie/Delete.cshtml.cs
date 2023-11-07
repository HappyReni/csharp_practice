using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FirstRazorPages.Pages.Movie
{
    public class DeleteModel : PageModel
    {
        private readonly FirstRazorPages.Data.FirstRazorPagesContext _context;

        public DeleteModel(FirstRazorPages.Data.FirstRazorPagesContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }
            var movie = await _context.Movie.FindAsync(id);

            if (movie != null)
            {
                Movie = movie;
                _context.Movie.Remove(Movie);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
