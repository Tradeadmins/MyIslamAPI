using MyIslamWebApp.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Models
{
    public class QuranTranslate : BaseModel 
    {
        /// <summary>
        /// Id of the QuranTranslate
        /// </summary>
        [Key]
        public int QuranTranslateId { get; set; }        
        /// <summary>
        /// QuranTranslate's Language
        /// </summary>
        [Required]
        public string QuranTranslateLanguage { get; set; }
        /// <summary>
        /// QuranTranslate's By
        /// </summary>        
        [Required]
        public string QuranTranslateBy { get; set; }
        /// <summary>
        /// QuranTranslate's Url
        /// </summary>        
        [Required]
        [MaxLength]
        public string QuranTranslateUrl { get; set; }
    }
}