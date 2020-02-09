using ERPAPI.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.PredefinedGuides
{
    public class PredefinedAccountGuide
    {
        public string ArName { get; set; }
        public string EnName { get; set; }
        public string GetName(string lang)
        {
            var isArabic = lang == "ar";
            if (isArabic)
            {
                return ArName;
            }
            else
            {
                return EnName;
            }
        }
        public int Order { get; set; }
        public Guid DefaultItemAccountId { get; set; }
        public Guid DefaultCashAccountId { get; set; }
        public AccountsGuides Id { get; set; }
        public List<PredefinedAccount> Accounts = new List<PredefinedAccount>();
    }
}
