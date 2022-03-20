using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tryneboka.Data;
using Tryneboka.Models;

namespace Tryneboka.Pages;

public class FeedModel : PageModel
{
    private readonly Tryneboka.Data.ApplicationDbContext _context;

    public FeedModel(Tryneboka.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Post> Posts { get; set; }

    public async Task OnGetAsync()
    {
        Posts = await _context.Posts.ToListAsync();
    }
}
