using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyIslamWebApp.DataTransferObjects.Hadith;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public interface IHadithService
    {
        //Hadith CRUD Operations
        ResultDTO<HadithDTO> GetAllHadiths(int pageIndex, int pageSize);
        HadithDTO GetHadithById(int dailyQuoteId);
        bool DeleteHadith(HadithDTO dua, string userId);
        bool AddHadith(HadithDTO dailyQuote, string userId);
        bool DeleteHadithById(int dailyQuoteId, string userId);
        bool UpdateHadith(HadithDTO dailyQuote, string userId);
    }
}