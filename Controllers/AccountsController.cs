using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Managers;
using Microsoft.AspNetCore.Mvc.Filters;
using ERPAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using ERPAPI.Models;
using Microsoft.EntityFrameworkCore;
using ERPAPI.Extentions;
using Swashbuckle.AspNetCore.Examples;
using Newtonsoft.Json.Converters;
using ERPAPI.ViewModels.Accounts;
using ERPAPI.SwaggerExamples.Accounts;
using ERPAPI.Options;
using ERPAPI.ViewModels;
using ERPAPI.Factories;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]")]
    public class AccountsController : BaseController
    {
        private ICurrencyRepository _currencyRepo;
        private IModelFactory _modelFactory;
        private IAccountRepository _accountRepo;
        private ICustomerRepository _customerRepo;
        private IWritableOptions<DefaultKeysOptions> _defaultKeysOptions;

        public AccountsController(ILanguageManager languageManager,
            IAccountRepository accountRepo,
            ICustomerRepository customerRepo,
            IWritableOptions<DefaultKeysOptions> defaultKeysOptions,
            ICurrencyRepository currencyRepo,
            IModelFactory modelFactory) : base(languageManager)
        {
            _accountRepo = accountRepo;
            _customerRepo = customerRepo;
            _defaultKeysOptions = defaultKeysOptions;
            _currencyRepo = currencyRepo;
            _modelFactory = modelFactory;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                _accountRepo.SetLoggedInUserId(GetUserId());
                _customerRepo.SetLoggedInUserId(GetUserId());

                _accountRepo.SetIsAdmin(GetIsUserAdmin());
                _customerRepo.SetIsAdmin(GetIsUserAdmin());
            }
            return base.OnActionExecutionAsync(context, next);
        }

        [HttpGet(Name = "GetAccounts")]
        [ProducesResponseType(typeof(IEnumerable<AccountViewModel>), 200)]
        [SwaggerResponseExample(200, typeof(AccountViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _accountRepo.GetAllNoTracking().ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<AccountViewModel>>(accounts);
            return Ok(viewModels);
        }

        [HttpGet("masternodes/", Name = "GetMasterAccounts")]
        public async Task<IActionResult> MasterNodes()
        {
            var viewModels = await _accountRepo.GetAllNoTracking().Where(e => e.ParentId == null).Select(e => new
            {
                Id = e.Number,
                Name = e.FinalAccount != null ? string.Format("{0} ({1})", e.CodeName, e.FinalAccount.Name) : e.CodeName,
                hasChildren = e.Accounts.LongCount(ee => ee.EntityStatus == EntityStatus.Active) > 0,
                Type = e.GetNodeType(),
                Children = e.Accounts.Where(ee => ee.EntityStatus == EntityStatus.Active).OrderBy(ee => ee.Code).Select(ee => new
                {
                    Id = ee.Number,
                    Name = ee.FinalAccount != null ? string.Format("{0} ({1})", ee.CodeName, ee.FinalAccount.Name) : ee.CodeName,
                    hasChildren = ee.Accounts.LongCount(eee => eee.EntityStatus == EntityStatus.Active) > 0,
                    Type = ee.GetNodeType(),
                    Children = ee.Accounts.Where(eee => eee.EntityStatus == EntityStatus.Active).OrderBy(eee => eee.Code).Select(eee => new
                    {
                        Id = eee.Number,
                        Name = eee.FinalAccount != null ? string.Format("{0} ({1})", eee.CodeName, eee.FinalAccount.Name) : eee.CodeName,
                        hasChildren = eee.Accounts.LongCount(eeee => eeee.EntityStatus == EntityStatus.Active) > 0,
                        Type = eee.GetNodeType(),
                    })
                })
            }).ToListAsync();
            return Ok(viewModels);
        }

        [HttpGet("permissions/{id}", Name = "GetAccountPermissions")]
        public async Task<IActionResult> Permissions(long id)
        {
            var viewModel = new
            {
                Id = id,
                CanBeDeleted = new
                {
                    Value = await _accountRepo.CheckIfCanBeDeletedAsync(id),
                    Message = "لا يمكن حذفه لانه مرتبط بعنصر"
                },

            };
            return Ok(viewModel);
        }

        [HttpGet("nodes/{id}", Name = "GetNodeAccounts")]
        public async Task<IActionResult> Nodes(long id)
        {
            var account = await _accountRepo.GetAsync(id);
            if (account == null)
            {
                return NotFound(Resources.Accounts.AccountResource.AccountNotFound);
            }
            var viewModels = await _accountRepo.GetAllNoTracking().Where(e => e.ParentId == account.Id)
                .Select(e => new
                {
                    Id = e.Number,
                    Name = e.FinalAccount != null ? string.Format("{0} ({1})", e.CodeName, e.FinalAccount.Name) : e.CodeName,
                    hasChildren = e.Accounts.LongCount(ee => ee.EntityStatus == EntityStatus.Active) > 0,
                    Type = e.GetNodeType(),
                }).ToListAsync();
            return Ok(viewModels);
        }

        [HttpGet("search/{key?}", Name = "SearchAccounts")]
        [ProducesResponseType(typeof(IEnumerable<SearchViewModel>), 200)]
        public async Task<IActionResult> Search(string key = "", bool? leavesOnly = false, AccountType? accountType = null)
        {
            var accounts = await _accountRepo.Search(key, leavesOnly, accountType).ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<SearchViewModel>>(accounts);
            return Ok(viewModels);
        }

        [HttpGet("customers/{key?}", Name = "SearchCustomerAccounts")]
        [ProducesResponseType(typeof(IEnumerable<SearchWithChildViewModel>), 200)]
        public async Task<IActionResult> Cusromers(string key = "")
        {
            var accounts = await _accountRepo.SearchCustomers(key).ToListAsync();
            var viewModels = accounts.Select(_modelFactory.CreateSearchWithChildViewModel).ToList();
            return Ok(viewModels);
        }

        [HttpGet("generatecode/{id?}", Name = "GenerateAccountCode")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> NewCode(long? id)
        {
            Guid? parentId = null;
            if (id.HasValue)
            {
                var costCenter = await _accountRepo.GetAsync(id.Value);
                if (costCenter == null)
                {
                    return NotFound(Resources.Accounts.AccountResource.AccountNotFound);
                }
                parentId = costCenter.Id;
            }
            var newCode = await _accountRepo.GetNewCodeAsync(parentId);
            return Ok(newCode);
        }

        [HttpGet("new/{id?}", Name = "NewAccount")]
        public async Task<IActionResult> NewAccount(long? id)
        {
            if (id.HasValue)
            {
                var account = await _accountRepo.GetAsync(id.Value);
                if (account == null)
                {
                    return NotFound(Resources.Accounts.AccountResource.AccountNotFound);
                }
                var newCode = await _accountRepo.GetNewCodeAsync(account.Id);
                var viewModel = new
                {
                    NewCode = newCode,
                    ParentAccountCodeName = account.CodeName,
                    ParentAccountId = account.Number,
                    FinalAccountCodeName = account.FinalAccount?.CodeName,
                    FinalAccountId = account.FinalAccount?.Number,
                    AccountType = account.AccountType
                };
                return Ok(viewModel);
            }
            else
            {
                var newCode = await _accountRepo.GetNewCodeAsync();
                var viewModel = new
                {
                    NewCode = newCode,
                };
                return Ok(viewModel);
            }
        }

        [HttpGet("{id}", Name = "GetAccount")]
        [ProducesResponseType(typeof(AccountViewModel), 200)]
        [SwaggerResponseExample(200, typeof(AccountViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Get(long id)
        {
            var account = await _accountRepo.GetAsync(id);
            if (account == null)
            {
                return NotFound(Resources.Accounts.AccountResource.AccountNotFound);
            }
            var viewModel = AutoMapper.Mapper.Map<AccountViewModel>(account);
            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AccountViewModel), 201)]
        [SwaggerResponseExample(201, typeof(AccountViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(AddAccountViewModel), typeof(AddAccountViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Post([FromBody]AddAccountViewModel model)
        {
            Guid? parentId = null;

            if (model == null)
            {
                return BadRequest();
            }

            Guid currencyId;
            var currency = await _currencyRepo.GetAsync(model.CurrencyId);
            if (currency != null)
            {
                currencyId = currency.Id;
            }
            else
            {
                currencyId = _defaultKeysOptions.Value.CurrencyId;
            }

            if (model.ParentAccountId.HasValue)
            {
                var parentAccount = await _accountRepo.GetAsync(model.ParentAccountId.Value);
                if (parentAccount == null)
                {
                    return NotFound(Resources.Accounts.AccountResource.ParentAccountNotFound);
                }
                parentId = parentAccount.Id;
            }

            Guid? finalAccountId = null;

            if (model.FinalAccountId.HasValue)
            {
                var finalAccount = await _accountRepo.GetAsync(model.FinalAccountId.Value);
                if (finalAccount == null)
                {
                    return NotFound(Resources.Accounts.AccountResource.FinalAccountNotFound);
                }
                finalAccountId = finalAccount.Id;
            }

            Guid? customerId = null;

            if (model.CustomerId.HasValue)
            {
                var customer = await _customerRepo.GetAsync(model.CustomerId.Value);
                if (customer == null)
                {
                    return NotFound(Resources.Customers.CustomerResource.CustomerNotFound);
                }
                customerId = customer.Id;
            }

            if (await _accountRepo.IsExistCodeAsync(model.Code))
            {
                ModelState.AddModelError("Code", Resources.Global.Common.ThisCodeExist);
            }
            if (await _accountRepo.IsExistNameAsync(model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            var account = new Account(model.Name, model.Code, parentId, finalAccountId, model.AccountType, model.DirectionType, currencyId, customerId, model.Note);
            if (parentId.HasValue)
            {
                account.ParentId = parentId.Value;
            }

            var affectedRows = await _accountRepo.AddAsync(account);
            if (affectedRows > 0)
            {
                _accountRepo.LoadReferences(account);

                var viewModel = AutoMapper.Mapper.Map<AccountViewModel>(account);

                return CreatedAtRoute("GetAccount", new { id = account.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AccountViewModel), 200)]
        [SwaggerResponseExample(200, typeof(AccountViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Delete(long id)
        {
            var account = await _accountRepo.GetAsync(id);
            if (account == null)
            {
                return NotFound(Resources.Accounts.AccountResource.AccountNotFound);
            }

            bool canBeDeleted = await _accountRepo.CheckIfCanBeDeletedAsync(id);
            if (!canBeDeleted)
            {
                return BadRequest(Resources.Accounts.AccountResource.CanNotDeleteAccount);
            }

            var affectedRows = await _accountRepo.SetDeletedAsync(account);

            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<AccountViewModel>(account);
                return Ok(viewModel);
            }
            return BadRequest();
        }

        [HttpDelete("undelete/{id}")]
        [ProducesResponseType(typeof(AccountViewModel), 200)]
        [SwaggerResponseExample(200, typeof(AccountViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> UndDlete(long id)
        {
            var costcenter = await _accountRepo.GetEntityAsync(id);
            if (costcenter == null)
            {
                return NotFound(Resources.Accounts.AccountResource.AccountNotFound);
            }

            var affectedRows = await _accountRepo.SetUnDeletedAsync(costcenter);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<AccountViewModel>(costcenter);
                return Ok(viewModel);
            }
            return BadRequest();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _accountRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
