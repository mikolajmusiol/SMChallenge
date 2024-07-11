using DeskBooking.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DeskBooking
{
    public class Seeder
    {
        private readonly SMCDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public Seeder(SMCDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!await _dbContext.Users.AnyAsync())
                {
                    var admin = SeedAdmin();
                    await _dbContext.Users.AddAsync(admin);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        private User SeedAdmin()
        {
            var admin = new User()
            {
                Name = "admin",
                Role = Role.Admin
            };

            admin.PasswordHash = _passwordHasher.HashPassword(admin, "admin");

            return admin;
        }
    }
}
