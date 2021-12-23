using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyIslamWebApp.DataTransferObjects.CustomDua;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public interface ICustomDuaService
    {
        //CustomDua CRUD Operations
        ResultDTO<CustomDuaDTO> GetAllCustomDuas(int pageIndex, int pageSize);
        CustomDuaDTO GetCustomDuaById(int dailyQuoteId);
        IEnumerable<CustomDuaDTO> GetAllCustomDuaByUserId(string userId);
        bool DeleteCustomDua(CustomDuaDTO dua, string userId);
        bool AddCustomDua(CustomDuaDTO dailyQuote, string userId);
        bool DeleteCustomDuaById(int dailyQuoteId, string userId);
        bool UpdateCustomDua(CustomDuaDTO dailyQuote, string userId);      
    }
}