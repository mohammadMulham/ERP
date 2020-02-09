using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ERPAPI.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Accounting");

            migrationBuilder.EnsureSchema(
                name: "CMS");

            migrationBuilder.EnsureSchema(
                name: "HumanResources");

            migrationBuilder.EnsureSchema(
                name: "Management");

            migrationBuilder.EnsureSchema(
                name: "Person");

            migrationBuilder.EnsureSchema(
                name: "Marketing");

            migrationBuilder.EnsureSchema(
                name: "Production");

            migrationBuilder.CreateTable(
                name: "BillTypes",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AutoGenerateEntry = table.Column<bool>(nullable: false),
                    AutoPostEntryToAccounts = table.Column<bool>(nullable: false),
                    AutoPostToStores = table.Column<bool>(nullable: false),
                    BillInOut = table.Column<short>(nullable: false),
                    BillTypeOrder = table.Column<int>(nullable: false),
                    CanEditItemPrice = table.Column<bool>(nullable: false),
                    CanEditItemTotalPrice = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(maxLength: 128, nullable: false),
                    Color1 = table.Column<int>(nullable: true),
                    Color2 = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    DefaultBranchId = table.Column<Guid>(nullable: true),
                    DefaultCashAccountId = table.Column<Guid>(nullable: true),
                    DefaultCostCenterId = table.Column<Guid>(nullable: true),
                    DefaultCurrencyId = table.Column<Guid>(nullable: true),
                    DefaultDiscAccountId = table.Column<Guid>(nullable: true),
                    DefaultExtraAccountId = table.Column<Guid>(nullable: true),
                    DefaultItemsAccountId = table.Column<Guid>(nullable: true),
                    DefaultPayType = table.Column<int>(nullable: false),
                    DefaultPriceId = table.Column<Guid>(nullable: true),
                    DefaultStoreId = table.Column<Guid>(nullable: true),
                    EntityStatus = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    NoPostToStores = table.Column<bool>(nullable: false),
                    NotGenerateEntry = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Number = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PreventNegativeOutput = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ShowBranchField = table.Column<bool>(nullable: false),
                    ShowCostCenterldField = table.Column<bool>(nullable: false),
                    ShowCustomerAccountldField = table.Column<bool>(nullable: false),
                    ShowDiscField = table.Column<bool>(nullable: false),
                    ShowExtraField = table.Column<bool>(nullable: false),
                    ShowItemCostCenterldField = table.Column<bool>(nullable: false),
                    ShowItemDiscField = table.Column<bool>(nullable: false),
                    ShowItemExpireDateField = table.Column<bool>(nullable: false),
                    ShowItemExtraField = table.Column<bool>(nullable: false),
                    ShowItemNoteField = table.Column<bool>(nullable: false),
                    ShowItemPriceFields = table.Column<bool>(nullable: false),
                    ShowItemProductionDateField = table.Column<bool>(nullable: false),
                    ShowItemStoreField = table.Column<bool>(nullable: false),
                    ShowItemUnitField = table.Column<bool>(nullable: false),
                    ShowNoteField = table.Column<bool>(nullable: false),
                    ShowPayTypeField = table.Column<bool>(nullable: false),
                    ShowSellersFields = table.Column<bool>(nullable: false),
                    ShowStoreField = table.Column<bool>(nullable: false),
                    ShowTotalPriceItemField = table.Column<bool>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CostCenters",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Number = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentId = table.Column<Guid>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCenters_CostCenters_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Accounting",
                        principalTable: "CostCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    ISOCode = table.Column<string>(maxLength: 128, nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Number = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PartName = table.Column<string>(maxLength: 128, nullable: true),
                    PartRate = table.Column<double>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayTypes",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AutoPostToAccounts = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(maxLength: 128, nullable: false),
                    Color1 = table.Column<int>(nullable: true),
                    Color2 = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    CreditFieldName = table.Column<string>(nullable: true),
                    DebitFieldName = table.Column<string>(nullable: true),
                    DefaultAccountId = table.Column<Guid>(nullable: true),
                    DefaultBranchId = table.Column<Guid>(nullable: true),
                    DefaultCostCenterId = table.Column<Guid>(nullable: true),
                    DefaultCurrencyId = table.Column<Guid>(nullable: true),
                    EntityStatus = table.Column<int>(nullable: false),
                    IsBeginEntry = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Number = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PayTypeOrder = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ShowBranchField = table.Column<bool>(nullable: false),
                    ShowCostCenterldField = table.Column<bool>(nullable: false),
                    ShowItemCostCenterldField = table.Column<bool>(nullable: false),
                    ShowItemCreditField = table.Column<bool>(nullable: false),
                    ShowItemCurrencyField = table.Column<bool>(nullable: false),
                    ShowItemDebitField = table.Column<bool>(nullable: false),
                    ShowItemNoteField = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sellers",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Number = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentId = table.Column<Guid>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sellers_Sellers_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Accounting",
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuthClients",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    AllowedOrigin = table.Column<string>(maxLength: 100, nullable: true),
                    ApplicationType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    RefreshTokenLifeTime = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Secret = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthClients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthRefreshTokens",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<string>(nullable: true),
                    Expires = table.Column<DateTimeOffset>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Issued = table.Column<DateTimeOffset>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Subject = table.Column<string>(maxLength: 256, nullable: false),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthRefreshTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionGroups",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Number = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TableId = table.Column<Guid>(nullable: false),
                    TableType = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branchs",
                schema: "HumanResources",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Number = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentId = table.Column<Guid>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branchs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branchs_Branchs_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "HumanResources",
                        principalTable: "Branchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "Management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Number = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                schema: "Management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    DeletedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    DeletedUserId = table.Column<Guid>(nullable: true),
                    EntityStatus = table.Column<int>(nullable: false),
                    ModifiedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedUserId = table.Column<Guid>(nullable: true),
                    Object = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TableId = table.Column<Guid>(nullable: false),
                    TableType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTypes",
                schema: "Marketing",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AllowEditCustomerAccountName = table.Column<bool>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    DefaultCustomerPriceId = table.Column<Guid>(nullable: true),
                    DefaultParentAccountId = table.Column<Guid>(nullable: true),
                    DefaultWithCreateAccount = table.Column<bool>(nullable: false),
                    DefaultWithCreateContact = table.Column<bool>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    GenerateCustomerAccountNameAutomatically = table.Column<bool>(nullable: false),
                    GenerateCustomerAccountNameWithCityName = table.Column<bool>(nullable: false),
                    GenerateCustomerAccountNameWithCustomerTypeName = table.Column<bool>(nullable: false),
                    GenerateCustomerAccountNameWithNeighborhoodName = table.Column<bool>(nullable: false),
                    GenerateCustomerAccountNameWithProvinceName = table.Column<bool>(nullable: false),
                    GenerateCustomerAccountNameWithStreetName = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Number = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PreventRepeatCustomer = table.Column<bool>(nullable: false),
                    PreventRepeatCustomerContact = table.Column<bool>(nullable: false),
                    PreventRepeatCustomerInAreaType = table.Column<short>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderTypes",
                schema: "Marketing",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    DefaultBranchId = table.Column<Guid>(nullable: true),
                    DefaultStoreId = table.Column<Guid>(nullable: true),
                    EntityStatus = table.Column<int>(nullable: false),
                    IsCurrentUserMarketer = table.Column<bool>(nullable: false),
                    MarketersNumber = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    NoEditFirstLastCustomerOrderDate = table.Column<bool>(nullable: false),
                    Number = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderTypeOrder = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ShowBranshField = table.Column<bool>(nullable: false),
                    ShowStoreField = table.Column<bool>(nullable: false),
                    WithAutoPostToStore = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactTypes",
                schema: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Number = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Prefix = table.Column<string>(maxLength: 4, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemGroups",
                schema: "Production",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Number = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentId = table.Column<Guid>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemGroups_ItemGroups_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Production",
                        principalTable: "ItemGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                schema: "Production",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Number = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                schema: "Production",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Number = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyHistories",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CurrencyId = table.Column<Guid>(nullable: false),
                    CurrencyValue = table.Column<double>(nullable: false),
                    EndtDate = table.Column<DateTimeOffset>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    StartDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyHistories_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "Accounting",
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionGroupPermissions",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PermissionGroupId = table.Column<Guid>(nullable: false),
                    PermissionId = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionGroupPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionGroupPermissions_PermissionGroups_PermissionGroupId",
                        column: x => x.PermissionGroupId,
                        principalSchema: "CMS",
                        principalTable: "PermissionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissionGroupPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "CMS",
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionGroupRoles",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PermissionGroupId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionGroupRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionGroupRoles_PermissionGroups_PermissionGroupId",
                        column: x => x.PermissionGroupId,
                        principalSchema: "CMS",
                        principalTable: "PermissionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissionGroupRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "CMS",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "CMS",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "CMS",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "CMS",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "CMS",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "CMS",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "CMS",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "CMS",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "CMS",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "CMS",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinancialPeriods",
                schema: "Management",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    EndtDate = table.Column<DateTimeOffset>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Number = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Order = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    StartDate = table.Column<DateTimeOffset>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialPeriods_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Management",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "Marketing",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    CustomerTypeId = table.Column<Guid>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Number = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    VIP = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_CustomerTypes_CustomerTypeId",
                        column: x => x.CustomerTypeId,
                        principalSchema: "Marketing",
                        principalTable: "CustomerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                schema: "Production",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    ItemGroupId = table.Column<Guid>(nullable: false),
                    MaxLimit = table.Column<double>(nullable: true),
                    MinLimit = table.Column<double>(nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Number = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReOrderLimit = table.Column<double>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_ItemGroups_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalSchema: "Production",
                        principalTable: "ItemGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entries",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    CurrencyId = table.Column<Guid>(nullable: false),
                    CurrencyValue = table.Column<double>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    EntryOrigin = table.Column<int>(nullable: false),
                    FinancialPeriodId = table.Column<Guid>(nullable: false),
                    IsPosted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Number = table.Column<long>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entries_Branchs_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "HumanResources",
                        principalTable: "Branchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entries_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "Accounting",
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entries_FinancialPeriods_FinancialPeriodId",
                        column: x => x.FinancialPeriodId,
                        principalSchema: "Management",
                        principalTable: "FinancialPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountType = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    CurrencyId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: true),
                    DirectionType = table.Column<int>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    FinalAccountId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Number = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentId = table.Column<Guid>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "Accounting",
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Marketing",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Accounts_FinalAccountId",
                        column: x => x.FinalAccountId,
                        principalSchema: "Accounting",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Accounts_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Accounting",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContacts",
                schema: "Marketing",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Adjective = table.Column<string>(maxLength: 128, nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    ContactTypeId = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 128, nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(maxLength: 128, nullable: true),
                    Number = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerContacts_ContactTypes_ContactTypeId",
                        column: x => x.ContactTypeId,
                        principalSchema: "Person",
                        principalTable: "ContactTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerContacts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Marketing",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemUnits",
                schema: "Production",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 128, nullable: true),
                    Factor = table.Column<double>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UnitId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemUnits_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Production",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemUnits_Units_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "Production",
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountBalances",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false),
                    Credit = table.Column<double>(nullable: false),
                    Debit = table.Column<double>(nullable: false),
                    FinancialPeriodId = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountBalances_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Accounting",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountBalances_FinancialPeriods_FinancialPeriodId",
                        column: x => x.FinancialPeriodId,
                        principalSchema: "Management",
                        principalTable: "FinancialPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntryItems",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false),
                    CostCenterId = table.Column<Guid>(nullable: true),
                    Credit = table.Column<double>(nullable: false),
                    CurrencyId = table.Column<Guid>(nullable: false),
                    CurrencyValue = table.Column<double>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Debit = table.Column<double>(nullable: false),
                    EntryId = table.Column<Guid>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryItems_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Accounting",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntryItems_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "Accounting",
                        principalTable: "CostCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntryItems_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "Accounting",
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntryItems_Entries_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "Accounting",
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayEntries",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    EntryId = table.Column<Guid>(nullable: false),
                    Number = table.Column<long>(nullable: false),
                    PayAccountId = table.Column<Guid>(nullable: true),
                    PayTypeId = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayEntries_Entries_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "Accounting",
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayEntries_Accounts_PayAccountId",
                        column: x => x.PayAccountId,
                        principalSchema: "Accounting",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayEntries_PayTypes_PayTypeId",
                        column: x => x.PayTypeId,
                        principalSchema: "Accounting",
                        principalTable: "PayTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                schema: "Production",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: true),
                    Code = table.Column<string>(maxLength: 128, nullable: false),
                    CostCenterId = table.Column<Guid>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Number = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentId = table.Column<Guid>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stores_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Accounting",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stores_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "Accounting",
                        principalTable: "CostCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stores_Stores_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Production",
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemUnitPrices",
                schema: "Production",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    ItemUnitId = table.Column<Guid>(nullable: false),
                    PriceId = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemUnitPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemUnitPrices_ItemUnits_ItemUnitId",
                        column: x => x.ItemUnitId,
                        principalSchema: "Production",
                        principalTable: "ItemUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemUnitPrices_Prices_PriceId",
                        column: x => x.PriceId,
                        principalSchema: "Production",
                        principalTable: "Prices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: true),
                    BillTypeId = table.Column<Guid>(nullable: false),
                    BranchId = table.Column<Guid>(nullable: true),
                    CostCenterId = table.Column<Guid>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    CurrencyId = table.Column<Guid>(nullable: false),
                    CurrencyValue = table.Column<double>(nullable: false),
                    CustomerAccountId = table.Column<Guid>(nullable: true),
                    CustomerName = table.Column<string>(maxLength: 128, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Disc = table.Column<double>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    Extra = table.Column<double>(nullable: false),
                    FinancialPeriodId = table.Column<Guid>(nullable: false),
                    IsEntryGenerated = table.Column<bool>(nullable: false),
                    IsPosted = table.Column<bool>(nullable: false),
                    IsPrinted = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Number = table.Column<long>(nullable: false),
                    PayType = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    StoreId = table.Column<Guid>(nullable: true),
                    Total = table.Column<double>(nullable: false),
                    TotalPaid = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Accounting",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_BillTypes_BillTypeId",
                        column: x => x.BillTypeId,
                        principalSchema: "Accounting",
                        principalTable: "BillTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_Branchs_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "HumanResources",
                        principalTable: "Branchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "Accounting",
                        principalTable: "CostCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "Accounting",
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_Accounts_CustomerAccountId",
                        column: x => x.CustomerAccountId,
                        principalSchema: "Accounting",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_FinancialPeriods_FinancialPeriodId",
                        column: x => x.FinancialPeriodId,
                        principalSchema: "Management",
                        principalTable: "FinancialPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_Stores_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Production",
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "Marketing",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BranchId = table.Column<Guid>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    DeliveryDate = table.Column<DateTimeOffset>(nullable: true),
                    DueDate = table.Column<DateTimeOffset>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    FinancialPeriodId = table.Column<Guid>(nullable: false),
                    Number = table.Column<long>(nullable: false),
                    OrderTypeId = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    StoreId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Branchs_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "HumanResources",
                        principalTable: "Branchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Marketing",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_FinancialPeriods_FinancialPeriodId",
                        column: x => x.FinancialPeriodId,
                        principalSchema: "Management",
                        principalTable: "FinancialPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_OrderTypes_OrderTypeId",
                        column: x => x.OrderTypeId,
                        principalSchema: "Marketing",
                        principalTable: "OrderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Stores_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Production",
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreItems",
                schema: "Production",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EndDate = table.Column<DateTimeOffset>(nullable: true),
                    FinancialPeriodId = table.Column<Guid>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false),
                    MaxLimit = table.Column<double>(nullable: true),
                    MinLimit = table.Column<double>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    ReOrderLimit = table.Column<double>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    StartDate = table.Column<DateTimeOffset>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreItems_FinancialPeriods_FinancialPeriodId",
                        column: x => x.FinancialPeriodId,
                        principalSchema: "Management",
                        principalTable: "FinancialPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Production",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreItems_Stores_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Production",
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreItemUnits",
                schema: "Production",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EndDate = table.Column<DateTimeOffset>(nullable: true),
                    FinancialPeriodId = table.Column<Guid>(nullable: false),
                    ItemUnitId = table.Column<Guid>(nullable: false),
                    MaxLimit = table.Column<double>(nullable: true),
                    MinLimit = table.Column<double>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    ReOrderLimit = table.Column<double>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    StartDate = table.Column<DateTimeOffset>(nullable: false),
                    StoreId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreItemUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreItemUnits_FinancialPeriods_FinancialPeriodId",
                        column: x => x.FinancialPeriodId,
                        principalSchema: "Management",
                        principalTable: "FinancialPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreItemUnits_ItemUnits_ItemUnitId",
                        column: x => x.ItemUnitId,
                        principalSchema: "Production",
                        principalTable: "ItemUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreItemUnits_Stores_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Production",
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillEntries",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BillId = table.Column<Guid>(nullable: false),
                    EntryId = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillEntries_Bills_BillId",
                        column: x => x.BillId,
                        principalSchema: "Accounting",
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillEntries_Entries_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "Accounting",
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillEntryItems",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false),
                    BillId = table.Column<Guid>(nullable: false),
                    CostCenterId = table.Column<Guid>(nullable: true),
                    Credit = table.Column<double>(nullable: false),
                    CurrencyId = table.Column<Guid>(nullable: false),
                    CurrencyValue = table.Column<double>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Debit = table.Column<double>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillEntryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillEntryItems_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Accounting",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillEntryItems_Bills_BillId",
                        column: x => x.BillId,
                        principalSchema: "Accounting",
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillEntryItems_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "Accounting",
                        principalTable: "CostCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillEntryItems_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "Accounting",
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillItems",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BillId = table.Column<Guid>(nullable: false),
                    CostCenterId = table.Column<Guid>(nullable: true),
                    Disc = table.Column<double>(nullable: false),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    Extra = table.Column<double>(nullable: false),
                    ItemUnitId = table.Column<Guid>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    ProductionDate = table.Column<DateTime>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    StoreId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillItems_Bills_BillId",
                        column: x => x.BillId,
                        principalSchema: "Accounting",
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillItems_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "Accounting",
                        principalTable: "CostCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillItems_ItemUnits_ItemUnitId",
                        column: x => x.ItemUnitId,
                        principalSchema: "Production",
                        principalTable: "ItemUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillItems_Stores_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Production",
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillSellers",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BillId = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SellerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillSellers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillSellers_Bills_BillId",
                        column: x => x.BillId,
                        principalSchema: "Accounting",
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillSellers_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalSchema: "Accounting",
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                schema: "Marketing",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BillItemId = table.Column<Guid>(nullable: true),
                    ItemUnitId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_BillItems_BillItemId",
                        column: x => x.BillItemId,
                        principalSchema: "Accounting",
                        principalTable: "BillItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_ItemUnits_ItemUnitId",
                        column: x => x.ItemUnitId,
                        principalSchema: "Production",
                        principalTable: "ItemUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Marketing",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountBalances_AccountId",
                schema: "Accounting",
                table: "AccountBalances",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountBalances_FinancialPeriodId",
                schema: "Accounting",
                table: "AccountBalances",
                column: "FinancialPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CurrencyId",
                schema: "Accounting",
                table: "Accounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerId",
                schema: "Accounting",
                table: "Accounts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_FinalAccountId",
                schema: "Accounting",
                table: "Accounts",
                column: "FinalAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ParentId",
                schema: "Accounting",
                table: "Accounts",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_BillEntries_BillId",
                schema: "Accounting",
                table: "BillEntries",
                column: "BillId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillEntries_EntryId",
                schema: "Accounting",
                table: "BillEntries",
                column: "EntryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillEntryItems_AccountId",
                schema: "Accounting",
                table: "BillEntryItems",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BillEntryItems_BillId",
                schema: "Accounting",
                table: "BillEntryItems",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillEntryItems_CostCenterId",
                schema: "Accounting",
                table: "BillEntryItems",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_BillEntryItems_CurrencyId",
                schema: "Accounting",
                table: "BillEntryItems",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_CostCenterId",
                schema: "Accounting",
                table: "BillItems",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_ItemUnitId",
                schema: "Accounting",
                table: "BillItems",
                column: "ItemUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_StoreId",
                schema: "Accounting",
                table: "BillItems",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_BillId_ItemUnitId_StoreId",
                schema: "Accounting",
                table: "BillItems",
                columns: new[] { "BillId", "ItemUnitId", "StoreId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_AccountId",
                schema: "Accounting",
                table: "Bills",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BranchId",
                schema: "Accounting",
                table: "Bills",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CostCenterId",
                schema: "Accounting",
                table: "Bills",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CurrencyId",
                schema: "Accounting",
                table: "Bills",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CustomerAccountId",
                schema: "Accounting",
                table: "Bills",
                column: "CustomerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_FinancialPeriodId",
                schema: "Accounting",
                table: "Bills",
                column: "FinancialPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_StoreId",
                schema: "Accounting",
                table: "Bills",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BillTypeId_Number_FinancialPeriodId",
                schema: "Accounting",
                table: "Bills",
                columns: new[] { "BillTypeId", "Number", "FinancialPeriodId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillSellers_SellerId",
                schema: "Accounting",
                table: "BillSellers",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_BillSellers_BillId_SellerId",
                schema: "Accounting",
                table: "BillSellers",
                columns: new[] { "BillId", "SellerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CostCenters_ParentId",
                schema: "Accounting",
                table: "CostCenters",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyHistories_CurrencyId",
                schema: "Accounting",
                table: "CurrencyHistories",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_BranchId",
                schema: "Accounting",
                table: "Entries",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_CurrencyId",
                schema: "Accounting",
                table: "Entries",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_FinancialPeriodId",
                schema: "Accounting",
                table: "Entries",
                column: "FinancialPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_Number_FinancialPeriodId",
                schema: "Accounting",
                table: "Entries",
                columns: new[] { "Number", "FinancialPeriodId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntryItems_AccountId",
                schema: "Accounting",
                table: "EntryItems",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryItems_CostCenterId",
                schema: "Accounting",
                table: "EntryItems",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryItems_CurrencyId",
                schema: "Accounting",
                table: "EntryItems",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryItems_EntryId",
                schema: "Accounting",
                table: "EntryItems",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_PayEntries_EntryId",
                schema: "Accounting",
                table: "PayEntries",
                column: "EntryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PayEntries_PayAccountId",
                schema: "Accounting",
                table: "PayEntries",
                column: "PayAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PayEntries_PayTypeId",
                schema: "Accounting",
                table: "PayEntries",
                column: "PayTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_ParentId",
                schema: "Accounting",
                table: "Sellers",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroupPermissions_PermissionId",
                schema: "CMS",
                table: "PermissionGroupPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroupPermissions_PermissionGroupId_PermissionId",
                schema: "CMS",
                table: "PermissionGroupPermissions",
                columns: new[] { "PermissionGroupId", "PermissionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroupRoles_RoleId",
                schema: "CMS",
                table: "PermissionGroupRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroupRoles_PermissionGroupId_RoleId",
                schema: "CMS",
                table: "PermissionGroupRoles",
                columns: new[] { "PermissionGroupId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "CMS",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "CMS",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "CMS",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "CMS",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "CMS",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "CMS",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "CMS",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Branchs_ParentId",
                schema: "HumanResources",
                table: "Branchs",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialPeriods_CompanyId",
                schema: "Management",
                table: "FinancialPeriods",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_ContactTypeId",
                schema: "Marketing",
                table: "CustomerContacts",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_CustomerId",
                schema: "Marketing",
                table: "CustomerContacts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerTypeId",
                schema: "Marketing",
                table: "Customers",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Name",
                schema: "Marketing",
                table: "Customers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTypes_Name",
                schema: "Marketing",
                table: "CustomerTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_BillItemId",
                schema: "Marketing",
                table: "OrderItems",
                column: "BillItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ItemUnitId",
                schema: "Marketing",
                table: "OrderItems",
                column: "ItemUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId_ItemUnitId",
                schema: "Marketing",
                table: "OrderItems",
                columns: new[] { "OrderId", "ItemUnitId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BranchId",
                schema: "Marketing",
                table: "Orders",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                schema: "Marketing",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FinancialPeriodId",
                schema: "Marketing",
                table: "Orders",
                column: "FinancialPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderTypeId",
                schema: "Marketing",
                table: "Orders",
                column: "OrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StoreId",
                schema: "Marketing",
                table: "Orders",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGroups_ParentId",
                schema: "Production",
                table: "ItemGroups",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemGroupId",
                schema: "Production",
                table: "Items",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitPrices_PriceId",
                schema: "Production",
                table: "ItemUnitPrices",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitPrices_ItemUnitId_PriceId",
                schema: "Production",
                table: "ItemUnitPrices",
                columns: new[] { "ItemUnitId", "PriceId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnits_UnitId",
                schema: "Production",
                table: "ItemUnits",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnits_ItemId_UnitId",
                schema: "Production",
                table: "ItemUnits",
                columns: new[] { "ItemId", "UnitId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreItems_FinancialPeriodId",
                schema: "Production",
                table: "StoreItems",
                column: "FinancialPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItems_ItemId",
                schema: "Production",
                table: "StoreItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItems_StoreId_ItemId_FinancialPeriodId",
                schema: "Production",
                table: "StoreItems",
                columns: new[] { "StoreId", "ItemId", "FinancialPeriodId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemUnits_FinancialPeriodId",
                schema: "Production",
                table: "StoreItemUnits",
                column: "FinancialPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemUnits_ItemUnitId",
                schema: "Production",
                table: "StoreItemUnits",
                column: "ItemUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemUnits_StoreId_ItemUnitId_FinancialPeriodId",
                schema: "Production",
                table: "StoreItemUnits",
                columns: new[] { "StoreId", "ItemUnitId", "FinancialPeriodId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stores_AccountId",
                schema: "Production",
                table: "Stores",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_CostCenterId",
                schema: "Production",
                table: "Stores",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_ParentId",
                schema: "Production",
                table: "Stores",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountBalances",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "BillEntries",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "BillEntryItems",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "BillSellers",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "CurrencyHistories",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "EntryItems",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "PayEntries",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "AuthClients",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "AuthRefreshTokens",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "PermissionGroupPermissions",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "PermissionGroupRoles",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "Logs",
                schema: "Management");

            migrationBuilder.DropTable(
                name: "CustomerContacts",
                schema: "Marketing");

            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "Marketing");

            migrationBuilder.DropTable(
                name: "ItemUnitPrices",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "StoreItems",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "StoreItemUnits",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Sellers",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "Entries",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "PayTypes",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "PermissionGroups",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "ContactTypes",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "BillItems",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "Marketing");

            migrationBuilder.DropTable(
                name: "Prices",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Bills",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "ItemUnits",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "OrderTypes",
                schema: "Marketing");

            migrationBuilder.DropTable(
                name: "BillTypes",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "Branchs",
                schema: "HumanResources");

            migrationBuilder.DropTable(
                name: "FinancialPeriods",
                schema: "Management");

            migrationBuilder.DropTable(
                name: "Stores",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Items",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Units",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "Management");

            migrationBuilder.DropTable(
                name: "Accounts",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "CostCenters",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "ItemGroups",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Currencies",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "Marketing");

            migrationBuilder.DropTable(
                name: "CustomerTypes",
                schema: "Marketing");
        }
    }
}
