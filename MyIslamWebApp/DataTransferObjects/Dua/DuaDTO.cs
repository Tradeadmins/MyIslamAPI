using MyIslamWebApp.Enums;

namespace MyIslamWebApp.DataTransferObjects.Dua
{
    public class DuaDTO
    {        
        public int DuaId { get; set; }        
        public int DuaCategoryId { get; set; }       
        public string DuaName { get; set; }        
        public string DuaArabicText { get; set; }       
        public string DuaEnglishText { get; set; }      
        public string DuaTurkeyText { get; set; }       
        public string DuaMalayText { get; set; }        
        public string DuaPronunciationText { get; set; } 
    }
    
}