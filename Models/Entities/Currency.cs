using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{

    [Table("Currencies", Schema = "Accounting")]
    public partial class Currency : Card
    {
        public Currency()
        {
            Initialize();
        }

        public Currency(string code, string name, double value, string partName, double partRate, string isoCode)
        {
            Initialize();
            Code = code;
            Name = name;
            Value = value;
            PartName = partName;
            PartRate = partRate;
            ISOCode = isoCode;
        }

        private void Initialize()
        {
            Bills = new HashSet<Bill>();
            Accounts = new HashSet<Account>();
        }

        /// <summary>
        /// التعادل و للعملة الافتراضية يجب ان يكون يساوي الـ 1
        /// وهو كقيمة افتراضية للتعادل ويمكن تعديله في جميع العمليات
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// اسم جزء العملة متل القرش
        /// </summary>
        [StringLength(128)]
        public string PartName { get; set; }

        /// <summary>
        /// اجزاء العملة
        /// </summary>
        public double PartRate { get; set; }

        /// <summary>
        /// الرمز الدولي
        /// </summary>
        [StringLength(128)]
        public string ISOCode { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
