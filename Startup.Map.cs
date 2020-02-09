using ERPAPI.Models;
using ERPAPI.ViewModels.Accounts;
using ERPAPI.ViewModels.Bills;
using ERPAPI.ViewModels.BillTypes;
using ERPAPI.ViewModels.Branchs;
using ERPAPI.ViewModels.CostCenters;
using ERPAPI.ViewModels.Currencies;
using ERPAPI.ViewModels.Customers;
using ERPAPI.ViewModels.CustomerTypes;
using ERPAPI.ViewModels.ItemGroups;
using ERPAPI.ViewModels.Items;
using ERPAPI.ViewModels.Prices;
using ERPAPI.ViewModels.Sellers;
using ERPAPI.ViewModels.Stores;
using ERPAPI.ViewModels.Units;
using ERPAPI.ViewModels.Companies;
using ERPAPI.ViewModels.Entries;
using ERPAPI.ViewModels.FinancialPeriods;
using ERPAPI.ViewModels.PayTypes;
using ERPAPI.ViewModels;

namespace ERPAPI
{
    public partial class Startup
    {
        private void MapAutoMapper()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<CostCenter, CostCenterViewModel>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                   .ForMember(dest => dest.ParentCostCenterId, opt => opt.MapFrom(src => src.ParentCostCenter.Number))
                   ;

                config.CreateMap<ItemGroup, ItemGroupViewModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                    .ForMember(dest => dest.ParentItemGroupId, opt => opt.MapFrom(src => src.ParentItemGroup.Number))
                    ;

                config.CreateMap<Item, ItemViewModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                    .ForMember(dest => dest.DefaultUnitId, opt => opt.MapFrom(src => src.DefaultUnit.Unit.Number))
                    .ForMember(dest => dest.DefaultUnitBarCode, opt => opt.MapFrom(src => src.DefaultUnit.Code))
                    .ForMember(dest => dest.ItemGroupId, opt => opt.MapFrom(src => src.ItemGroup.Number))
                    .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.ItemUnits))
                    ;

                config.CreateMap<Customer, SearchViewModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.NumberFullName))
                    .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name))
                    ;

                config.CreateMap<Account, SearchViewModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                    .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.CodeName))
                    ;

                config.CreateMap<Store, SearchViewModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                    .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.CodeName))
                    ;

                config.CreateMap<CostCenter, SearchViewModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                    .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.CodeName))
                    ;

                config.CreateMap<ItemGroup, SearchViewModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                    .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.CodeName))
                    ;

                config.CreateMap<Unit, SearchViewModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                    .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name))
                    ;

                config.CreateMap<Branch, SearchViewModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                    .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.CodeName))
                    ;

                config.CreateMap<ItemUnit, ItemUnitViewModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Unit.Number))
                    .ForMember(dest => dest.BarCode, opt => opt.MapFrom(src => src.Code))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Unit.Name))
                    ;

                config.CreateMap<Unit, UnitViewModel>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                   ;

                config.CreateMap<Store, StoreViewModel>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                   .ForMember(dest => dest.ParentStoreId, opt => opt.MapFrom(src => src.ParentStore.Number))
                   .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Account.Number))
                   .ForMember(dest => dest.CostCenterId, opt => opt.MapFrom(src => src.CostCenter.Number))
                   ;

                config.CreateMap<Branch, BranchViewModel>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                   .ForMember(dest => dest.ParentBranchId, opt => opt.MapFrom(src => src.ParentBranch.Number))
                   ;

                config.CreateMap<Account, AccountViewModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                    .ForMember(dest => dest.ParentAccountId, opt => opt.MapFrom(src => src.ParentAccount.Number))
                    .ForMember(dest => dest.FinalAccountId, opt => opt.MapFrom(src => src.FinalAccount.Number))
                    .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.Currency.Number))
                    ;

                config.CreateMap<BillType, BillTypeViewModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                    ;

                config.CreateMap<AddBillTypeViewModel, BillType>();

                config.CreateMap<EditBillTypeViewModel, BillType>()
                        .ForMember(x => x.Id, opt => opt.Ignore());
                ;

                config.CreateMap<CustomerType, CustomerTypeViewModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                    ;

                config.CreateMap<Customer, CustomerViewModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                    ;

                config.CreateMap<Bill, BillViewModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                    .ForMember(dest => dest.StoreId, opt => opt.MapFrom(src => src.Store.Number))
                    .ForMember(dest => dest.CustomerAccountId, opt => opt.MapFrom(src => src.CustomerAccount.Number))
                    .ForMember(dest => dest.CustomerAccountName, opt => opt.MapFrom(src => src.CustomerAccountId == null ? src.CustomerName : src.CustomerAccount.Name))
                    .ForMember(dest => dest.CostCenterId, opt => opt.MapFrom(src => src.CostCenter.Number))
                    .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.Branch.Number))
                    .ForMember(dest => dest.BillTypeId, opt => opt.MapFrom(src => src.BillType.Number))
                    .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Account.Number))
                    .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.Currency.Number))
                    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.BillItems))
                    ;

                config.CreateMap<BillItem, BillItemViewModel>()
                   .ForMember(dest => dest.StoreId, opt => opt.MapFrom(src => src.Store.Number))
                   .ForMember(dest => dest.CostCenterId, opt => opt.MapFrom(src => src.CostCenter.Number))
                   .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ItemUnit.Item.Number))
                   .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.ItemUnit.Item.Name))
                   .ForMember(dest => dest.ItemCode, opt => opt.MapFrom(src => src.ItemUnit.Item.Code))
                   .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => src.ItemUnit.Unit.Number))
                   .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.ItemUnit.Unit.Name))
                   ;

                config.CreateMap<Price, PriceViewModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                    ;

                config.CreateMap<Seller, SellerViewModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                    .ForMember(dest => dest.ParentSellerId, opt => opt.MapFrom(src => src.ParentSeller.Number))
                    ;
                
                config.CreateMap<EditCurrencyViewModel, Currency>()
                    .ForMember(x => x.Id, opt => opt.Ignore());
                ;

                config.CreateMap<EditSellerViewModel, Seller>()
                    .ForMember(x => x.Id, opt => opt.Ignore());
                ;

                config.CreateMap<Company, CompanyViewModel>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                   ;

                config.CreateMap<FinancialPeriod, FinancialPeriodViewModel>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                   .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Company.Number))
                   ;

                config.CreateMap<Entry, EntryViewModel>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
                   .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.Currency.Number))
                   .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.Branch.Number))
                ;

                config.CreateMap<EntryItem, EntryItemViewModel>()
                   .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Account.Number))
                   .ForMember(dest => dest.CostCenterId, opt => opt.MapFrom(src => src.CostCenter.Number))
                   .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(src => src.Currency.Number))
                ;

                config.CreateMap<PayType, PayTypeViewModel>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Number))
               ;
                config.CreateMap<AddPayTypeViewModel, PayType>()
                 .ForMember(x => x.Id, opt => opt.Ignore())
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                ;

            });
        }
    }
}
