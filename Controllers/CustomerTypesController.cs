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
using ERPAPI.ViewModels.CustomerTypes;
using ERPAPI.SwaggerExamples.CustomerTypes;
using ERPAPI.SwaggerExamples.Items;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]")]
    public class CustomerTypesController : BaseController
    {
        private ICustomerTypeRepository _customerTypeRepo;

        public CustomerTypesController(
            ILanguageManager languageManager,
            ICustomerTypeRepository customerTypeRepo) : base(languageManager)
        {
            _customerTypeRepo = customerTypeRepo;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                _customerTypeRepo.SetLoggedInUserId(GetUserId());

                _customerTypeRepo.SetIsAdmin(GetIsUserAdmin());
            }
            return base.OnActionExecutionAsync(context, next);
        }

        [HttpGet(Name = "GetCustomerTypes")]
        [ProducesResponseType(typeof(IEnumerable<CustomerTypeViewModel>), 200)]
        [SwaggerResponseExample(200, typeof(CustomerTypeViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> GetAll()
        {
            var customerTypes = await _customerTypeRepo.GetAllNoTracking().ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<CustomerTypeViewModel>>(customerTypes);
            return Ok(viewModels);
        }

        [HttpGet("{id}", Name = "GetCustomerType")]
        [ProducesResponseType(typeof(CustomerTypeViewModel), 200)]
        [SwaggerResponseExample(200, typeof(CustomerTypeViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Get(long id)
        {
            var customerType = await _customerTypeRepo.GetAsync(id);
            if (customerType == null)
            {
                return NotFound(Resources.Customers.CustomerResource.CustomerTypeNotFound);
            }
            var viewModel = AutoMapper.Mapper.Map<CustomerTypeViewModel>(customerType);
            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerTypeViewModel), 200)]
        [SwaggerResponseExample(200, typeof(CustomerTypeViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(AddCustomerTypeViewModel), typeof(AddCustomerTypeViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Post([FromBody]AddCustomerTypeViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (await _customerTypeRepo.IsExistNameAsync(model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            var customerType = new CustomerType(model.Name, model.Note);

            var affectedRows = await _customerTypeRepo.AddAsync(customerType);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<CustomerTypeViewModel>(customerType);

                return CreatedAtRoute("GetCustomerType", new { id = customerType.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CustomerTypeViewModel), 200)]
        [SwaggerResponseExample(200, typeof(CustomerTypeViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Delete(long id)
        {
            var customerType = await _customerTypeRepo.GetAsync(id);
            if (customerType == null)
            {
                return NotFound(Resources.Customers.CustomerResource.CustomerTypeNotFound);
            }

            var affectedRows = await _customerTypeRepo.DeleteAsync(customerType);
            if (affectedRows == -1)
            {
                return BadRequest(Resources.Customers.CustomerResource.CanNotDeleteCustomerType);
            }
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<CustomerTypeViewModel>(customerType);
                return Ok(viewModel);
            }
            return BadRequest();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _customerTypeRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
