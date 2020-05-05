using System.Threading.Tasks;
using System.Collections.Generic;
using DateMate.API.Models;

namespace DateMate.API.Data
{
    public interface IDateMateRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);
    }
}