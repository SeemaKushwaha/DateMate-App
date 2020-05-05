using System.Threading.Tasks;
using System.Collections.Generic;
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
            //_context.Users.Add(entity);
        }
        public void Delete<T>(T entity) where T: class
        {
            //_context.Users.Delete(entity);
        }
        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p =>p.Photos).FirstOrDefaultAsync(n =>n.Id == id);
            return user;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.Include(p =>p.Photos).ToListAsync();
            return users;        
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}