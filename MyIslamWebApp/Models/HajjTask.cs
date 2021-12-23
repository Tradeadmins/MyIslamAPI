using MyIslamWebApp.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.Models
{
    public class HajjTask : BaseModel
    {
        /// <summary>
        /// Id of the HajjTask
        /// </summary>
        [Key]
        public int HajjTaskId { get; set; }        
        /// <summary>
        /// HajjTask Name
        /// </summary>
        [Required]
        public string HajjTaskName { get; set; }
    }
}