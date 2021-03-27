using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Domain
{
    public class ApplicationDbContext : IdentityDbContext<CustomUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<UserTag> UserTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "d27478ac-0ac9-4afd-96dd-67c4956f4744",
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            builder.Entity<CustomUser>().HasData(new CustomUser
            {
                Id = "4735086e-728d-499c-821c-98a2e24be6e3",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "my@email.cum",
                NormalizedEmail = "MY@EMAIL.CUM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<CustomUser>().HashPassword(null, "jokoboko"),
                //UserProfile = new() { Id = "c425623b-1898-4b34-bb94-4fff26f45615", CustomUserId = "4735086e-728d-499c-821c-98a2e24be6e3" }
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "d27478ac-0ac9-4afd-96dd-67c4956f4744",
                UserId = "4735086e-728d-499c-821c-98a2e24be6e3"
            });
        }
    }
}
