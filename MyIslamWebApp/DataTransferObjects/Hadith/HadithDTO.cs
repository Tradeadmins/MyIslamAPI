using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.DataTransferObjects.Hadith
{
    public class HadithDTO
    {      
        public int HadithId { get; set; }      
        public string HadithArabicText { get; set; }      
        public string HadithEnglishText { get; set; }        
        public string HadithTurkeyText { get; set; }       
        public string HadithMalayText { get; set; }       
        public string HadithPronunciationText { get; set; }
    }
}