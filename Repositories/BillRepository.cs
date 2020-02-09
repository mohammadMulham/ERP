using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class BillRepository : BaseEntityRepository<Bill>, IBillRepository
    {
        public BillRepository(ERPContext context) : base(context)
        {
        }

        public override IQueryable<Bill> GetAll()
        {
            return base.GetAll();
        }

        public IQueryable<Bill> GetAllByTypeId(Guid typeId)
        {
            var bills = GetAllNoTracking().Where(e => e.BillTypeId == typeId);
            return bills;
        }

        public override IQueryable<Bill> NativeGetAll()
        {
            return base.NativeGetAll()
                        .Where(e => e.FinancialPeriodId == PerdioId)
                        .Include(a => a.FinancialPeriod)
                        .Include(a => a.Currency)
                        .Include(a => a.BillType)
                        .Include(a => a.Account)
                        .Include(a => a.Branch)
                        .Include(a => a.CostCenter)
                        .Include(a => a.CustomerAccount)
                        .Include(a => a.Store)
                        .Include(a => a.BillItems)
                            .ThenInclude(b => b.ItemUnit)
                                .ThenInclude(b => b.Item)
                         .Include(a => a.BillItems)
                            .ThenInclude(b => b.ItemUnit)
                                .ThenInclude(b => b.Unit)
                        .Include(a => a.BillItems)
                            .ThenInclude(b => b.Store)
                        .Include(a => a.BillItems)
                            .ThenInclude(b => b.CostCenter)
                        .Include(a => a.BillEntry)
                            .ThenInclude(b => b.Entry)
                              .ThenInclude(c => c.Items)
                        ;
        }

        public IQueryable<Bill> GetBeginningInventory()
        {
            var bills = GetAll().Where(e => e.BillType.Type == BillsType.BeginningInventory);
            return bills;
        }

        public IQueryable<Bill> GetEndPeriodInventory()
        {
            var bills = GetAll().Where(e => e.BillType.Type == BillsType.EndPeriodInventory);
            return bills;
        }

        public IQueryable<Bill> GetPurchase()
        {
            var bills = GetAll().Where(e => e.BillType.Type == BillsType.Purchase);
            return bills;
        }

        public IQueryable<Bill> GetSales()
        {
            var bills = GetAll().Where(e => e.BillType.Type == BillsType.Sales);
            return bills;
        }

        public IQueryable<Bill> GetSalesReturn()
        {
            var bills = GetAll().Where(e => e.BillType.Type == BillsType.SalesReturn);
            return bills;
        }

        public IQueryable<Bill> GetTransfer()
        {
            var bills = GetAll().Where(e => e.BillType.Type == BillsType.Transfer);
            return bills;
        }

        public long GetNextNumber(Guid billTypeId)
        {
            var count = Count();
            if (count == 0)
            {
                return 1;
            }
            var maxNumber = NativeGetAllNoTracking().Max(e => e.Number);
            return maxNumber + 1;
        }

        public async Task<long> GetNextNumberAsync(Guid billTypeId)
        {
            var count = await CountAsync();
            if (count == 0)
            {
                return 1;
            }
            var maxNumber = await NativeGetAllNoTracking().MaxAsync(e => e.Number);
            return maxNumber + 1;
        }

        public bool CheckIfExist(long number, Guid billTypeId)
        {
            var count = GetAll().Count(e => e.Number == number && e.BillTypeId == billTypeId);
            return count > 0;
        }

        public async Task<bool> CheckIfExistAsync(long number, Guid billTypeId)
        {
            var count = await GetAll().CountAsync(e => e.Number == number && e.BillTypeId == billTypeId);
            return count > 0;
        }

        /// <summary>
        /// don't use it
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public new Bill Get(long number)
        {
            throw new Exception("you can't use this method for bills, you should to use it with billTypeId param");
        }

        /// <summary>
        /// don't use it
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public new Task<Bill> GetAsync(long number)
        {
            throw new Exception("you can't use this method for bills, you should to use it with billTypeId param");
        }

        public Bill Get(Guid billTypeId, long number)
        {
            var entity = GetAll().FirstOrDefault(e => e.BillTypeId == billTypeId && e.Number == number);
            return entity;
        }

        public async Task<Bill> GetAsync(Guid billTypeId, long number)
        {
            var entity = await GetAll().FirstOrDefaultAsync(e => e.BillTypeId == billTypeId && e.Number == number);
            return entity;
        }

        public override void SetCreateProps(Bill bill)
        {
            base.SetCreateProps(bill);
            bill.FinancialPeriodId = PerdioId;
        }
    }
}
