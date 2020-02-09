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
using ERPAPI.ViewModels.ItemGroups;
using ERPAPI.SwaggerExamples.ItemGroups;
using ERPAPI.ViewModels;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]")]
    public class ItemGroupsController : BaseController
    {
        private IItemGroupRepository _itemGroupRepo;

        public ItemGroupsController(ILanguageManager languageManager, IItemGroupRepository itemGroupRepo) : base(languageManager)
        {
            _itemGroupRepo = itemGroupRepo;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                _itemGroupRepo.SetLoggedInUserId(GetUserId());

                _itemGroupRepo.SetIsAdmin(GetIsUserAdmin());
            }
            return base.OnActionExecutionAsync(context, next);
        }

        [HttpGet(Name = "GetItemGroups")]
        [ProducesResponseType(typeof(IEnumerable<ItemGroupViewModel>), 200)]
        [SwaggerResponseExample(200, typeof(ItemGroupViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> GetAll()
        {
            var itemGroups = await _itemGroupRepo.GetAllNoTracking().ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<ItemGroupViewModel>>(itemGroups);
            return Ok(viewModels);
        }

        [HttpGet("search/{key?}", Name = "SearchItemGroup")]
        [ProducesResponseType(typeof(IEnumerable<SearchViewModel>), 200)]
        public async Task<IActionResult> Search(string key = "")
        {
            var accounts = await _itemGroupRepo.Search(key).ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<SearchViewModel>>(accounts);
            return Ok(viewModels);
        }

        [HttpGet("generatecode/{id?}", Name = "GenerateItemGroupCode")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> NewCode(long? id)
        {
            Guid? parentId = null;
            if (id.HasValue)
            {
                var costCenter = await _itemGroupRepo.GetAsync(id.Value);
                if (costCenter == null)
                {
                    return NotFound(Resources.Items.ItemResource.ItemGroupNotFound);
                }
                parentId = costCenter.Id;
            }
            var newCode = await _itemGroupRepo.GetNewCodeAsync(parentId);
            return Ok(newCode);
        }

        [HttpGet("{id}", Name = "GetItemGroup")]
        [ProducesResponseType(typeof(ItemGroupViewModel), 200)]
        [SwaggerResponseExample(200, typeof(ItemGroupViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Get(long id)
        {
            var itemGroup = await _itemGroupRepo.GetAsync(id);
            if (itemGroup == null)
            {
                return NotFound(Resources.Items.ItemResource.ItemGroupNotFound);
            }
            var viewModel = AutoMapper.Mapper.Map<ItemGroupViewModel>(itemGroup);
            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ItemGroupViewModel), 200)]
        [SwaggerResponseExample(200, typeof(ItemGroupViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(AddItemGroupViewModel), typeof(AddItemGroupViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Post([FromBody]AddItemGroupViewModel model)
        {
            Guid? parentId = null;

            if (model == null)
            {
                return BadRequest();
            }

            if (model.ParentItemGroupId.HasValue)
            {
                var parentItemGroup = await _itemGroupRepo.GetAsync(model.ParentItemGroupId.Value);
                if (parentItemGroup == null)
                {
                    return NotFound(Resources.Items.ItemResource.ParentItemGroupNotFound);
                }
                parentId = parentItemGroup.Id;
            }

            if (await _itemGroupRepo.IsExistCodeAsync(model.Code))
            {
                ModelState.AddModelError("Code", Resources.Global.Common.ThisCodeExist);
            }
            if (await _itemGroupRepo.IsExistNameAsync(model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            var itemGroup = new ItemGroup(model.Name, model.Code,model.Note);
            if (parentId.HasValue)
            {
                itemGroup.ParentId = parentId.Value;
            }

            var affectedRows = await _itemGroupRepo.AddAsync(itemGroup);
            if (affectedRows > 0)
            {
                _itemGroupRepo.LoadReferences(itemGroup);

                var viewModel = AutoMapper.Mapper.Map<ItemGroupViewModel>(itemGroup);

                return CreatedAtRoute("GetItemGroup", new { id = itemGroup.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ItemGroupViewModel), 200)]
        [SwaggerResponseExample(200, typeof(ItemGroupViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Delete(long id)
        {
            var itemGroup = await _itemGroupRepo.GetAsync(id);
            if (itemGroup == null)
            {
                return NotFound(Resources.Items.ItemResource.ItemGroupNotFound);
            }

            var affectedRows = await _itemGroupRepo.DeleteAsync(itemGroup);
            if (affectedRows == -1)
            {
                return BadRequest(Resources.Items.ItemResource.CanNotDeleteItemGroup);
            }
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<ItemGroupViewModel>(itemGroup);
                return Ok(viewModel);
            }
            return BadRequest();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _itemGroupRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
