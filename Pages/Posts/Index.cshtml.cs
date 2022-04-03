#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tryneboka.Data;
using Tryneboka.Models;

namespace Tryneboka.Pages.Posts
{
    public class IndexModel : PageModel
    {
        private readonly Tryneboka.Data.ApplicationDbContext _context;

        public IndexModel(Tryneboka.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Post> Post { get;set; }

        public async Task OnGetAsync()
        {
            Post = await _context.Posts.ToListAsync();
        }
    }
}
