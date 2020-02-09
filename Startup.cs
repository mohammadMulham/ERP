using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using ERPAPI.Models;
using Microsoft.AspNetCore.Http;
using ERPAPI.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using ERPAPI.Managers;
using Swashbuckle.AspNetCore.Examples;
using ERPAPI.Filters;
using ERPAPI.Options;
using ERPAPI.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using ERPAPI.Factories;

namespace ERPAPI
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (Configuration["Database:InMemoryDatabase"].ToLower() == "true")
            {
                services.AddDbContext<ERPContext>(opt => opt.UseInMemoryDatabase("ERPDb"));
            }
            else
            {
                services.AddDbContext<ERPContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("ERPConnection"));
                });
            }

            // appsettings
            services.Configure<JwtOptions>(Configuration.GetSection("JWT"));
            services.ConfigureWritable<DefaultKeysOptions>(Configuration.GetSection("DefaultKeys"));

            // mvc services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region business services
            services.AddScoped<IItemGroupRepository, ItemGroupRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IFinancialPeriodRepository, FinancialPeriodRepository>();
            services.AddScoped<IPayTypeRepository, PayTypeRepository>();
            services.AddScoped<IPayEntryRepository, PayEntryRepository>();
            services.AddScoped<IPayEntryService, PayEntryService>();
            services.AddScoped<IEntryRepository, EntryRepository>();
            services.AddScoped<IEntryItemRepository, EntryItemRepository>();
            services.AddScoped<IPriceRepository, PriceRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IItemUnitRepository, ItemUnitRepository>();
            services.AddScoped<IStoreItemRepository, StoreItemRepository>();
            services.AddScoped<IStoreItemUnitRepository, StoreItemUnitRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IBillTypeRepository, BillTypeRepository>();
            services.AddScoped<ICustomerTypeRepository, CustomerTypeRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<ICostCenterRepository, CostCenterRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountBalanceRepository, AccountBalanceRepository>();
            services.AddScoped<IAccountBalanceService, AccountBalanceService>();
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<ISellerRepository, SellerRepository>();
            services.AddScoped<IItemUnitPriceRepository, ItemUnitPriceRepository>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IAuthClientRepository, AuthClientRepository>();
            services.AddScoped<IAuthRefreshTokenRepository, AuthRefreshTokenRepository>();

            services.AddTransient<DataInitializer>();
            services.AddSingleton<ILanguageManager, LanguageManager>();
            services.AddSingleton<IPeriodManager, PeriodManager>();
            services.AddSingleton<IFileManager, FileManager>();
            services.AddSingleton<IPredefinedGuideService, PredefinedGuideService>();
            services.AddSingleton<IModelFactory, ModelFactory>();

            services.AddScoped<IBillService, BillService>();
            #endregion

            services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<ERPContext>()
                    .AddDefaultTokenProviders();

            ConfigureJwtAuthService(services);

            services.AddAuthorization(config =>
            {
                //config.AddPolicy("Admins", p =>
                //{
                //    p.RequireRole("Admin");
                //    p.RequireClaim("SuperUser", "True");
                //});
            });

            services.ConfigureApplicationCookie(options => options.Events = new CookieAuthenticationEvents()
            {
                OnRedirectToLogin = (ctx) =>
                {
                    if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                    {
                        ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    }
                    return Task.CompletedTask;
                },
                OnRedirectToAccessDenied = (ctx) =>
                {
                    if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                    {
                        ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
                    }
                    return Task.CompletedTask;
                }
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = false;
            });

            // add cors
            services.AddCors(config =>
            {
                //config.AddPolicy("AAA", builder =>
                //{
                //    builder.AllowAnyOrigin();
                //});
            });

            services.AddDataProtection()
                 .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
                 {
                     EncryptionAlgorithm = EncryptionAlgorithm.AES_256_GCM,
                     ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                 });

            // add mvc after cors
            services.AddMvc(opt =>
            {
                // opt.SslPort = 43388,
                // opt.Filters.Add(new RequireHttpsAttribute());
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = Configuration["Globalization:DateFormat"];
                options.SerializerSettings.Culture = new System.Globalization.CultureInfo(Configuration["Globalization:Culture"]);
            }); ;

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "ERP API",
                    Version = "v1",
                    Description = "featuer ERP API",
                    Contact = new Contact { Name = "ERP System support", Email = "", Url = "" },
                });

                c.AddSecurityDefinition("Bearer",
                    new ApiKeyScheme()
                    {
                        In = "header",
                        Description = "Please insert JWT with Bearer into field",
                        Name = "Authorization",
                        Type = "apiKey"
                    });

                c.OperationFilter<ExamplesOperationFilter>();
                c.OperationFilter<AcceptLanguageHeaderParameterOperationFilter>();
                c.OperationFilter<AcceptPeriodHeaderParameterOperationFilter>();
                c.DocumentFilter<SecurityRequirementsDocumentFilter>();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ERPContext context, DataInitializer dataInitializer)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();

            // env.EnvironmentName = EnvironmentName.Production;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async db =>
                    {
                        await HandleErrors(db);
                    });
                });
            }

            // allow global cors
            app.UseCors(config =>
            {
                config
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
            });

            app.UseAuthentication();

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            MapAutoMapper();

            if (Configuration["Database:InMemoryDatabase"].ToLower() != "true")
            {
                // Applies any pending migrations for the context to the database. 
                // Will create the database if it does not already exist
                context.Database.Migrate();
            }

            dataInitializer.Seed().Wait();
        }

        private static async Task HandleErrors(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("Admin !");
        }
    }
}
