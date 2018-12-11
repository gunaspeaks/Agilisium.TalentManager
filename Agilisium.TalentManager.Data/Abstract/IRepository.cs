using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Agilisium.TalentManager.Data.Abstract
{
    public interface IRepository<T> : IDisposable where T : class
    {
        bool Exists(string itemName);

        bool Exists(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        T GetByID(int id);

        IEnumerable<T> GetAll();
    }
}
