using System.Collections.Generic;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;
using MyIslamWebApp.DataTransferObjects.Dua;

namespace MyIslamWebApp.Repository.Duas
{
    public interface IDuaRepository : IRepository<Dua>
    {
        bool AddDua(Dua dua);
        bool DeleteDua(Dua dua);
        bool DeleteDuaById(int duaId);
        bool UpdateDua(Dua dua);
        Dua GetDuaById(int duaId);
        Result<Dua> GetAllDuas(int pageIndex, int pageSize);
        IEnumerable<DuaDTO> GetAllDuaByCategoryId(int duaCategoryId);
    }
}