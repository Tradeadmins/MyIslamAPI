using MyIslamWebApp.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Models
{
    public class UmrahGuideComplete : BaseModel
    {
        /// <summary>
        /// UmrahGuide's Id
        /// </summary>
        [Key]
        public int UmrahGuideCompleteId { get; set; }
        /// <summary>
        /// UmrahGuide's(Prayer Request) Id
        /// </summary>
        [Required]
        public int UmrahGuideId { get; set; }
        /// <summary>
        /// UmrahGuide's User
        /// </summary>
        [Required]
        public string UmrahGuideCompleteByUserId { get; set; }

    }
}