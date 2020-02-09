using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public partial class CustomerType
    {
        //private string determinantsOfGenerateAccountName;

        //[NotMapped]
        //public string DeterminantsOfGenerateAccountName
        //{
        //    get
        //    {
        //        determinantsOfGenerateAccountName = "";
        //        if (GenerateCustomerAccountNameWithCustomerTypeName)
        //        {
        //            determinantsOfGenerateAccountName += Resources.CustomerTypes.CustomerTypeRes.CustomerType;
        //        }
        //        if (GenerateCustomerAccountNameWithProvinceName)
        //        {
        //            determinantsOfGenerateAccountName += string.Format(", {0}", Resources.Areas.AreaRes.Province);
        //        }
        //        if (GenerateCustomerAccountNameWithCityName)
        //        {
        //            determinantsOfGenerateAccountName += string.Format(", {0}", Resources.Areas.AreaRes.City);
        //        }
        //        if (GenerateCustomerAccountNameWithNeighborhoodName)
        //        {
        //            determinantsOfGenerateAccountName += string.Format(", {0}", Resources.Areas.AreaRes.Neighborhood);
        //        }
        //        if (GenerateCustomerAccountNameWithStreetName)
        //        {
        //            determinantsOfGenerateAccountName += string.Format(", {0}", Resources.Areas.AreaRes.Street);
        //        }
        //        return determinantsOfGenerateAccountName;
        //    }
        //    set
        //    {
        //        determinantsOfGenerateAccountName = value;
        //    }
        //}

        //private string customerTypeGroupNames;
        //[NotMapped]
        //public string CustomerTypeGroupNames
        //{
        //    get
        //    {
        //        customerTypeGroupNames = "";
        //        var groups = CustomerTypeGroupDetails.ToList().Select(gd => gd.CustomerTypeGroup);
        //        foreach (var group in groups)
        //        {
        //            if (group.Id == groups.Last().Id)
        //            {
        //                customerTypeGroupNames += string.Format("{0}", group.Name);
        //            }
        //            else
        //            {
        //                customerTypeGroupNames += string.Format("{0}, ", group.Name);
        //            }
        //        }
        //        return customerTypeGroupNames;
        //    }
        //}

        //private string customerTypeGroupCodeNames;
        //[NotMapped]
        //public string CustomerTypeGroupCodeNames
        //{
        //    get
        //    {
        //        customerTypeGroupCodeNames = "";
        //        var groups = CustomerTypeGroupDetails.ToList().Select(gd => gd.CustomerTypeGroup);
        //        foreach (var group in groups)
        //        {
        //            if (group.Id == groups.Last().Id)
        //            {
        //                customerTypeGroupCodeNames += string.Format("{0}", group.CodeName);
        //            }
        //            else
        //            {
        //                customerTypeGroupCodeNames += string.Format("{0}, ", group.CodeName);
        //            }
        //        }
        //        return customerTypeGroupCodeNames;
        //    }
        //}
    }
}
