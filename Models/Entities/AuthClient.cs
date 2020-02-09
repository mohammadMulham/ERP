using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("AuthClients", Schema = "CMS")]
    public class AuthClient : BaseClass
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Secret { get; set; }
        public ApplicationType ApplicationType { get; set; }
        /// <summary>
        /// in minutes
        /// </summary>
        public int RefreshTokenLifeTime { get; set; }
        [MaxLength(100)]
        public string AllowedOrigin { get; set; }
        public bool Active { get; set; }
    }
}
