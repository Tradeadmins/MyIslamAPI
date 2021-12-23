using MyIslamWebApp.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Models
{
    public class Hadith : BaseModel
    {
        /// <summary>
        /// Id of the Hadith
        /// </summary>
        [Key]
        public int HadithId { get; set; }       
        /// <summary>
        /// Hadith Arabic Text
        /// </summary>
        [Required]
        public string HadithArabicText { get; set; }
        /// <summary>
        /// Hadith English Text
        /// </summary>
        [Required]
        public string HadithEnglishText { get; set; }
        /// <summary>
        /// Hadith Turkey Text
        /// </summary>
        [Required]
        public string HadithTurkeyText { get; set; }
        /// <summary>
        /// Hadith Malay Text
        /// </summary>
        [Required]
        public string HadithMalayText { get; set; }
        /// <summary>
        /// Hadith Pronunciation Text
        /// </summary>
        [Required]
        public string HadithPronunciationText { get; set; }
    }
}