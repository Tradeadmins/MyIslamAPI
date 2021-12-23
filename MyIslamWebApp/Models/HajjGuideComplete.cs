using MyIslamWebApp.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Models
{
    public class HajjGuideComplete : BaseModel
    {
        /// <summary>
        /// HajjGuide's Id
        /// </summary>
        [Key]
        public int HajjGuideCompleteId { get; set; }
        /// <summary>
        /// HajjGuide's(Prayer Request) Id
        /// </summary>
        [Required]
        public int HajjGuideId { get; set; }
        /// <summary>
        /// HajjGuide's User
        /// </summary>
        [Required]
        public string HajjGuideCompleteByUserId { get; set; }

    }
}