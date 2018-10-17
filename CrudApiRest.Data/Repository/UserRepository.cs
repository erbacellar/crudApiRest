using CrudApiRest.Data.Helpers;
using CrudApiRest.Data.Interfaces;
using CrudApiRest.Data.Models;
using CrudApiRest.Shared.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CrudApiRest.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMockData _mockRepository;
        public UserRepository(IMockData mockData)
        {
            _mockRepository = mockData;
        }

        public Expression<Func<User, bool>> Condition(PagingData paging)
        {
            return x => (x.Id.ToString().Contains(paging.Filter) 
                        || x.Name.ToUpper().Contains(paging.Filter.ToUpper())
                        || x.Login.ToUpper().Contains(paging.Filter.ToUpper()));
        }

        public void Delete(int id)
        {
            _mockRepository.Delete(id);
        }

        public User FindById(int id)
        {
            return _mockRepository.GetById(id);
        }

        public User Insert(User model)
        {
            if (VerifyDuplicity(model) == null)
            {
                _mockRepository.Insert(model);
                return model;
            }
            return null;
        }

        public IEnumerable<dynamic> List(PagingData paging)
        {
            var query = _mockRepository.GetUsers();

            query = query
                    .AddOrderByCodigo(paging, x => x.Id)
                    .AddPagination(paging);

            return query;
        }

        public IEnumerable<dynamic> ListByFilter(PagingData paging)
        {
            var query = _mockRepository.GetUsers().Where(Condition(paging));

            query = query
                    .AddOrderByCodigo(paging, x => x.Id)
                    .AddPagination(paging);

            return query;
        }

        public User Update(User model)
        {
            var objBd = FindById(model.Id);
            if (objBd == null)
                return null;

            var objExists = VerifyDuplicity(model);

            if (objExists == null || (objExists.Id == model.Id))
            {
                _mockRepository.Update(model);
                return model;
            }
            else
                return null;

        }

        public void UpdatePassword(User user)
        {
            if(FindById(user.Id) != null)
                _mockRepository.UpdatePassword(user);
        }

        private User VerifyDuplicity(User model)
        {
            return _mockRepository.GetUsers().FirstOrDefault(x => x.Login == model.Login);
        }
    }
}
