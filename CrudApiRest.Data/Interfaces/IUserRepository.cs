using CrudApiRest.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrudApiRest.Data.Interfaces
{
    public interface IUserRepository : IGenericCrudRepository<User>
    {
        int UpdatePassword(int id, User user);
    }
}
