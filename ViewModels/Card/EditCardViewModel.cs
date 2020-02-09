using ERPAPI.ViewModels.DefaultEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Card
{
    public class EditCardViewModel : AddCardViewModel
    {
        public long Id { get; set; }
    }
}
