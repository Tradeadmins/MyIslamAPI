using MyIslamWebApp.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyIslamWebApp.Models
{
    public class MakeDua : BaseModel
    {
        /// <summary>
        /// MakeDua's Id
        /// </summary>
        [Key]
        public int MakeDuaId { get; set; }
        /// <summary>
        /// MakeDua's(Prayer Request) Id
        /// </summary>
        [Required]
        public int MakeDuaPrayerRequestId { get; set; }
        /// <summary>
        /// MakeDua's User
        /// </summary>
        [Required]
        public string MakeDuaByUserId { get; set; }
    }
}