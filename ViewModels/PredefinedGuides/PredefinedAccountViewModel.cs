using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.PredefinedGuides
{
    public class PredefinedAccountViewModel
    {
        public Guid Id { get; set; }
        public long Number { get; set; }
        public string Code { get; set; }
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
        public int Type { get; set; }
        public Guid? FinalAccountId { get; set; }
        public Guid? ParentId { get; set; }
    }
}
