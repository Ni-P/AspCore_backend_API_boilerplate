using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context,
            UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "User 1",
                        UserName = "user1",
                        Email = "user1@test.com"
                    },
                    new AppUser
                    {
                        DisplayName = "User 2",
                        UserName = "user2",
                        Email = "user2@test.com"
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

            // populate other db sets here


            await context.SaveChangesAsync();
        }
    }
}