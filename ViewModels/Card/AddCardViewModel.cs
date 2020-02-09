using ERPAPI.ViewModels.DefaultEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Card
{
    public class AddCardViewModel : AddDefaultEntityViewModel
    {
        [Required(ErrorMessageResourceName = "ThisFieldIsRequired", ErrorMessageResourceType = typeof(Resources.Global.Common))]
        [StringLength(128, MinimumLength = 2, ErrorMessageResourceName = "StringMustBeMoreThanTwoCharactersAndNotMoreThan128", ErrorMessageResourceType = typeof(Resources.Global.Common))]
        public  virtual string Code { get; set; }

        public string Note { get; set; }
    }
}
