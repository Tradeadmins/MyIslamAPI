using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyIslamWebApp.DataTransferObjects.DuaCategory;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public interface IDuaCategoryService
    {
        //DuaCategory CRUD Operations
        IEnumerable<DuaCategoryDTO> GetAllDuaCategory();
        DuaCategoryDTO GetDuaCategoryById(int duaCategoryId);
        bool DeleteDuaCategory(DuaCategoryDTO dua, string userId);
        bool AddDuaCategory(DuaCategoryDTO duaCategory, string userId);
        bool DeleteDuaCategoryById(int duaCategoryId, string userId);
        bool UpdateDuaCategory(DuaCategoryDTO duaCategory, string userId);
    }
}