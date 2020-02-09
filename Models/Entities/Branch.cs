using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("Branchs", Schema = "HumanResources")]
    public class Branch : Card
    {
        public Branch()
        {
            Initialize();
        }

        public Branch(string name, string code, string note)
        {
            Initialize();
            Name = name;
            Code = code;
            Note = note;
        }

        private void Initialize()
        {
            Branchs = new HashSet<Branch>();
            //EmployeeDepartments = new HashSet<EmployeeDepartment>();
            //VehicleBranchs = new HashSet<VehicleBranch>();
            //Entries = new HashSet<Entry>();
            Bills = new HashSet<Bill>();
        }

        public Guid? ParentId { get; set; }
        public virtual Branch ParentBranch { get; set; }

        //public Guid? AddressId { get; set; }
        //public virtual Address Address { get; set; }

        public string Note { get; set; }

        public virtual ICollection<Branch> Branchs { get; set; }
        //public virtual ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }
        //public virtual ICollection<VehicleBranch> VehicleBranchs { get; set; }
        //public virtual ICollection<Entry> Entries { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
    }
}