using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyIslamWebApp.DataTransferObjects.JumaQuote;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public interface IJumaQuoteService
    {
        //JumaQuote CRUD Operations
        ResultDTO<JumaQuoteDTO> GetAllJumaQuotes(int pageIndex, int pageSize);
        JumaQuoteDTO GetJumaQuoteById(int dailyQuoteId);
        JumaQuoteDTO GetJumaQuoteByLang_Date(int languagecode, DateTime utcDateTime);
        bool AddJumaQuote(JumaQuoteDTO jumaQuote, string userId);
        bool DeleteJumaQuote(JumaQuoteDTO jumaQuote, string userId);
        bool UpdateJumaQuote(JumaQuoteDTO jumaQuote, string userId);
        bool DeleteJumaQuoteById(int jumaQuoteID, string userId);
    }
}