using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyIslamWebApp.DataTransferObjects.Dua;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public interface IDuaService
    {
        //Dua CRUD Operations
        ResultDTO<DuaDTO> GetAllDuas(int pageIndex, int pageSize);
        IEnumerable<DuaDTO> GetAllDuaByCategoryId(int duaCategoryId);
        DuaDTO GetDuaById(int duaId);
        bool DeleteDua(DuaDTO dua, string userId);
        bool AddDua(DuaDTO dua, string userId);
        bool DeleteDuaById(int duaId, string userId);
        bool UpdateDua(DuaDTO dua, string userId); 
    }
}