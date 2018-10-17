﻿using CrudApiRest.Shared.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CrudApiRest.Data.Interfaces
{
    public interface IGenericCrudRepository<TModel>
    {
        IEnumerable<dynamic> List(PagingData paging);
        IEnumerable<dynamic> ListByFilter(PagingData paging);
        TModel FindById(int id);
        TModel Update(TModel model);
        TModel Insert(TModel model);
        void Delete(int id);
        Expression<Func<TModel, bool>> Condition(PagingData paging);
    }
}
