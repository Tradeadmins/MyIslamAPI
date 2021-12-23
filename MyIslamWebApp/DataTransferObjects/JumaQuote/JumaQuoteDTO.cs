using System;
using MyIslamWebApp.Enums;

namespace MyIslamWebApp.DataTransferObjects.JumaQuote
{
    public class JumaQuoteDTO
    {
        public int JumaQuoteId { get; set; }
        public string JumaQuoteText { get; set; }
        public AppLangauges JumaQuoteLanguage { get; set; }
        public DateTime JumaQuoteValidOn { get; set; }
    }
}