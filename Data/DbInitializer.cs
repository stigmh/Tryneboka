using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Tryneboka.Models;

namespace Tryneboka.Data {
    public static class DbInitializer {
        public static async void Initialize(ApplicationDbContext context) {
            // Check if DB has been seeded
            if (context.Users.Any()) {
                return; // DB has been seeded
            }

            var users = new User[]
            {
                new User{UserName="Y_122",PasswordHash="austin"},
                new User{UserName="Snoo26837",PasswordHash="princess"},
                new User{UserName="boiwotm88",PasswordHash="diamond"},
                new User{UserName="beerbellybegone",PasswordHash="qwertyui"},
                new User{UserName="bezhad2000",PasswordHash="avangsert"}
            };


            var userStore = new UserStore<User>(context);
            var hasher = new PasswordHasher<User>();

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

            var savedUsers = context.Users.ToList();

            var postContents = new List<string>(){
                "Oh wow, this is the best social network!",
                "Actually... here's 10 reasons why Android is better than iPhone",
                "No\nDiggidy\n\n!",
                "\"Do or do not, there is no try\" - Gandalf Dumbledore",
                "That is a game I've never played and know nothing about.",
                "my grandson is suposed to be having a job interview is he there"
            };

            var gratitude = "<div style=\"display: flex; align-items: center; "
                + "gap: 1rem;\"><div><img src=\"erik.jpg\" "
                + "alt=\"Erik\" width=\"100px\" /></div><div "
                + "style=\"flex-grow: 1;\">This awesome guy made the Docker "
                + "image multi-patform so it runs on both amd64 and arm64, "
                + "including M1 Macs! Thanks brushj!</div></div>";

            string[] postDates = {
                "Mar 12, 2020 19:03",
                "Aug 31, 2022 02:10",
                "Sep 03, 2022 23:45",
                "Dec 22, 2021 13:37"
            };

            var random = new Random();
            var postDateIdx = 0;
            var posts = new List<Post>();

            foreach (var savedUser in savedUsers) {
                var postIdx = random.Next(postContents.Count);
                Console.WriteLine($"User {savedUser.UserName}'s ID is ${savedUser.Id}\n");

                if (savedUser.UserName == "bezhad2000") {
                    posts.Add(new Post{
                        Content = gratitude,
                        Created = DateTime.Parse("Aug 15, 2022 22:41"),
                        UserId = savedUser.Id
                    });
                } else {
                    posts.Add(new Post{
                        Content = postContents[postIdx],
                        Created = DateTime.Parse(postDates[postDateIdx++]),
                        UserId = savedUser.Id
                    });

                    postContents.RemoveAt(postIdx);
                }
            }

            context.Posts.AddRange(posts);
            context.SaveChanges();
        }
    }
}
