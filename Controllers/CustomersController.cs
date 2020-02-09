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
using ERPAPI.ViewModels.Customers;
using ERPAPI.SwaggerExamples.Customers;
using ERPAPI.SwaggerExamples.Items;
using ERPAPI.ViewModels.Reports.CustomerAccountStatement;
using ERPAPI.ViewModels;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]")]
    public class CustomersController : BaseController
    {
        #region variables
        private ICustomerRepository _customerRepo;
        private ICustomerTypeRepository _customerTypeRepo;
        #endregion

        public CustomersController(
            ILanguageManager languageManager,
            ICustomerRepository customerRepo,
            ICustomerTypeRepository customerTypeRepo) : base(languageManager)
        {
            _customerRepo = customerRepo;
            _customerTypeRepo = customerTypeRepo;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                _customerRepo.SetLoggedInUserId(GetUserId());
                _customerTypeRepo.SetLoggedInUserId(GetUserId());

                _customerRepo.SetIsAdmin(GetIsUserAdmin());
                _customerTypeRepo.SetIsAdmin(GetIsUserAdmin());
            }
            return base.OnActionExecutionAsync(context, next);
        }

        [HttpGet(Name = "GetCustomers")]
        [ProducesResponseType(typeof(IEnumerable<CustomerViewModel>), 200)]
        [SwaggerResponseExample(200, typeof(CustomerViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerRepo.GetAllNoTracking().ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<CustomerViewModel>>(customers);
            return Ok(viewModels);
        }

        [HttpGet("search/{key?}", Name = "SearchCustomers")]
        [ProducesResponseType(typeof(IEnumerable<SearchWithChildViewModel>), 200)]
        public async Task<IActionResult> Search(string key = "")
        {
            var customers = await _customerRepo.Search(key).ToListAsync();
            var viewModels = customers.Select(customer => new SearchWithChildViewModel
            {
                Id = customer.Number,
                Label = customer.NumberFullName
            });
            return Ok(viewModels);
        }
        
        [HttpGet("{id}", Name = "GetCustomer")]
        [ProducesResponseType(typeof(CustomerViewModel), 200)]
        [SwaggerResponseExample(200, typeof(CustomerViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Get(long id)
        {
            var customer = await _customerRepo.GetAsync(id);
            if (customer == null)
            {
                return NotFound(Resources.Customers.CustomerResource.CustomerNotFound);
            }
            var viewModel = AutoMapper.Mapper.Map<CustomerViewModel>(customer);
            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerViewModel), 200)]
        [SwaggerResponseExample(200, typeof(CustomerViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(AddCustomerViewModel), typeof(AddCustomerViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Post([FromBody]AddCustomerViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var customerType = await _customerTypeRepo.GetAsync(model.CustomerTypeId);
            if (customerType == null)
            {
                return NotFound(Resources.Customers.CustomerResource.CustomerTypeNotFound);
            }

            if (await _customerRepo.IsExistNameAsync(model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            var customer = new Customer(model.Name, customerType.Id, model.Note);

            var affectedRows = await _customerRepo.AddAsync(customer);
            if (affectedRows > 0)
            {
                _customerRepo.LoadReferences(customer);

                var viewModel = AutoMapper.Mapper.Map<CustomerViewModel>(customer);

                return CreatedAtRoute("GetCustomer", new { id = customer.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CustomerViewModel), 200)]
        [SwaggerResponseExample(200, typeof(CustomerViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Delete(long id)
        {
            var customer = await _customerRepo.GetAsync(id);
            if (customer == null)
            {
                return NotFound(Resources.Customers.CustomerResource.CustomerNotFound);
            }

            var affectedRows = await _customerRepo.DeleteAsync(customer);
            if (affectedRows == -1)
            {
                return BadRequest(Resources.Customers.CustomerResource.CanNotDeleteCustomer);
            }
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<CustomerViewModel>(customer);
                return Ok(viewModel);
            }
            return BadRequest();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _customerRepo.Dispose();
                _customerTypeRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
