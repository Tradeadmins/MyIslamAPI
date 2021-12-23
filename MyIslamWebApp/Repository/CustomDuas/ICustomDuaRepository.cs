using System.Collections.Generic;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;
using MyIslamWebApp.DataTransferObjects.CustomDua;

namespace MyIslamWebApp.Repository.CustomDuas
{
    public interface ICustomDuaRepository : IRepository<CustomDua>
    {
        bool AddCustomDua(CustomDua dua);
        bool DeleteCustomDua(CustomDua dua);
        bool DeleteCustomDuaById(int duaId);
        bool UpdateCustomDua(CustomDua dua);
        CustomDua GetCustomDuaById(int duaId);
        Result<CustomDua> GetAllCustomDuas(int pageIndex, int pageSize);
        IEnumerable<CustomDuaDTO> GetAllCustomDuaByUserId(string userId);
    }
}