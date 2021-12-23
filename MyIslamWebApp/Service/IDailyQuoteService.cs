using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyIslamWebApp.DataTransferObjects.DailyQuotes;
using MyIslamWebApp.Models;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public interface IDailyQuoteService
    {
        //DailyQuote CRUD Operations
        ResultDTO<DailyQuoteDTO> GetAllDailyQuotes(int pageIndex, int pageSize);
        DailyQuoteDTO GetDailyQuoteById(int dailyQuoteId);
        DailyQuoteDTO GetDailyQuoteByLang_Date(int languageCode, DateTime utcDateTime);
        bool AddDailyQuote(DailyQuoteDTO dailyQuote, string userId);
        bool DeleteDailyQuoteById(int dailyQuoteID, string userId);
        bool UpdateDailyQuote(DailyQuoteDTO dailyQuote, string userId);
        bool DeleteDailyQuote(DailyQuoteDTO dailyQuote, string userId);
    }
}