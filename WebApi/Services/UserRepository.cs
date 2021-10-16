using CoolApi.Data;
using CoolApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolApi.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public int TotalCount { get; set; }


        private IQueryable<User> GetUserAsync()
        {
            var category = _context.Users.Include(user => user.File);

            TotalCount = category.Count();
            return category;
        }


        public async Task<IEnumerable<User>> GetUsers(int page, int perPage)
        {
            var query = await GetUserAsync().Skip((page - 1) * perPage).Take(perPage).ToListAsync();
            return query;
        }


        public async Task<bool> AddUsers(User user, List<File> files)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.Files.AddRangeAsync(files);
                return await SavedAsync();
            }
            catch (Exception)
            {
                return false;
            }
       
        }

        public  async Task<User> GetUserByIdAsync(string customerId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == customerId);
            return user;
        }


        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.Include(u => u.File).FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<bool> EmailExist(string email)
        {
            var result = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (result == null)
            {
                return false;
            }

            return true;

        }


        public async Task<bool> SavedAsync()
        {
            var valueToReturned = false;
            if (await _context.SaveChangesAsync() > 0)
                valueToReturned = true;
            else
                valueToReturned = false;

            return valueToReturned;
        }
    }
}
