using MyIslamWebApp.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Models
{
    public class UmrahGuide : BaseModel
    {
        // <summary>
        /// Id of the UmrahGuide
        /// </summary>
        [Key]
        public int UmrahGuideId { get; set; }
        /// <summary>
        /// UmrahGuide Name
        /// </summary>
        [Required]
        public string UmrahGuideName { get; set; }
    }
}