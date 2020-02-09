using ERPAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ERPAPI.Data
{
    public class ERPContext : IdentityDbContext<User, Role, Guid>
    {
        public ERPContext(DbContextOptions<ERPContext> options) : base(options)
        {
        }

        #region tables
        public DbSet<AuthClient> AuthClients { get; set; }
        public DbSet<AuthRefreshToken> AuthRefreshTokens { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<BillType> BillTypes { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillItem> BillItems { get; set; }
        public DbSet<BillEntryItem> BillEntryItems { get; set; }
        public DbSet<BillSeller> BillSellers { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<CostCenter> CostCenters { get; set; }
        public DbSet<CustomerContact> CustomerContacts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemGroup> ItemGroups { get; set; }
        public DbSet<ItemUnit> ItemUnits { get; set; }
        public DbSet<ItemUnitPrice> ItemUnitPrices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreItem> StoreItems { get; set; }
        public DbSet<StoreItemUnit> StoreItemUnits { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionGroup> PermissionGroups { get; set; }
        public DbSet<PermissionGroupPermisson> PermissionGroupPermissons { get; set; }
        public DbSet<PermissionGroupRole> PermissionGroupRoles { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<EntryItem> EntryItems { get; set; }
        public DbSet<PayType> PayTypes { get; set; }
        public DbSet<PayEntry> PayEntries { get; set; }
        public DbSet<BillEntry> BillEntries { get; set; }
        public DbSet<CurrencyHistory> CurrencyHistories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<FinancialPeriod> FinancialPeriods { get; set; }
        public DbSet<AccountBalance> AccountBalances { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            var identitySchema = "CMS";
            builder.Entity<Role>().ToTable("Roles", identitySchema);
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims", identitySchema);
            builder.Entity<User>().ToTable("Users", identitySchema);
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles", identitySchema);
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims", identitySchema);
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins", identitySchema);
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens", identitySchema);

            builder.Entity<Account>().Property(p => p.Number)
                .UseSqlServerIdentityColumn().Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore
                ;

            builder.Entity<BillType>().Property(p => p.Number)
                .UseSqlServerIdentityColumn().Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore
                ;

            builder.Entity<Item>().Property(p => p.Number)
                .UseSqlServerIdentityColumn().Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore
                ;

            builder.Entity<Unit>().Property(p => p.Number)
                .UseSqlServerIdentityColumn().Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore
                ;

            builder.Entity<Price>().Property(p => p.Number)
                .UseSqlServerIdentityColumn().Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore
                ;

            builder.Entity<Seller>().Property(p => p.Number)
                .UseSqlServerIdentityColumn().Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore
                ;

            builder.Entity<CostCenter>().Property(p => p.Number)
                .UseSqlServerIdentityColumn().Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore
                ;

            builder.Entity<Currency>().Property(p => p.Number)
                .UseSqlServerIdentityColumn().Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore
                ;

            builder.Entity<FinancialPeriod>()
                .HasMany(e => e.Bills)
                .WithOne(e => e.FinancialPeriod)
                .HasForeignKey(e => e.FinancialPeriodId);

            builder.Entity<FinancialPeriod>()
                .HasMany(e => e.Entries)
                .WithOne(e => e.FinancialPeriod)
                .HasForeignKey(e => e.FinancialPeriodId);

            builder.Entity<FinancialPeriod>()
                .HasMany(e => e.StoreItems)
                .WithOne(e => e.FinancialPeriod)
                .HasForeignKey(e => e.FinancialPeriodId);

            builder.Entity<FinancialPeriod>()
                .HasMany(e => e.StoreItemUnits)
                .WithOne(e => e.FinancialPeriod)
                .HasForeignKey(e => e.FinancialPeriodId);

            builder.Entity<Account>()
                .HasMany(e => e.AccountBills)
                .WithOne(e => e.Account)
                .HasForeignKey(e => e.AccountId);

            builder.Entity<Account>()
                .HasMany(e => e.CustomerBills)
                .WithOne(e => e.CustomerAccount)
                .HasForeignKey(e => e.CustomerAccountId);

            builder.Entity<Account>()
                .HasMany(e => e.FinalAccounts)
                .WithOne(e => e.FinalAccount)
                .HasForeignKey(e => e.FinalAccountId);

            builder.Entity<Account>()
                .HasMany(e => e.Accounts)
                .WithOne(e => e.ParentAccount)
                .HasForeignKey(e => e.ParentId);

            builder.Entity<Account>()
                .HasMany(e => e.AccountBalances)
                .WithOne(e => e.Account)
                .HasForeignKey(e => e.AccountId);

            builder.Entity<FinancialPeriod>()
                .HasMany(e => e.AccountBalances)
                .WithOne(e => e.FinancialPeriod)
                .HasForeignKey(e => e.FinancialPeriodId);

            builder.Entity<CostCenter>()
                .HasMany(e => e.CostCenters)
                .WithOne(e => e.ParentCostCenter)
                .HasForeignKey(e => e.ParentId);

            builder.Entity<Seller>()
                .HasMany(e => e.Sellers)
                .WithOne(e => e.ParentSeller)
                .HasForeignKey(e => e.ParentId);

            builder.Entity<Branch>()
                .HasMany(e => e.Branchs)
                .WithOne(e => e.ParentBranch)
                .HasForeignKey(e => e.ParentId);

            builder.Entity<ItemGroup>()
                .HasMany(e => e.Groups)
                .WithOne(e => e.ParentItemGroup)
                .HasForeignKey(e => e.ParentId);

            builder.Entity<Store>()
               .HasMany(e => e.Stores)
               .WithOne(e => e.ParentStore)
               .HasForeignKey(e => e.ParentId);

            builder.Entity<PayEntry>()
               .HasOne(e => e.Entry)
               .WithOne(e => e.PayEntry)
               .HasForeignKey<PayEntry>(e => e.EntryId);

            builder.Entity<BillEntry>()
               .HasIndex(p => new { p.BillId }).IsUnique();

            builder.Entity<BillEntry>()
               .HasOne(e => e.Entry)
               .WithOne(e => e.BillEntry)
               .HasForeignKey<BillEntry>(e => e.EntryId);

            builder.Entity<BillEntry>()
               .HasOne(e => e.Bill)
               .WithOne(e => e.BillEntry)
               .HasForeignKey<BillEntry>(e => e.BillId);

            builder.Entity<PermissionGroupPermisson>()
                .HasIndex(p => new { p.PermissionGroupId, p.PermissionId }).IsUnique();

            builder.Entity<PermissionGroupRole>()
                .HasIndex(p => new { p.PermissionGroupId, p.RoleId }).IsUnique();

            builder.Entity<ItemUnit>()
                .HasIndex(p => new { p.ItemId, p.UnitId }).IsUnique();

            builder.Entity<StoreItem>()
            .HasIndex(p => new { p.StoreId, p.ItemId, p.FinancialPeriodId }).IsUnique();

            builder.Entity<StoreItemUnit>()
                .HasIndex(p => new { p.StoreId, p.ItemUnitId, p.FinancialPeriodId }).IsUnique();

            builder.Entity<OrderItem>()
                .HasIndex(p => new { p.OrderId, p.ItemUnitId }).IsUnique();

            builder.Entity<ItemUnitPrice>()
                .HasIndex(p => new { p.ItemUnitId, p.PriceId }).IsUnique();

            builder.Entity<CustomerType>()
                .HasIndex(p => new { p.Name }).IsUnique();

            builder.Entity<Customer>()
                .HasIndex(p => new { p.Name }).IsUnique();

            builder.Entity<Entry>()
                .HasIndex(p => new { p.Number, p.FinancialPeriodId }).IsUnique();

            builder.Entity<Bill>()
                .HasIndex(p => new { p.BillTypeId, p.Number, p.FinancialPeriodId }).IsUnique();

            builder.Entity<BillItem>()
                .HasIndex(p => new { p.BillId, p.ItemUnitId, p.StoreId }).IsUnique();

            builder.Entity<BillSeller>()
               .HasIndex(p => new { p.BillId, p.SellerId }).IsUnique();

            builder.Entity<Item>()
              .HasMany(e => e.ItemUnits)
              .WithOne(e => e.Item)
              .HasForeignKey(e => e.ItemId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Bill>()
              .HasMany(e => e.BillItems)
              .WithOne(e => e.Bill)
              .HasForeignKey(e => e.BillId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Bill>()
              .HasMany(e => e.BillSellers)
              .WithOne(e => e.Bill)
              .HasForeignKey(e => e.BillId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Bill>()
              .HasMany(e => e.BillEntryItems)
              .WithOne(e => e.Bill)
              .HasForeignKey(e => e.BillId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Entry>()
              .HasMany(e => e.Items)
              .WithOne(e => e.Entry)
              .HasForeignKey(e => e.EntryId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Order>()
              .HasMany(e => e.OrderItems)
              .WithOne(e => e.Order)
              .HasForeignKey(e => e.OrderId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
