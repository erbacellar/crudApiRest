using CrudApiRest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrudApiRest.Data.Repository
{
    public class MockData : IMockData
    {
        private readonly List<User> users;
        public MockData()
        {
            users = new List<User>()
            {
                new User() {Id = 1, Login = "teste1", Name = "Joao", Password = ""},
                new User() {Id = 2, Login = "teste2", Name = "Maria", Password = ""},
                new User() {Id = 3, Login = "teste3", Name = "Rafael", Password = ""},
                new User() {Id = 4, Login = "teste4", Name = "Fernanda", Password = ""},
                new User() {Id = 5, Login = "teste5", Name = "Mateus", Password = ""}
            };
        }

        public IQueryable<User> GetUsers()
        {
            return users.AsQueryable(); 
        }

        public User GetById(int id)
        {
            return users.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(User user)
        {
            users.Add(user);
        }

        public void Update(User user)
        {
            var userBd = users.FirstOrDefault(x => x.Id == user.Id);
            userBd.Login = user.Login;
            userBd.Name = user.Name;
        }

        public void Delete(int id)
        {
            var itemToRemove = users.FirstOrDefault(x => x.Id == id);
            if (itemToRemove != null)
                users.Remove(itemToRemove);
        }

        public void UpdatePassword(User user)
        {
            var userBd = GetById(user.Id);
            userBd.Password = user.Password;
            userBd.PasswordEncrypt = user.PasswordEncrypt;
            userBd.Salt = user.Salt;
        }
    }
}
