using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("AuthRefreshTokens", Schema = "CMS")]
    public class AuthRefreshToken : BaseClass
    {
        public AuthRefreshToken()
        {
            Initialize();
        }

        public AuthRefreshToken(string clientId, string subject, string value, DateTimeOffset expires)
        {
            Initialize();
            ClientId = clientId;
            Value = value;
            Expires = expires;
            Subject = subject;
        }

        private void Initialize()
        {
            IsActive = true;
            Issued = DateTimeOffset.UtcNow;
        }

        [Required]
        [MaxLength(256)]
        public string Subject { get; set; }

        public string ClientId { get; set; }
        [Required]
        public string Value { get; set; }

        public DateTimeOffset Issued { get; set; }

        public DateTimeOffset Expires { get; set; }

        public bool IsActive { get; set; }
    }
}
