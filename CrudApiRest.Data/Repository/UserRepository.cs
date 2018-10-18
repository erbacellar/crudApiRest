using CrudApiRest.Data.Context;
using CrudApiRest.Data.Helpers;
using CrudApiRest.Data.Interfaces;
using CrudApiRest.Data.Models;
using CrudApiRest.Shared.Common.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CrudApiRest.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbUsersContext _context;
        public UserRepository(IDbUsersContext context)
        {
            _context = context;
        }

        public Expression<Func<User, bool>> Condition(PagingData paging)
        {
            return x => (x.Id.ToString().Contains(paging.Filter) 
                        || x.Name.ToUpper().Contains(paging.Filter.ToUpper())
                        || x.Login.ToUpper().Contains(paging.Filter.ToUpper()));
        }

        public int Delete(int id)
        {
            var user = FindById(id);
            _context.Entry(user).State = EntityState.Deleted;
            return _context.SaveChanges();
        }

        public User FindById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User Insert(User model)
        {
            if (VerifyDuplicity(model) == null)
            {
                _context.Users.Add(model);                
            }

            if (_context.SaveChanges() > 0)
                return model;
            else
                return null;
        }

        public IEnumerable<dynamic> List(PagingData paging)
        {
            var query = _context.Users.AsQueryable();

            query = query
                    .AddOrderByCodigo(paging, x => x.Id)
                    .AddPagination(paging);

            return query;
        }

        public IEnumerable<dynamic> ListByFilter(PagingData paging)
        {
            var query = _context.Users.Where(Condition(paging));

            query = query
                    .AddOrderByCodigo(paging, x => x.Id)
                    .AddPagination(paging);

            return query;
        }

        public User Update(int id, User model)
        {
            var objBd = FindById(id);
            if (objBd == null)
                return null;

            var objExists = VerifyDuplicity(model);

            if (objExists == null || (objExists.Id == model.Id))
            {
                objBd.Login = model.Login;
                objBd.Name = model.Name;                               
            }

            if (_context.SaveChanges() > 0)
                return model;
            else
                return null;
        }

        public int UpdatePassword(int id, User user)
        {
            var userBd = FindById(user.Id);
            if (userBd != null)
            {                
                userBd.Password = user.Password;
                userBd.Salt = user.Salt;
            }

            return _context.SaveChanges();
        }

        private User VerifyDuplicity(User model)
        {
            return _context.Users.FirstOrDefault(x => x.Login == model.Login);
        }
    }
}
