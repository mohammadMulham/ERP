using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using ERPAPI.Options;
using ERPAPI.Repositories;
using Swashbuckle.AspNetCore.Examples;
using Newtonsoft.Json.Converters;
using ERPAPI.ViewModels.PayTypes;
using ERPAPI.SwaggerExamples.PayTypes;
using Microsoft.EntityFrameworkCore;
using ERPAPI.Extentions;
using ERPAPI.SwaggerExamples.BillTypes;
using ERPAPI.ViewModels.BillTypes;
using ERPAPI.Models;
using System.Data.SqlClient;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]")]
    public class PayTypesController : BaseController
    {
        #region variabls
        private IPayTypeRepository _payTypeRep;
        
        #endregion

        public PayTypesController(
            ILanguageManager languageManager,
            IPayTypeRepository payTypeRepo) : base(languageManager)
        {
            _payTypeRep = payTypeRepo;
        }
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                _payTypeRep.SetLoggedInUserId(GetUserId());
                _payTypeRep.SetIsAdmin(GetIsUserAdmin());
                //_payTypeRep.SetPeriodId(_periodManager.GetPeriod());
            }
            return base.OnActionExecutionAsync(context, next);
        }
        [HttpGet(Name = "GetPayTypes")]
        [ProducesResponseType(typeof(IEnumerable<PayTypeViewModel>), 200)]
        [SwaggerResponseExample(200, typeof(PayTypeViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> GetAll()
        {
            var payTypes = await _payTypeRep.GetAllNoTracking().ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<PayTypeViewModel>>(payTypes);
            return Ok(viewModels);
        }

        [HttpGet("{id}", Name = "GetPayType")]
        [ProducesResponseType(typeof(PayTypeViewModel), 200)]
        [SwaggerResponseExample(200, typeof(PayTypeViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Get(long id)
        {
             var payType = await _payTypeRep.GetAsync(id);
             if (payType == null)
             {
                 return NotFound("Pay type not found !");
             }
             var viewModel = AutoMapper.Mapper.Map<PayTypeViewModel>(payType);
             return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PayTypeViewModel), 200)]
        [SwaggerResponseExample(200, typeof(PayTypeViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(AddPayTypeViewModel), typeof(AddPayTypeViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Post([FromBody]AddPayTypeViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (await _payTypeRep.IsExistNameAsync(model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
                return BadRequest(ModelState.GetWithErrorsKey());
            }
           

            var payType = AutoMapper.Mapper.Map<PayType>(model);
            payType.Name = model.Name;
            var affectedRows = await _payTypeRep.AddAsync(payType);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<PayTypeViewModel>(payType);

                return CreatedAtRoute("GetPayType", new { id = payType.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PayTypeViewModel), 200)]
        [SwaggerResponseExample(200, typeof(PayTypeViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(EditPayTypeViewModel), typeof(EditPayTypeViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Put(long id, [FromBody]EditPayTypeViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            var payType = await _payTypeRep.GetAsync(id);
            if (payType == null)
            {
                return NotFound(Resources.Bills.BillResource.BillTypeNotFound);
            }

            if (await _payTypeRep.IsExistNameAsync(payType.Id, model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            payType = AutoMapper.Mapper.Map(model, payType);

            var affectedRows = 0;

            affectedRows = await _payTypeRep.EditAsync(payType);

            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<PayTypeViewModel>(payType);

                return Ok(viewModel);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(PayTypeViewModel), 200)]
        [SwaggerResponseExample(200, typeof(PayTypeViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Delete(long id)
        {
            var payType = await _payTypeRep.GetAsync(id);
            if (payType == null)
            {
                return NotFound(Resources.Bills.BillResource.BillTypeNotFound);
            }

            var affectedRows = await _payTypeRep.DeleteAsync(payType);
            if (affectedRows == -1)
            {
                return BadRequest(Resources.Bills.BillResource.CanNotDeleteBillType);
            }
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<PayTypeViewModel>(payType);
                return Ok(viewModel);
            }
            return BadRequest();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _payTypeRep.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}