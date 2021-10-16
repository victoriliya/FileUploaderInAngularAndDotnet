using CoolApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolApi.Services
{
    public interface IUserRepository
    {
        public int TotalCount { get; set; }
        public Task<IEnumerable<User>> GetUsers(int page, int perPage);
        public Task<bool> AddUsers(User user, List<File> files);
        public Task<User> GetUserByIdAsync(string customerId);
        public Task<User> GetUserByEmailAsync(string email);
        public Task<bool> EmailExist(string email);

    }
}
