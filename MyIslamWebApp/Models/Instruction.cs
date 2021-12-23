using MyIslamWebApp.Enums;
using MyIslamWebApp.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MyIslamWebApp.Models
{
    public class Instruction : BaseModel
    {
        /// <summary>
        /// Id of the Instruction
        /// </summary>
        [Key]
        public int InstructionId { get; set; }
        /// <summary>
        /// Instruction's Instruction Language
        /// </summary>
        [Required]
        public AppLangauges InstructionLanguage { get; set; }
        /// <summary>
        /// Instruction's Instruction Name
        /// </summary>
        [Required]
        public string InstructionTitle { get; set; }
        /// <summary>
        /// Instruction's Instruction Text
        /// </summary>
        [Required]
        public string InstructionDescription { get; set; }
        /// <summary>
        /// Instruction's Instruction Image URL
        /// </summary>      
        public string InstructionImageURL { get; set; }
    }
}