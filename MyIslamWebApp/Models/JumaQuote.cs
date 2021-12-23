using MyIslamWebApp.Enums;
using MyIslamWebApp.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyIslamWebApp.Models
{
    public class JumaQuote : BaseModel
    {
        /// <summary>
        /// Id of the JumaQuote
        /// </summary>
        [Key]
        public int JumaQuoteId { get; set; }
        /// <summary>
        /// JumaQuote's JumaQuoteText 
        /// </summary>        
        [Required]
        public string JumaQuoteText { get; set; }
        /// <summary>
        /// JumaQuote's JumaQuote Language
        /// </summary>
        [Required]
        public AppLangauges JumaQuoteLanguage { get; set; }
        /// <summary>
        /// JumaQuote's JumaQuote Valid On
        /// </summary>
        [Required]
        public DateTime JumaQuoteValidOn { get; set; }
    }
}