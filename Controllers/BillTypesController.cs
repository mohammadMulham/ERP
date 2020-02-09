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
using ERPAPI.ViewModels.BillTypes;
using ERPAPI.SwaggerExamples.BillTypes;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]")]
    public class BillTypesController : BaseController
    {
        private IBillTypeRepository _billTypeRepo;

        public BillTypesController(
            ILanguageManager languageManager,
            IBillTypeRepository billTypeRepo) : base(languageManager)
        {
            _billTypeRepo = billTypeRepo;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                _billTypeRepo.SetLoggedInUserId(GetUserId());

                _billTypeRepo.SetIsAdmin(GetIsUserAdmin());
            }
            return base.OnActionExecutionAsync(context, next);
        }

        [HttpGet(Name = "GetBillTypes")]
        [ProducesResponseType(typeof(IEnumerable<BillTypeViewModel>), 200)]
        [SwaggerResponseExample(200, typeof(BillTypeViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> GetAll()
        {
            var billTypes = await _billTypeRepo.GetAllNoTracking().ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<BillTypeViewModel>>(billTypes);
            return Ok(viewModels);
        }

        [HttpGet("{id}", Name = "GetBillType")]
        [ProducesResponseType(typeof(BillTypeViewModel), 200)]
        [SwaggerResponseExample(200, typeof(BillTypeViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Get(long id)
        {
            var billType = await _billTypeRepo.GetAsync(id);
            if (billType == null)
            {
                return NotFound(Resources.Bills.BillResource.BillTypeNotFound);
            }
            var viewModel = AutoMapper.Mapper.Map<BillTypeViewModel>(billType);
            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BillTypeViewModel), 200)]
        [SwaggerResponseExample(200, typeof(BillTypeViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(AddBillTypeViewModel), typeof(AddBillTypeViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Post([FromBody]AddBillTypeViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (await _billTypeRepo.IsExistNameAsync(model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            var billType = AutoMapper.Mapper.Map<BillType>(model);

            var affectedRows = await _billTypeRepo.AddAsync(billType);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<BillTypeViewModel>(billType);

                return CreatedAtRoute("GetBillType", new { id = billType.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BillTypeViewModel), 200)]
        [SwaggerResponseExample(200, typeof(BillTypeViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(EditBillTypeViewModel), typeof(EditBillTypeViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Put(long id, [FromBody]EditBillTypeViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            var billType = await _billTypeRepo.GetAsync(id);
            if (billType == null)
            {
                return NotFound(Resources.Bills.BillResource.BillTypeNotFound);
            }

            if (await _billTypeRepo.IsExistNameAsync(billType.Id, model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            billType = AutoMapper.Mapper.Map(model, billType);

            var affectedRows = 0;

            affectedRows = await _billTypeRepo.EditAsync(billType);

            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<BillTypeViewModel>(billType);

                return Ok(viewModel);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BillTypeViewModel), 200)]
        [SwaggerResponseExample(200, typeof(BillTypeViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Delete(long id)
        {
            var billType = await _billTypeRepo.GetAsync(id);
            if (billType == null)
            {
                return NotFound(Resources.Bills.BillResource.BillTypeNotFound);
            }

            var affectedRows = await _billTypeRepo.DeleteAsync(billType);
            if (affectedRows == -1)
            {
                return BadRequest(Resources.Bills.BillResource.CanNotDeleteBillType);
            }
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<BillTypeViewModel>(billType);
                return Ok(viewModel);
            }
            return BadRequest();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _billTypeRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
