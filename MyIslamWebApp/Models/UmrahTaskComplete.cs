using MyIslamWebApp.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Models
{
    public class UmrahTaskComplete : BaseModel
    {
        /// <summary>
        /// UmrahTask's Id
        /// </summary>
        [Key]
        public int UmrahTaskCompleteId { get; set; }
        /// <summary>
        /// UmrahTask's(Prayer Request) Id
        /// </summary>
        [Required]
        public int UmrahTaskId { get; set; }
        /// <summary>
        /// UmrahTask's User
        /// </summary>
        [Required]
        public string UmrahTaskCompleteByUserId { get; set; }
    }
}