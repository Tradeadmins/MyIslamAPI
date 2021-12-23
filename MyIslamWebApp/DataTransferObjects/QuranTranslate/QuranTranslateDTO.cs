using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.DataTransferObjects.QuranTranslate
{
    public class QuranTranslateDTO
    {        
        public int QuranTranslateId { get; set; }        
        public string QuranTranslateLanguage { get; set; }       
        public string QuranTranslateBy { get; set; }       
        public string QuranTranslateUrl { get; set; }
    }
}