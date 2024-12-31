using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppRole>().HasData(
                new AppRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new AppRole
                {
                    Id = "2",
                    Name = "User",
                    NormalizedName = "USER"
                },
                new AppRole
                {
                    Id = "3",
                    Name = "Instructor",
                    NormalizedName = "INSTRUCTOR"
                }
            );

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = "1",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    DisplayName = "Admin",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAECBS57rOBk4rS3X8stW5LOeJTt2XwrAMLMgDrjfG/xOFtbsJY9vWX5mU3RXdIp5JQQ=="
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "1",
                    RoleId = "1"
                }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Title = "C# Fundamentals",
                    Description = "Learn the basics of C#",
                    Category = "Programming",
                    Price = 9.99m,
                    ImageUrl = "https://placehold.co/600x400",
                    Hours = 10,
                    InstructorId = 1
                },
                new Course
                {
                    Id = 2,
                    Title = "ASP.NET Core",
                    Description = "Learn the basics of ASP.NET Core",
                    Category = "Programming",
                    Price = 19.99m,
                    ImageUrl = "https://placehold.co/600x400",
                    Hours = 20,
                    InstructorId = 1
                },
                new Course
                {
                    Id = 3,
                    Title = "Angular",
                    Description = "Learn the basics of Angular",
                    Category = "Programming",
                    Price = 14.99m,
                    ImageUrl = "https://placehold.co/600x400",
                    Hours = 15,
                    InstructorId = 1
                }
            );
        }

    }

}
