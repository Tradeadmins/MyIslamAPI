using MyIslamWebApp.Enums;

namespace MyIslamWebApp.DataTransferObjects.Instruction
{
    public class InstructionDTO
    {
        public int InstructionId { get; set; }
        public string InstructionTitle { get; set; }
        public string InstructionDescription { get; set; }
        public string InstructionImageURL { get; set; }
        public AppLangauges InstructionLanguage { get; set; }
    }
}