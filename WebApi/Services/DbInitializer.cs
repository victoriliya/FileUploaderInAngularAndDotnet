using CoolApi.Data;
using CoolApi.DTOs;
using CoolApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolApi.Services
{
    public class DbInitializer : IDbInitializer

    {
        private readonly AppDbContext _context;

        public DbInitializer(AppDbContext context)
        {
            _context = context;
        }
        public void Initialize()
        {
            if (_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }

            if (_context.Users.Any())
            {
                return;
            }
            else
            {
                var user = new User {
                    Name  = "Lord",
                    Email  = "vee@mail.com",
                    TransactionNumber  = Guid.NewGuid().ToString(),
                };

                var user2 = new User
                {
                    Name = "King",
                    Email = "vee2@mail.com",
                    TransactionNumber = Guid.NewGuid().ToString()
                };


                _context.Users.Add(user);
                _context.Users.Add(user2);

                var file1 = new List<File>
                {
                    new File
                    {
                        UserId = user.Id,
                        FileName = "My Home",
                        FileUrl = "https://res.cloudinary.com/dk4zgckt7/image/upload/v1634335772/test/wjj47d5on7vqgmz9vtmh.jpg",
                    },
                    new File
                    {
                        UserId = user.Id,
                        FileName = "Game",
                        FileUrl = "https://res.cloudinary.com/dk4zgckt7/image/upload/v1634335772/test/wjj47d5on7vqgmz9vtmh.jpg"
                    }

                };

                _context.Files.AddRange(file1);

                var file2 = new List<File>
                {
                    new File
                    {
                        UserId = user2.Id,
                        FileName = "My Home",
                        FileUrl = "https://res.cloudinary.com/dk4zgckt7/image/upload/v1634335772/test/wjj47d5on7vqgmz9vtmh.jpg",
                    },
                    new File
                    {
                        UserId = user2.Id,
                        FileName = "Game",
                        FileUrl = "https://res.cloudinary.com/dk4zgckt7/image/upload/v1634335772/test/wjj47d5on7vqgmz9vtmh.jpg"
                    }

                };


                _context.Files.AddRange(file2);

                _context.SaveChanges();

                return;



            }


        }
    }
}
