using MyIslamWebApp.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Models
{
    public class HajjGuide : BaseModel
    {
        /// <summary>
        /// Id of the HajjGuide
        /// </summary>
        [Key]
        public int HajjGuideId { get; set; }
        /// <summary>
        /// HajjGuide Name
        /// </summary>
        [Required]
        public string HajjGuideName { get; set; }
    }
}