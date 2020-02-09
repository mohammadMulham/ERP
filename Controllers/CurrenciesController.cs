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
using ERPAPI.ViewModels.Currencies;
using Microsoft.EntityFrameworkCore;
using ERPAPI.Extentions;
using Swashbuckle.AspNetCore.Examples;
using Newtonsoft.Json.Converters;
using ERPAPI.SwaggerExamples.Currencies;
using Microsoft.AspNetCore.Authorization;
using ERPAPI.Options;
using ERPAPI.Factories;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]")]
    public class CurrenciesController : BaseController
    {
        private ICurrencyRepository _currencyRepo;
        private IModelFactory _modelFactory;
        private IWritableOptions<DefaultKeysOptions> _defaultKeysOptions;

        public CurrenciesController(
            ILanguageManager languageManager,
            ICurrencyRepository currencyRepo,
            IWritableOptions<DefaultKeysOptions> defaultKeysOptions,
            IModelFactory modelFactory) : base(languageManager)
        {
            _defaultKeysOptions = defaultKeysOptions;
            _currencyRepo = currencyRepo;
            _modelFactory = modelFactory;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                _currencyRepo.SetLoggedInUserId(GetUserId());

                _currencyRepo.SetIsAdmin(GetIsUserAdmin());
            }
            return base.OnActionExecutionAsync(context, next);
        }

        [HttpGet(Name = "GetCurrencies")]
        [ProducesResponseType(typeof(IEnumerable<CurrencyViewModel>), 200)]
        [SwaggerResponseExample(200, typeof(CurrencyViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> GetAll()
        {

            var currencies = await _currencyRepo.GetAllNoTracking().ToListAsync();
            var viewModels = currencies.Select(c => _modelFactory.CreateViewModel(c, _defaultKeysOptions.Value.CurrencyId));
            return Ok(viewModels);
        }

        [HttpGet("{id}", Name = "GetCurrency")]
        [ProducesResponseType(typeof(CurrencyViewModel), 200)]
        [SwaggerResponseExample(200, typeof(CurrencyViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Get(long id)
        {
            var currency = await _currencyRepo.GetAsync(id);
            if (currency == null)
            {
                return NotFound(Resources.Currencies.CurrencyResource.CurrencyNotFound);
            }
            var viewModel = _modelFactory.CreateViewModel(currency, _defaultKeysOptions.Value.CurrencyId);
            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CurrencyViewModel), 200)]
        [SwaggerResponseExample(200, typeof(CurrencyViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(AddCurrencyViewModel), typeof(AddCurrencyViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Post([FromBody]AddCurrencyViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (await _currencyRepo.IsExistCodeAsync(model.Code))
            {
                ModelState.AddModelError("Code", Resources.Global.Common.ThisCodeExist);
            }
            if (await _currencyRepo.IsExistNameAsync(model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            var currency = new Currency(model.Code, model.Name, model.Value, model.PartName, model.PartRate, model.Note);

            var affectedRows = await _currencyRepo.AddAsync(currency);
            if (affectedRows > 0)
            {
                var viewModel = _modelFactory.CreateViewModel(currency, _defaultKeysOptions.Value.CurrencyId);

                return CreatedAtRoute("GetCurrency", new { id = currency.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CurrencyViewModel), 200)]
        [SwaggerResponseExample(200, typeof(CurrencyViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Put(long id, [FromBody]EditCurrencyViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (model.Id != id)
            {
                return BadRequest();
            }

            var currency = await _currencyRepo.GetAsync(id);
            if (currency == null)
            {
                return NotFound(Resources.Currencies.CurrencyResource.CurrencyNotFound);
            }

            if (await _currencyRepo.IsExistCodeAsync(currency.Id, model.Code))
            {
                ModelState.AddModelError("Code", Resources.Global.Common.ThisCodeExist);
            }
            if (await _currencyRepo.IsExistNameAsync(currency.Id, model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            currency = AutoMapper.Mapper.Map(model, currency);

            var affectedRows = await _currencyRepo.EditAsync(currency);
            if (affectedRows > 0)
            {
                var viewModel = _modelFactory.CreateViewModel(currency, _defaultKeysOptions.Value.CurrencyId);

                return CreatedAtRoute("GetCurrency", new { id = currency.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CurrencyViewModel), 200)]
        [SwaggerResponseExample(200, typeof(CurrencyViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Delete(long id)
        {
            var currency = await _currencyRepo.GetAsync(id);
            if (currency == null)
            {
                return NotFound(Resources.Currencies.CurrencyResource.CurrencyNotFound);
            }

            var affectedRows = await _currencyRepo.DeleteAsync(currency);
            if (affectedRows == -1)
            {
                return BadRequest(Resources.Currencies.CurrencyResource.CanNotDeleteCurrency);
            }
            if (affectedRows > 0)
            {
                var viewModel = _modelFactory.CreateViewModel(currency, _defaultKeysOptions.Value.CurrencyId);
                return Ok(viewModel);
            }
            return BadRequest();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _currencyRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
