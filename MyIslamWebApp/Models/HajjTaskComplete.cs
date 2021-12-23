using MyIslamWebApp.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Models
{
    public class HajjTaskComplete : BaseModel
    {
        /// <summary>
        /// HajjTask's Id
        /// </summary>
        [Key]
        public int HajjTaskCompleteId { get; set; }
        /// <summary>
        /// HajjTask's(Prayer Request) Id
        /// </summary>
        [Required]
        public int HajjTaskId { get; set; }
        /// <summary>
        /// HajjTask's User
        /// </summary>
        [Required]
        public string HajjTaskCompleteByUserId { get; set; }

    }
}