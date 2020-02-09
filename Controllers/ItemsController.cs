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
using Microsoft.EntityFrameworkCore;
using ERPAPI.Extentions;
using Swashbuckle.AspNetCore.Examples;
using Newtonsoft.Json.Converters;
using ERPAPI.ViewModels.Items;
using ERPAPI.SwaggerExamples.Items;
using Microsoft.AspNetCore.Authorization;
using ERPAPI.ViewModels.Units;
using ERPAPI.ViewModels;
using ERPAPI.Factories;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]")]
    public class ItemsController : BaseController
    {
        #region variables
        private IPeriodManager _periodManager;
        private IItemRepository _itemRepo;
        private IItemUnitRepository _itemUnitRepo;
        private IUnitRepository _unitRepo;
        private IPriceRepository _priceRepo;
        private IItemUnitPriceRepository _itemUnitPriceRepo;
        private IStoreItemUnitRepository _storeItemUnitRepo;
        private IModelFactory _modelFactory;
        private IItemGroupRepository _itemGroupRepo;
        #endregion

        public ItemsController(
            ILanguageManager languageManager,
            IPeriodManager periodManager,
            IItemGroupRepository itemGroupRepo,
            IItemRepository itemRepo,
            IUnitRepository unitRepo,
            IItemUnitRepository itemUnitRepo,
            IPriceRepository priceRepo,
            IItemUnitPriceRepository itemUnitPriceRepo,
            IStoreItemUnitRepository storeItemUnitRepo,
            IModelFactory modelFactory) : base(languageManager)
        {
            _periodManager = periodManager;
            _itemRepo = itemRepo;
            _unitRepo = unitRepo;
            _itemUnitRepo = itemUnitRepo;
            _itemGroupRepo = itemGroupRepo;
            _priceRepo = priceRepo;
            _itemUnitPriceRepo = itemUnitPriceRepo;
            _storeItemUnitRepo = storeItemUnitRepo;
            _modelFactory = modelFactory;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                _itemRepo.SetLoggedInUserId(GetUserId());
                _itemRepo.SetLoggedInUserName(GetUserUserName());
                _itemRepo.SetIsAdmin(GetIsUserAdmin());

                _unitRepo.SetLoggedInUserId(GetUserId());
                _unitRepo.SetLoggedInUserName(GetUserUserName());
                _unitRepo.SetIsAdmin(GetIsUserAdmin());

                _itemGroupRepo.SetLoggedInUserId(GetUserId());
                _itemGroupRepo.SetLoggedInUserName(GetUserUserName());
                _itemGroupRepo.SetIsAdmin(GetIsUserAdmin());

                _itemUnitRepo.SetLoggedInUserId(GetUserId());
                _itemUnitRepo.SetLoggedInUserName(GetUserUserName());
                _itemUnitRepo.SetIsAdmin(GetIsUserAdmin());

                _priceRepo.SetLoggedInUserId(GetUserId());
                _priceRepo.SetLoggedInUserName(GetUserUserName());
                _priceRepo.SetIsAdmin(GetIsUserAdmin());

                _itemUnitPriceRepo.SetLoggedInUserId(GetUserId());
                _itemUnitPriceRepo.SetLoggedInUserName(GetUserUserName());
                _itemUnitPriceRepo.SetIsAdmin(GetIsUserAdmin());

                _storeItemUnitRepo.SetLoggedInUserId(GetUserId());
                _storeItemUnitRepo.SetLoggedInUserName(GetUserUserName());
                _storeItemUnitRepo.SetIsAdmin(GetIsUserAdmin());
                _storeItemUnitRepo.SetPeriodId(_periodManager.GetPeriod());
            }
            return base.OnActionExecutionAsync(context, next);
        }

        [HttpGet(Name = "GetItems")]
        [ProducesResponseType(typeof(IEnumerable<ItemViewModel>), 200)]
        [SwaggerResponseExample(200, typeof(ItemViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> GetAll()
        {
            var items = await _itemRepo.GetAllNoTracking().ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<ItemViewModel>>(items);
            return Ok(viewModels);
        }

        [HttpGet("search/{key?}", Name = "SearchItems")]
        [ProducesResponseType(typeof(IEnumerable<SearchViewModel>), 200)]
        public async Task<IActionResult> Search(string key = "")
        {
            var itemUnits = await _itemUnitRepo.Search(key).ToListAsync();
            var viewModels = itemUnits.Select(_modelFactory.CreateSearchWithChildViewModel).ToList();
            return Ok(viewModels);
        }

        [HttpGet("generatecode/{id}", Name = "GenerateItemCode")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> NewCode(long id)
        {
            var itemGroup = await _itemGroupRepo.GetAsync(id);
            if (itemGroup == null)
            {
                return NotFound(Resources.Items.ItemResource.ItemGroupNotFound);
            }
            var newCode = await _itemRepo.GetNewCodeAsync(itemGroup.Id);
            return Ok(newCode);
        }

        [HttpGet("{id}", Name = "GetItem")]
        [ProducesResponseType(typeof(ItemViewModel), 200)]
        [SwaggerResponseExample(200, typeof(ItemViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Get(long id)
        {
            var item = await _itemRepo.GetAsync(id);
            if (item == null)
            {
                return NotFound(Resources.Items.ItemResource.ItemNotFound);
            }
            var viewModel = AutoMapper.Mapper.Map<ItemViewModel>(item);
            return Ok(viewModel);
        }

        [HttpGet("{id}/units/{key?}", Name = "GetItemUnits")]
        [ProducesResponseType(typeof(UnitViewModel), 200)]
        public async Task<IActionResult> GetItemUnits(long id, string key = "")
        {
            var units = await _itemUnitRepo.GetItemUnits(id, key).ToListAsync();

            var viewModels = AutoMapper.Mapper.Map<IEnumerable<SearchViewModel>>(units);

            return Ok(viewModels);
        }

        [HttpGet("{id}/{unitId}/prices", Name = "GetItemUnitPrices")]
        [ProducesResponseType(typeof(UnitViewModel), 200)]
        public async Task<IActionResult> GetItemUnitPrices(long id, long unitId)
        {
            var itemUnitPrices = await _itemUnitPriceRepo.GetItemUnitPrices(id, unitId).ToListAsync();
            var viewModels = itemUnitPrices.Select(_modelFactory.CreateSearchViewModel);

            return Ok(viewModels);
        }

        [HttpGet("{id}/{unitId}/{storeId}/store", Name = "GetItemUnitStore")]
        public async Task<IActionResult> GetItemUnitStoreQuantity(long id, long unitId, long storeId)
        {
            var storeItemUnit = await _storeItemUnitRepo.GetAsync(id, unitId, storeId);
            var viewModel = new
            {
                Quantity = storeItemUnit != null ? storeItemUnit.Quantity : 0,
                MinLimit  = storeItemUnit != null ? storeItemUnit.MinLimit : 0,
                MaxLimit = storeItemUnit != null ? storeItemUnit.MaxLimit : 0,
                ReOrderLimit  = storeItemUnit != null ? storeItemUnit.ReOrderLimit : 0,
            };
            return Ok(viewModel);
        }

        [HttpGet("{id}/{unitId}/prices/{priceId}", Name = "GetItemUnitPrice")]
        [ProducesResponseType(typeof(UnitViewModel), 200)]
        public async Task<IActionResult> GetItemUnitPrice(long id, long unitId, long priceId)
        {
            var price = await _itemUnitPriceRepo.GetItemUnitPriceAsync(id, unitId, priceId);
            return Ok(price);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ItemViewModel), 200)]
        [SwaggerResponseExample(200, typeof(ItemViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(AddItemViewModel), typeof(AddItemViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Post([FromBody]AddItemViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var parentItem = await _itemGroupRepo.GetAsync(model.ItemGroupId);
            if (parentItem == null)
            {
                return NotFound(Resources.Items.ItemResource.ItemGroupNotFound);
            }

            var unit = await _unitRepo.GetAsync(model.UnitId);
            if (unit == null)
            {
                return NotFound(Resources.Items.ItemResource.UnitNotFound);
            }

            if (await _itemRepo.IsExistCodeAsync(model.Code))
            {
                ModelState.AddModelError("Code", Resources.Global.Common.ThisCodeExist);
            }
            if (await _itemRepo.IsExistNameAsync(model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
            }

            if (!string.IsNullOrWhiteSpace(model.UnitBarcode) && await _itemUnitRepo.IsExistCodeAsync(model.UnitBarcode))
            {
                ModelState.AddModelError("UnitBarcode", Resources.Items.ItemResource.ThisBarCodeExist);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            var item = new Item(model.Name, model.Code, model.Type, parentItem.Id, true, model.Note, unit.Id, model.UnitBarcode);

            var affectedRows = await _itemRepo.AddAsync(item);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<ItemViewModel>(item);

                return CreatedAtRoute("GetItem", new { id = item.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpPost("{id}/unit")]
        [ProducesResponseType(typeof(ItemViewModel), 200)]
        public async Task<IActionResult> PostUnit(long id, [FromBody]AddItemUnitViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (model.ItemId != id)
            {
                return BadRequest();
            }

            var item = await _itemRepo.GetAsync(model.ItemId);
            if (item == null)
            {
                return NotFound(Resources.Items.ItemResource.ItemNotFound);
            }

            var unit = await _unitRepo.GetAsync(model.UnitId);
            if (unit == null)
            {
                return NotFound(Resources.Items.ItemResource.UnitNotFound);
            }

            if (await _itemUnitRepo.IsExistAsync(item.Id, unit.Id))
            {
                ModelState.AddModelError("", "this item have this unit");
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            if (!string.IsNullOrWhiteSpace(model.UnitBarcode) && await _itemUnitRepo.IsExistCodeAsync(model.UnitBarcode))
            {
                ModelState.AddModelError("UnitBarcode", Resources.Items.ItemResource.ThisBarCodeExist);
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            item.ItemUnits.Add(new ItemUnit(unit.Id, model.UnitBarcode, model.UnitFactor));

            var affectedRows = await _itemRepo.EditAsync(item);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<ItemViewModel>(item);

                return CreatedAtRoute("GetItem", new { id = item.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpPost("{id}/{unitId}/price")]
        [ProducesResponseType(typeof(ItemViewModel), 200)]
        public async Task<IActionResult> PostPrice(long id, long unitId, [FromBody]AddItemUnitPriceViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (model.ItemId != id)
            {
                return BadRequest();
            }

            if (model.UnitId != unitId)
            {
                return BadRequest();
            }

            var itemUnit = await _itemUnitRepo.GetAsync(model.ItemId, model.UnitId);
            if (itemUnit == null)
            {
                return NotFound("item unit not found");
            }

            var price = await _priceRepo.GetAsync(model.PriceId);
            if (price == null)
            {
                return NotFound(Resources.Items.ItemResource.PriceNotFound);
            }

            if (await _itemUnitPriceRepo.IsExistAsync(itemUnit.Id, price.Id))
            {
                ModelState.AddModelError("", "this item unit have this price");
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            itemUnit.ItemUnitPrices.Add(new ItemUnitPrice(price.Id, model.Price));

            var affectedRows = await _itemUnitRepo.EditAsync(itemUnit);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<ItemViewModel>(itemUnit.Item);

                return CreatedAtRoute("GetItem", new { id = itemUnit.Item.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ItemViewModel), 200)]
        [SwaggerResponseExample(200, typeof(ItemViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Delete(long id)
        {
            var item = await _itemRepo.GetAsync(id);
            if (item == null)
            {
                return NotFound(Resources.Items.ItemResource.ItemNotFound);
            }

            var affectedRows = await _itemRepo.DeleteAsync(item);
            if (affectedRows == -1)
            {
                return BadRequest(Resources.Items.ItemResource.CanNotDeleteItem);
            }
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<ItemViewModel>(item);
                return Ok(viewModel);
            }
            return BadRequest();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _itemRepo.Dispose();
                _unitRepo.Dispose();
                _itemGroupRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
