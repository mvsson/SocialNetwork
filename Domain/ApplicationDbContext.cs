using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SocialNetwork.Domain
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "d27478ac-0ac9-4afd-96dd-67c4956f4744",
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "c34632f7-305c-41aa-b431-99c1e17210aa",
                Name = "user",
                NormalizedName = "USER"
            });

            builder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "4735086e-728d-499c-821c-98a2e24be6e3",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "my@email.cum",
                NormalizedEmail = "MY@EMAIL.CUM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "jokoboko")
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "d27478ac-0ac9-4afd-96dd-67c4956f4744",
                UserId = "4735086e-728d-499c-821c-98a2e24be6e3"
            });
        }
    }
}
