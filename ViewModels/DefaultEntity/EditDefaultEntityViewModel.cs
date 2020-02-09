using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.DefaultEntity
{
    public class EditDefaultEntityViewModel : AddDefaultEntityViewModel
    {
        public long Id { get; set; }
    }
}
