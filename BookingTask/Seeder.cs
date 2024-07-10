using DeskBooking.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeskBooking
{
    public class Seeder
    {
        private readonly SMCDbContext _dbContext;

        public Seeder(SMCDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!await _dbContext.Users.AnyAsync())
                {
                    var users = GetUsers();
                    await _dbContext.Users.AddRangeAsync(users);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Name = "Admin",
                    Role = Role.Admin
                },
                new User()
                {
                    Name = "Employee1",
                    Role = Role.Employee
                },
                new User()
                {
                    Name = "Employee2",
                    Role = Role.Employee
                }
            };

            return users;
        }
    }
}
