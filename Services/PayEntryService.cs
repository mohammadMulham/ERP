using ERPAPI.Models;
using ERPAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Services
{
    public class PayEntryService : IPayEntryService
    {
        IPayTypeRepository _payTypeRepo;
        IPayEntryRepository _payEntryRepo;
        public PayEntryService(IPayEntryRepository payEntryRepo,IPayTypeRepository payTypeRepo)
        {
            _payEntryRepo = payEntryRepo;
            _payTypeRepo = payTypeRepo;
        }
        public long GetNextNumber(long payTypeNumber)
        {
            var payType = _payTypeRepo.Get(payTypeNumber);
            if (payType != null)
            {
                var payTypeId = payType.Id;
                var result = _payEntryRepo.GetAll().Where(p => p.PayTypeId == payTypeId);
                var count = result.Count();
                if (count == 0)
                {
                    return 1;
                }
                var maxNumber = result.Max(e => e.Number);
                return maxNumber + 1;
            }
            else
                return 1;
         
         
           
        }
        public async Task<long> GetNextNumberAsync(long payTypeNumber)
        {
            var payType =  await _payTypeRepo.GetAsync(payTypeNumber);
            if (payType != null)
            {
                var payTypeId = payType.Id;
                var result = _payEntryRepo.GetAll().Where(p => p.PayTypeId == payTypeId);
                var count = result.Count();
                if (count == 0)
                {
                    return 1;
                }
                var maxNumber = result.Max(e => e.Number);
                return maxNumber + 1;
            }
            else
                return 1;
        }

    }
}
