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
using ERPAPI.ViewModels.Sellers;
using Microsoft.EntityFrameworkCore;
using ERPAPI.Extentions;
using Swashbuckle.AspNetCore.Examples;
using Newtonsoft.Json.Converters;
using ERPAPI.SwaggerExamples.Sellers;
using Microsoft.AspNetCore.Authorization;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]")]
    public class SellersController : BaseController
    {
        private ISellerRepository _sellerRepo;

        public SellersController(ILanguageManager languageManager, ISellerRepository sellerRepo) : base(languageManager)
        {
            _sellerRepo = sellerRepo;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                _sellerRepo.SetLoggedInUserId(GetUserId());

                _sellerRepo.SetIsAdmin(GetIsUserAdmin());
            }
            return base.OnActionExecutionAsync(context, next);
        }

        [HttpGet(Name = "GetSellers")]
        [ProducesResponseType(typeof(IEnumerable<SellerViewModel>), 200)]
        [SwaggerResponseExample(200, typeof(SellerViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> GetAll()
        {
            var sellers = await _sellerRepo.GetAllNoTracking().ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<SellerViewModel>>(sellers);
            return Ok(viewModels);
        }

        [HttpGet("generatecode/{id?}", Name = "GenerateSellerCode")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> NewCode(long? id)
        {
            Guid? parentId = null;
            if (id.HasValue)
            {
                var seller = await _sellerRepo.GetAsync(id.Value);
                if (seller == null)
                {
                    return NotFound(Resources.Sellers.SellerResource.SellerNotFound);
                }
                parentId = seller.Id;
            }
            var newCode = await _sellerRepo.GetNewCodeAsync(parentId);
            return Ok(newCode);
        }

        [HttpGet("{id}", Name = "GetSeller")]
        [ProducesResponseType(typeof(SellerViewModel), 200)]
        [SwaggerResponseExample(200, typeof(SellerViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Get(long id)
        {
            var seller = await _sellerRepo.GetAsync(id);
            if (seller == null)
            {
                return NotFound(Resources.Sellers.SellerResource.SellerNotFound);
            }
            var viewModel = AutoMapper.Mapper.Map<SellerViewModel>(seller);
            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(SellerViewModel), 200)]
        [SwaggerResponseExample(200, typeof(SellerViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(AddSellerViewModel), typeof(AddSellerViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Post([FromBody]AddSellerViewModel model)
        {
            Guid? parentId = null;

            if (model == null)
            {
                return BadRequest();
            }

            if (model.ParentSellerId.HasValue)
            {
                var parentSeller = await _sellerRepo.GetAsync(model.ParentSellerId.Value);
                if (parentSeller == null)
                {
                    return NotFound(Resources.Sellers.SellerResource.ParentSellerNotFound);
                }
                parentId = parentSeller.Id;
            }

            if (await _sellerRepo.IsExistCodeAsync(model.Code))
            {
                ModelState.AddModelError("Code", Resources.Global.Common.ThisCodeExist);
            }
            if (await _sellerRepo.IsExistNameAsync(model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            var seller = new Seller(model.Code, model.Name, model.Note);
            if (parentId.HasValue)
            {
                seller.ParentId = parentId.Value;
            }

            var affectedRows = await _sellerRepo.AddAsync(seller);
            if (affectedRows > 0)
            {
                _sellerRepo.LoadReferences(seller);

                var viewModel = AutoMapper.Mapper.Map<SellerViewModel>(seller);

                return CreatedAtRoute("GetSeller", new { id = seller.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SellerViewModel), 200)]
        [SwaggerResponseExample(200, typeof(SellerViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(AddSellerViewModel), typeof(AddSellerViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Put(long id, [FromBody]EditSellerViewModel model)
        {
            Guid? parentId = null;
            if (model == null)
            {
                return BadRequest();
            }

            if (model.Id != id)
            {
                return BadRequest();
            }

            var seller = await _sellerRepo.GetAsync(id);
            if (seller == null)
            {
                return NotFound(Resources.Sellers.SellerResource.SellerNotFound);
            }

            if (model.ParentSellerId.HasValue)
            {
                var parentSeller = await _sellerRepo.GetAsync(model.ParentSellerId.Value);
                if (parentSeller == null)
                {
                    return NotFound(Resources.Sellers.SellerResource.ParentSellerNotFound);
                }
                parentId = parentSeller.Id;
            }

            if (await _sellerRepo.IsExistCodeAsync(seller.Id, model.Code))
            {
                ModelState.AddModelError("Code", Resources.Global.Common.ThisCodeExist);
            }
            if (await _sellerRepo.IsExistNameAsync(seller.Id, model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            seller = AutoMapper.Mapper.Map(model, seller);
            seller.ParentId = parentId;

            var affectedRows = await _sellerRepo.EditAsync(seller);
            if (affectedRows > 0)
            {
                _sellerRepo.LoadReferences(seller);

                var viewModel = AutoMapper.Mapper.Map<SellerViewModel>(seller);

                return CreatedAtRoute("GetSeller", new { id = seller.Number }, viewModel);
            }
            return BadRequest();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(SellerViewModel), 200)]
        [SwaggerResponseExample(200, typeof(SellerViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Delete(long id)
        {
            var itemGroup = await _sellerRepo.GetAsync(id);
            if (itemGroup == null)
            {
                return NotFound(Resources.Sellers.SellerResource.SellerNotFound);
            }

            var affectedRows = await _sellerRepo.DeleteAsync(itemGroup);
            if (affectedRows == -1)
            {
                return BadRequest(Resources.Sellers.SellerResource.CanNotDeleteSeller);
            }
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<SellerViewModel>(itemGroup);
                return Ok(viewModel);
            }
            return BadRequest();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sellerRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
