using ERPAPI.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.PredefinedGuides
{
    public class PredefinedCurrency
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Code { get; set; }
        public string ISOCode { get; set; }
        public string ArName { get; set; }
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
        public string EnName { get; set; }
        public string PartArName { get; set; }
        public string PartEnName { get; set; }
        public string GetPartName(string lang)
        {
            var isArabic = lang == "ar";
            if (isArabic)
            {
                return PartArName;
            }
            else
            {
                return PartEnName;
            }
        }
        public double PartRate { get; set; }
    }
}
