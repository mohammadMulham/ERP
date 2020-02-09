using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface IPayTypeRepository : IDefaultEntityRepository<PayType>
    {
        void LoadReferences(PayType payType);
    }
}
