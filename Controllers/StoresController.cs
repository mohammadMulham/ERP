using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Managers;
using Microsoft.AspNetCore.Mvc.Filters;
using ERPAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using ERPAPI.ViewModels.Card;
using ERPAPI.Models;
using ERPAPI.ViewModels.CostCenters;
using Microsoft.EntityFrameworkCore;
using ERPAPI.Extentions;
using Swashbuckle.AspNetCore.Examples;
using Newtonsoft.Json.Converters;
using ERPAPI.SwaggerExamples.CostCenters;
using Microsoft.AspNetCore.Authorization;
using ERPAPI.ViewModels.Stores;
using ERPAPI.SwaggerExamples.Stores;
using ERPAPI.ViewModels;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]")]
    public class StoresController : BaseController
    {
        #region variables
        private IStoreRepository _storeRepo;
        private ICostCenterRepository _costCenterRepo;
        private IAccountRepository _accountRepo;
        #endregion

        public StoresController(
            ILanguageManager languageManager,
            IStoreRepository storeRepo,
            ICostCenterRepository costCenterRepo,
            IAccountRepository accountRepo) : base(languageManager)
        {
            _storeRepo = storeRepo;
            _costCenterRepo = costCenterRepo;
            _accountRepo = accountRepo;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                _storeRepo.SetLoggedInUserId(GetUserId());
                _accountRepo.SetLoggedInUserId(GetUserId());
                _costCenterRepo.SetLoggedInUserId(GetUserId());

                _storeRepo.SetIsAdmin(GetIsUserAdmin());
                _accountRepo.SetIsAdmin(GetIsUserAdmin());
                _costCenterRepo.SetIsAdmin(GetIsUserAdmin());
            }
            return base.OnActionExecutionAsync(context, next);
        }

        [HttpGet(Name = "GetStores")]
        [ProducesResponseType(typeof(IEnumerable<StoreViewModel>), 200)]
        [SwaggerResponseExample(200, typeof(StoreViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> GetAll()
        {
            var stores = await _storeRepo.GetAllNoTracking().ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<StoreViewModel>>(stores);
            return Ok(viewModels);
        }
        
        [HttpGet("search/{key?}", Name = "SearchStores")]
        [ProducesResponseType(typeof(IEnumerable<SearchViewModel>), 200)]
        public async Task<IActionResult> Search(string key = "")
        {
            var stores = await _storeRepo.Search(key).ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<SearchViewModel>>(stores);
            return Ok(viewModels);
        }

        [HttpGet("generatecode/{id?}", Name = "GenerateStoreCode")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> NewCode(long? id = null)
        {
            Guid? parentId = null;
            if (id.HasValue)
            {
                var costCenter = await _storeRepo.GetAsync(id.Value);
                if (costCenter == null)
                {
                    return NotFound(Resources.Stores.StoreResource.StoreNotFound);
                }
                parentId = costCenter.Id;
            }
            var newCode = await _storeRepo.GetNewCodeAsync(parentId);
            return Ok(newCode);
        }

        [HttpGet("{id}", Name = "GetStore")]
        [ProducesResponseType(typeof(StoreViewModel), 200)]
        [SwaggerResponseExample(200, typeof(StoreViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Get(long id)
        {
            var store = await _storeRepo.GetAsync(id);
            if (store == null)
            {
                return NotFound(Resources.Stores.StoreResource.StoreNotFound);
            }
            var viewModel = AutoMapper.Mapper.Map<StoreViewModel>(store);
            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(StoreViewModel), 200)]
        [SwaggerResponseExample(200, typeof(StoreViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(AddStoreViewModel), typeof(AddStoreViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Post([FromBody]AddStoreViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            Guid? accountId = null;
            if (model.AccountId.HasValue)
            {
                var account = await _accountRepo.GetAsync(model.AccountId.Value);
                if (account == null)
                {
                    return NotFound(Resources.Accounts.AccountResource.AccountNotFound);
                }
                accountId = account.Id;
            }

            Guid? costCenterId = null;
            if (model.CostCenterId.HasValue)
            {
                var costCenter = await _costCenterRepo.GetAsync(model.CostCenterId.Value);
                if (costCenter == null)
                {
                    return NotFound(Resources.CostCenters.CostCenterResource.CostCenterNotFound);
                }
                costCenterId = costCenter.Id;
            }

            Guid? parentId = null;
            if (model.ParentId.HasValue)
            {
                var parentStore = await _storeRepo.GetAsync(model.ParentId.Value);
                if (parentStore == null)
                {
                    return NotFound(Resources.Stores.StoreResource.ParentStoreNotFound);
                }
                parentId = parentStore.Id;
            }

            if (await _storeRepo.IsExistCodeAsync(model.Code))
            {
                ModelState.AddModelError("Code", Resources.Global.Common.ThisCodeExist);
            }
            if (await _storeRepo.IsExistNameAsync(model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            var store = new Store(model.Name, model.Code, accountId, costCenterId, model.Note);
            if (parentId.HasValue)
            {
                store.ParentId = parentId.Value;
            }

            var affectedRows = await _storeRepo.AddAsync(store);
            if (affectedRows > 0)
            {
                _storeRepo.LoadReferences(store);

                var viewModel = AutoMapper.Mapper.Map<StoreViewModel>(store);

                return CreatedAtRoute("GetStore", new { id = store.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(StoreViewModel), 200)]
        [SwaggerResponseExample(200, typeof(StoreViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Delete(long id)
        {
            var store = await _storeRepo.GetAsync(id);
            if (store == null)
            {
                return NotFound(Resources.Stores.StoreResource.StoreNotFound);
            }

            var affectedRows = await _storeRepo.DeleteAsync(store);
            if (affectedRows == -1)
            {
                return BadRequest(Resources.Stores.StoreResource.CanNotDeleteStore);
            }
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<StoreViewModel>(store);
                return Ok(viewModel);
            }
            return BadRequest();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _storeRepo.Dispose();
                _accountRepo.Dispose();
                _costCenterRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
