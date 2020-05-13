using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using DateMate.API.Models;
using DateMate.API.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DateMate.API.Data
{
    public class DateMateRepository: IDateMateRepository
    {
        private readonly DataContext _context;
        public DateMateRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T: class
        {
            _context.Add(entity);
        }
        public void Delete<T>(T entity) where T: class
        {
            _context.Remove(entity);
        }
        public async Task<User> GetUser(int id)
        {
            return await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(n =>n.Id == id);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            return await _context.Photos.FirstOrDefaultAsync(n =>n.Id == id);
        }

         public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(n =>n.UserId==userId).FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users= _context.Users.Include(p =>p.Photos).OrderByDescending(o => o.LastActive).AsQueryable();
            users = users.Where( u => u.Id != userParams.UserId);
            users = users.Where( u => u.Gender == userParams.Gender);
            if(userParams.MinAge != 18 || userParams.MaxAge != 99)
            {
                var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

                users = users.Where(a => a.DateOfBirth >= minDob && a.DateOfBirth <= maxDob);
            }
            if(!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch(userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(o => o.Created);
                        break;
                    default:
                        users = users.OrderByDescending(o => o.LastActive);
                        break;

                }
            }
            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}