using CrudApiRest.Application.Helpers;
using CrudApiRest.Application.Interfaces;
using CrudApiRest.Data.Interfaces;
using CrudApiRest.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrudApiRest.Application.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public void UpdatePassword(User user)
        {
            user.Salt = Utils.GetSalt(user.Password);
            user.PasswordEncrypt = Utils.GetSenhaEncrypt(user.Password, user.Salt);

            _repository.UpdatePassword(user);
        }
    }
}
