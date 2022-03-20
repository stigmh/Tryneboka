using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Tryneboka.Data {
    public static class DbInitializer {
        public static async void Initialize(ApplicationDbContext context) {
            // Check if DB has been seeded
            if (context.Users.Any()) {
                return; // DB has been seeded
            }

            var users = new IdentityUser[]
            {
                new IdentityUser{UserName="Y_122",PasswordHash="austin"},
                new IdentityUser{UserName="Snoo26837",PasswordHash="princess"},
                new IdentityUser{UserName="boiwotm88",PasswordHash="diamond"},
                new IdentityUser{UserName="beerbellybegone",PasswordHash="qwertyui"}
            };

            var userStore = new UserStore<IdentityUser>(context);
            var hasher = new PasswordHasher<IdentityUser>();

            foreach (var user in users) {
                user.PasswordHash = hasher.HashPassword(user, user.PasswordHash);
                user.NormalizedUserName = user.UserName;
                user.Email = "";
                user.NormalizedEmail = "";
                user.AccessFailedCount = 0;
                user.EmailConfirmed = false;
                user.LockoutEnabled = false;
                user.PhoneNumber = "";
                user.PhoneNumberConfirmed = false;
                user.TwoFactorEnabled = false;

                await userStore.CreateAsync(user);
            }

            context.SaveChanges();

            /*
            var posts = new Post[]
            {
                new Post{
                    UserID=4,
                    Created=DateTime.Parse("Mar 12, 2020 19:03"),
                    Content="Oh wow, this is the best social network!"
                },
                new Post{
                    UserID=2,
                    Created=DateTime.Parse("Aug 20, 2020 02:10"),
                    Content="No\nDiggidy\n\n!"
                },
                new Post{
                    UserID=1,
                    Created=DateTime.Parse("Nov 11, 2020 23:45"),
                    Content="No\nDiggidy\n\n!"
                },
                new Post{
                    UserID=3,
                    Created=DateTime.Parse("Dec 22, 2021 13:37"),
                    Content="\"Do or do not, there is no try\" - Gandalf Dumbledore"
                }
            };

            context.Posts.AddRange(posts);
            context.SaveChanges();
            */
        }
    }
}