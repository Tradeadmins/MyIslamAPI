using System;
using MyIslamWebApp.Enums;

namespace MyIslamWebApp.DataTransferObjects.DailyQuotes
{
    public class DailyQuoteDTO
    {
        public int DailyQuoteId { get; set; }
        public string DailyQuoteText { get; set; }
        public AppLangauges DailyQuoteLanguage { get; set; }
        public DateTime DailyQuoteValidOn { get; set; }
    }
}