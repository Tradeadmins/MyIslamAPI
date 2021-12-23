using MyIslamWebApp.Enums;
using MyIslamWebApp.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyIslamWebApp.Models
{
    public class DailyQuote : BaseModel
    {
        /// <summary>
        /// Id of the DailyQuote
        /// </summary>
        [Key]
        public int DailyQuoteId { get; set; }
        /// <summary>
        /// DailyQuote's DailyQuote Text
        /// </summary>
        [Required]
        public string DailyQuoteText { get; set; }
        /// <summary>
        /// DailyQuote's DailyQuote Language
        /// </summary>
        [Required]
        public AppLangauges DailyQuoteLanguage { get; set; }
        /// <summary>
        /// DailyQuote's Valid On
        /// </summary>        
        [Required]
        public DateTime DailyQuoteValidOn { get; set; }
    }
}