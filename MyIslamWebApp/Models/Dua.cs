using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyIslamWebApp.Enums;
using MyIslamWebApp.Models.Base;

namespace MyIslamWebApp.Models
{
    public class Dua : BaseModel
    {
        /// <summary>
        /// Id of the Dua
        /// </summary>
        [Key]
        public int DuaId { get; set; }
        /// <summary>
        /// Dua Category
        /// </summary>
        [ForeignKey("DuaCategory")]
        public int DuaCategoryId { get; set; }
        public DuaCategory DuaCategory { get; set; }
        /// <summary>
        /// Dua Name
        /// </summary>
        [Required]
        public string DuaName { get; set; }
        /// <summary>
        /// Dua Arabic Text
        /// </summary>
        [Required]
        public string DuaArabicText { get; set; }
        /// <summary>
        /// Dua English Text
        /// </summary>
        [Required]
        public string DuaEnglishText { get; set; }
        /// <summary>
        /// Dua Turkey Text
        /// </summary>
        [Required]
        public string DuaTurkeyText { get; set; }
        /// <summary>
        /// Dua Malay Text
        /// </summary>
        [Required]
        public string DuaMalayText { get; set; }
        /// <summary>
        /// Dua Pronunciation Text
        /// </summary>
        [Required]
        public string DuaPronunciationText { get; set; }        
    }
}