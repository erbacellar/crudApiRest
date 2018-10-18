using CrudApiRest.Application.Interfaces;
using CrudApiRest.Data.Interfaces;
using CrudApiRest.Shared.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrudApiRest.Application.Services
{
    public abstract class BaseService<T> : IBaseService<T>
    {
        protected readonly IGenericCrudRepository<T> _repository;
        protected BaseService(IGenericCrudRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual dynamic List(PagingData paging)
        {
            if (!string.IsNullOrEmpty(paging.Filter))
                return _repository.ListByFilter(paging);

            return _repository.List(paging);
        }

        public virtual dynamic FindById(int id)
        {
            return _repository.FindById(id);
        }

        public virtual dynamic Update(int id, T param)
        {
            return _repository.Update(id, param);
        }

        public virtual dynamic Insert(T param)
        {
            return _repository.Insert(param);
        }

        public virtual int Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
