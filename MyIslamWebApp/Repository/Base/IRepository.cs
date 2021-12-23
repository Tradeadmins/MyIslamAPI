using System.Collections.Generic;

namespace MyIslamWebApp.Repository.Base
{
    /// <summary>
    /// A generic repository interface (concept of generic classes) which takes a Model as class and operates accordingly
    /// </summary>
    /// <typeparam name="T">T is a generic class which will be passed on runtime</typeparam>
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool DeleteById(string id);
    }
}