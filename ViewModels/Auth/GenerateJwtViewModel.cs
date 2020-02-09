using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Auth
{
    public class GenerateJwtViewModel
    {
        public string grant_type { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        /// <summary>
        /// must to hash befor add to database
        /// </summary>
        public string refresh_token { get; set; }
    }
}
