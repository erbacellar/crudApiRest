using CrudApiRest.Application.Helpers;
using CrudApiRest.Application.Interfaces;
using CrudApiRest.Data.Interfaces;
using CrudApiRest.Data.Models;
using CrudApiRest.Shared.Common.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CrudApiRest.Application.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly new IUserRepository _repository;
        private const string KEY = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";

        public UserService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public override dynamic Insert(User param)
        {
            param = EncryptPass(param);
            return _repository.Insert(param);
        }

        public int UpdatePassword(int id, User user)
        {
            user = EncryptPass(user);
            return _repository.UpdatePassword(id, user);
        }

        public dynamic GetJwtToken(int id)
        {
            var user = FindById(id);
            if (user != null)
                return GenerateToken(user);
            else
                return null;
        }

        private dynamic GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var header = new JwtHeader(credentials);

            var payload = new JwtPayload
           {
               { "login", user.Login},
               { "name", user.Name},
               { "id", user.Id},
           };

            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            var tokenString = handler.WriteToken(secToken);

            return new JwtToken() { Token = tokenString };
        }

        private User EncryptPass(User user)
        {
            user.Salt = Utils.GetSalt(user.Password);
            user.Password = Utils.GetSenhaEncrypt(user.Password, user.Salt);

            return user;
        }
    }
}
