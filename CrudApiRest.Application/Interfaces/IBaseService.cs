using CrudApiRest.Shared.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrudApiRest.Application.Interfaces
{
    public interface IBaseService<in T>
    {
        dynamic FindById(int id);
        dynamic List(PagingData paging);
        dynamic Update(int id, T param);
        dynamic Insert(T param);
        void Delete(int id);
    }
}
