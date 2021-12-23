using System;
using System.Collections.Generic;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.DataTransferObjects.Result;
using MyIslamWebApp.DataTransferObjects.DailyQuotes;

namespace MyIslamWebApp.Repository.DailyQuotes
{

    public interface IDailyQuoteRepository : IRepository<DailyQuote>
    {
        Result<DailyQuote> GetAllDailyQuotes(int pageIndex, int pageSize);
        DailyQuote GetDailyQuoteById(int dailyQuoteId);
        DailyQuote GetDailyQuoteByLang_Date(int languageCode, DateTime utcDateTime);
        bool AddDailyQuote(DailyQuote dailyQuote);
        bool DeleteDailyQuote(DailyQuote dailyQuote);
        bool DeleteDailyQuoteById(int dailyQuoteId);
        bool UpdateDailyQuote(DailyQuote dailyQuote);
    }
}