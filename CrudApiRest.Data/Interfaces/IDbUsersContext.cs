using CrudApiRest.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrudApiRest.Data.Interfaces
{
    public interface IDbUsersContext
    {
        DbSet<User> Users { get; set; }
    }
}
