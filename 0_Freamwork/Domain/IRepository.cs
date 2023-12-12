using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace _0_Freamwork.Domain
{
    public interface IRepository<Tkey, T> where T : class
    {
        T GetById(Tkey id);
        List<T> Get();
        void Create(T entity);
        bool Exists(Expression<Func<T, bool>> expression);
        void SaveChanges();
    }
}
