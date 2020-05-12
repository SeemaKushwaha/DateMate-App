using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using DateMate.API.Models;
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

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.Include(p =>p.Photos).ToListAsync();
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}