using MyIslamWebApp.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Models
{
    public class CustomDua : BaseModel
    {
        /// <summary>
        /// Id of the CustomDua
        /// </summary>
        [Key]
        public int CustomDuaId { get; set; }
        /// <summary>
        /// CustomDua Name
        /// </summary>
        [Required]
        public string CustomDuaName { get; set; }
        /// <summary>
        /// CustomDua Text
        /// </summary>
        [Required]
        public string CustomDuaText { get; set; }       
    }
}