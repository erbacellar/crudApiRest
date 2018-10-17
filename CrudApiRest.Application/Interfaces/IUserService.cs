using CrudApiRest.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrudApiRest.Application.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        void UpdatePassword(User user);
    }
}
