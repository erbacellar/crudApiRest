using System.Linq;
using CrudApiRest.Data.Models;

namespace CrudApiRest.Data.Repository
{
    public interface IMockData
    {
        void Delete(int id);
        User GetById(int id);
        IQueryable<User> GetUsers();
        void Insert(User user);
        void Update(User user);
        void UpdatePassword(User user);
    }
}