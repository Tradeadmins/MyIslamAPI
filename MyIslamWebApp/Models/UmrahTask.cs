using MyIslamWebApp.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Models
{
    public class UmrahTask : BaseModel
    {
        /// <summary>
        /// Id of the UmrahTask
        /// </summary>
        [Key]
        public int UmrahTaskId { get; set; }
        /// <summary>
        /// UmrahTask Name
        /// </summary>
        [Required]
        public string UmrahTaskName { get; set; }
    }
}