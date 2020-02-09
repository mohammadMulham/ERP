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
using ERPAPI.ViewModels.Prices;
using ERPAPI.SwaggerExamples.Prices;
using ERPAPI.SwaggerExamples.Items;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]")]
    public class PricesController : BaseController
    {
        private IPriceRepository _priceRepo;

        public PricesController(
            ILanguageManager languageManager,
            IPriceRepository priceRepo) : base(languageManager)
        {
            _priceRepo = priceRepo;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                _priceRepo.SetLoggedInUserId(GetUserId());

                _priceRepo.SetIsAdmin(GetIsUserAdmin());
            }
            return base.OnActionExecutionAsync(context, next);
        }

        [HttpGet(Name = "GetPrices")]
        [ProducesResponseType(typeof(IEnumerable<PriceViewModel>), 200)]
        [SwaggerResponseExample(200, typeof(PriceViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> GetAll()
        {
            var prices = await _priceRepo.GetAllNoTracking().ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<PriceViewModel>>(prices);
            return Ok(viewModels);
        }

        [HttpGet("{id}", Name = "GetPrice")]
        [ProducesResponseType(typeof(PriceViewModel), 200)]
        [SwaggerResponseExample(200, typeof(PriceViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Get(long id)
        {
            var price = await _priceRepo.GetAsync(id);
            if (price == null)
            {
                return NotFound(Resources.Items.ItemResource.PriceNotFound);
            }
            var viewModel = AutoMapper.Mapper.Map<PriceViewModel>(price);
            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PriceViewModel), 200)]
        [SwaggerResponseExample(200, typeof(PriceViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(AddPriceViewModel), typeof(AddPriceViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Post([FromBody]AddPriceViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (await _priceRepo.IsExistNameAsync(model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            var price = new Price(model.Name);

            var affectedRows = await _priceRepo.AddAsync(price);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<PriceViewModel>(price);

                return CreatedAtRoute("GetPrice", new { id = price.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PriceViewModel), 200)]
        [SwaggerResponseExample(200, typeof(PriceViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Put(long id, [FromBody]EditPriceViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            var price = await _priceRepo.GetAsync(id);
            if (price == null)
            {
                return NotFound(Resources.Items.ItemResource.PriceNotFound);
            }

            if (await _priceRepo.IsExistNameAsync(price.Id, model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            price.Name = model.Name;

            var affectedRows = await _priceRepo.EditAsync(price);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<PriceViewModel>(price);

                return Ok(viewModel);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(PriceViewModel), 200)]
        [SwaggerResponseExample(200, typeof(PriceViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Delete(long id)
        {
            var price = await _priceRepo.GetAsync(id);
            if (price == null)
            {
                return NotFound(Resources.Items.ItemResource.PriceNotFound);
            }

            var affectedRows = await _priceRepo.DeleteAsync(price);
            if (affectedRows == -1)
            {
                return BadRequest(Resources.Items.ItemResource.CanNotDeletePrice);
            }
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<PriceViewModel>(price);
                return Ok(viewModel);
            }
            return BadRequest();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _priceRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
