using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public partial class CustomerContact
    {
        private string contactWithType;

        [NotMapped]
        public string ContactWithType
        {
            get
            {
                if (ContactType != null)
                {
                    contactWithType = string.Format("{0}:{1}", ContactType.Name, Contact);
                }
                return contactWithType;
            }
        }

        private string contactWithPrefix;

        [NotMapped]
        public string ContactWithPrefix
        {
            get
            {
                if (ContactType != null)
                {
                    contactWithPrefix = string.Format("{0}.{1}", ContactType.Prefix, Contact);
                }
                return contactWithPrefix;
            }
        }
    }
}
