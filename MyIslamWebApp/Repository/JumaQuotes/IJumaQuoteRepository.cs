using System;
using System.Collections.Generic;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;

namespace MyIslamWebApp.Repository.JumaQuotes
{
    public interface IJumaQuoteRepository : IRepository<JumaQuote>
    {
        Result<JumaQuote> GetAllJumaQuotes(int pageIndex, int pageSize);
        JumaQuote GetJumaQuoteById(int jumaQuoteId);

        JumaQuote GetJumaQuoteByLang_Date(int languagecode, DateTime utcDateTime);
        bool AddJumaQuote(JumaQuote jumaQuote);

        bool DeleteJumaQuote(JumaQuote jumaQuote);
        bool DeleteJumaQuoteById(int jumaQuoteId);

        bool UpdateJumaQuote(JumaQuote jumaQuote);
    }
}