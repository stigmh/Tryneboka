using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tryneboka.Data;
using Tryneboka.Models;

namespace Tryneboka.Pages;

public class FeedViewModel {
    public int PostId { get; set; }
    public string UserId { get; set; } = "";
    public string Username { get; set; } = "";
    public DateTime Created { get; set; }
    public string Published { get; set; } = "";
    public string Post { get; set; } = "";
}

public class FeedModel : PageModel
{
    private readonly Tryneboka.Data.ApplicationDbContext _context;

    public FeedModel(Tryneboka.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<FeedViewModel> Posts { get; set; }

    public async Task OnGetAsync()
    {
        Posts = await _context.Posts.Join(
            _context.Users,
            post => post.UserId,
            user => user.Id,
            (post, user) => new FeedViewModel {
                PostId = post.PostId,
                UserId = user.Id,
                Username = user.UserName,
                Created = post.Created,
                Published = DateTimeToRelativeTime(post.Created),
                Post = post.Content
            }
        ).OrderByDescending(p => p.Created
        ).ToListAsync();
    }

    private static String DateTimeToRelativeTime(DateTime time) {
        var timespan = new TimeSpan(DateTime.UtcNow.Ticks - time.Ticks);
        var deltaSeconds = Math.Abs(timespan.TotalSeconds);
        const int minute = 60;

        if (deltaSeconds < minute) {
            if (deltaSeconds < 10) {
                return "just now";
            } else {
                return $"{timespan.Seconds} seconds ago";
            }
        }

        if (deltaSeconds < (2 * minute)) {
            return "a minute ago";
        }

        if (deltaSeconds < (45 * minute)) {
            return $"{timespan.Minutes} minutes ago";
        }

        if (deltaSeconds < (90 * minute)) {
            return "an hour ago";
        }

        const int day = 24 * (minute * 60);

        if (deltaSeconds < day) {
            return $"{timespan.Hours} hours ago";
        }

        if (deltaSeconds < (2 * day)) {
            return "yesterday";
        }

        const int month = 30 * day;

        if (deltaSeconds < month) {
            return $"{timespan.Days} days ago";
        }

        if (deltaSeconds < (12 * month)) {
            var months = Convert.ToInt32(
                Math.Floor((double)timespan.Days / 30));

            if (months <= 1) {
                return "one month ago";
            } else {
                return $"{months} months ago";
            }
        } else {
            var years = Convert.ToInt32(
                Math.Floor((double)timespan.Days / 365));

            if (years <= 1) {
                return "one year ago";
            } else {
                return $"{years} years ago";
            }
        }
    }
}
