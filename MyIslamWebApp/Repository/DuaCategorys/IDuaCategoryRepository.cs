using System.Collections.Generic;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;

namespace MyIslamWebApp.Repository.DuaCategorys
{
    public interface IDuaCategoryRepository : IRepository<DuaCategory>
    {
        bool AddDuaCategory(DuaCategory dua);
        bool DeleteDuaCategory(DuaCategory dua);
        bool DeleteDuaCategoryById(int duaId);
        bool UpdateDuaCategory(DuaCategory dua);
        DuaCategory GetDuaCategoryById(int duaId);
        IEnumerable<DuaCategory> GetAllDuaCategorys();
    }
}