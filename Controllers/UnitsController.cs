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
using ERPAPI.ViewModels.Units;
using ERPAPI.SwaggerExamples.Units;
using ERPAPI.SwaggerExamples.Items;
using ERPAPI.ViewModels;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]")]
    public class UnitsController : BaseController
    {
        private IUnitRepository _unitRepo;

        public UnitsController(
            ILanguageManager languageManager,
            IUnitRepository unitRepo) : base(languageManager)
        {
            _unitRepo = unitRepo;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                _unitRepo.SetLoggedInUserId(GetUserId());
                _unitRepo.SetLoggedInUserName(GetUserHostName());
                _unitRepo.SetIsAdmin(GetIsUserAdmin());
            }
            return base.OnActionExecutionAsync(context, next);
        }

        [HttpGet(Name = "GetUnits")]
        [ProducesResponseType(typeof(IEnumerable<UnitViewModel>), 200)]
        [SwaggerResponseExample(200, typeof(UnitViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> GetAll()
        {
            var units = await _unitRepo.GetAllNoTracking().ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<UnitViewModel>>(units);
            return Ok(viewModels);
        }

        [HttpGet("search/{key?}", Name = "SearchUnit")]
        [ProducesResponseType(typeof(IEnumerable<SearchViewModel>), 200)]
        public async Task<IActionResult> Search(string key = "")
        {
            var units = await _unitRepo.Search(key).ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<SearchViewModel>>(units);
            return Ok(viewModels);
        }

        [HttpGet("{id}", Name = "GetUnit")]
        [ProducesResponseType(typeof(UnitViewModel), 200)]
        [SwaggerResponseExample(200, typeof(UnitViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Get(long id)
        {
            var unit = await _unitRepo.GetAsync(id);
            if (unit == null)
            {
                return NotFound(Resources.Items.ItemResource.UnitNotFound);
            }
            var viewModel = AutoMapper.Mapper.Map<UnitViewModel>(unit);
            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UnitViewModel), 200)]
        [SwaggerResponseExample(200, typeof(UnitViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(AddUnitViewModel), typeof(AddUnitViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Post([FromBody]AddUnitViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (await _unitRepo.IsExistNameAsync(model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            var unit = new Unit(model.Name);

            var affectedRows = await _unitRepo.AddAsync(unit);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<UnitViewModel>(unit);

                return CreatedAtRoute("GetUnit", new { id = unit.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UnitViewModel), 200)]
        [SwaggerResponseExample(200, typeof(UnitViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Put(long id, [FromBody]EditUnitViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            var unit = await _unitRepo.GetAsync(id);
            if (unit == null)
            {
                return NotFound(Resources.Items.ItemResource.UnitNotFound);
            }

            if (await _unitRepo.IsExistNameAsync(unit.Id, model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            unit.Name = model.Name;

            var affectedRows = await _unitRepo.EditAsync(unit);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<UnitViewModel>(unit);

                return Ok(viewModel);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(UnitViewModel), 200)]
        [SwaggerResponseExample(200, typeof(UnitViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Delete(long id)
        {
            var unit = await _unitRepo.GetAsync(id);
            if (unit == null)
            {
                return NotFound(Resources.Items.ItemResource.UnitNotFound);
            }

            var affectedRows = await _unitRepo.DeleteAsync(unit);
            if (affectedRows == -1)
            {
                return BadRequest(Resources.Items.ItemResource.CanNotDeleteUnit);
            }
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<UnitViewModel>(unit);
                return Ok(viewModel);
            }
            return BadRequest();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
