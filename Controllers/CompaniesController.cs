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
using ERPAPI.ViewModels.Companies;
using ERPAPI.SwaggerExamples.Companies;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]")]
    public class CompaniesController : BaseController
    {
        private ICompanyRepository _companyRepo;

        public CompaniesController(
            ILanguageManager languageManager,
            ICompanyRepository companyRepo) : base(languageManager)
        {
            _companyRepo = companyRepo;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                _companyRepo.SetLoggedInUserId(GetUserId());

                _companyRepo.SetIsAdmin(GetIsUserAdmin());
            }
            return base.OnActionExecutionAsync(context, next);
        }

        [HttpGet(Name = "GetCompanies")]
        [ProducesResponseType(typeof(IEnumerable<CompanyViewModel>), 200)]
        [SwaggerResponseExample(200, typeof(CompanyViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyRepo.GetAllNoTracking().ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<CompanyViewModel>>(companies);
            return Ok(viewModels);
        }

        [HttpGet("{id}", Name = "GetCompany")]
        [ProducesResponseType(typeof(CompanyViewModel), 200)]
        [SwaggerResponseExample(200, typeof(CompanyViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Get(long id)
        {
            var company = await _companyRepo.GetAsync(id);
            if (company == null)
            {
                return NotFound(Resources.Companies.CompanyResource.CompanyNotFound);
            }
            var viewModel = AutoMapper.Mapper.Map<CompanyViewModel>(company);
            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CompanyViewModel), 200)]
        [SwaggerResponseExample(200, typeof(CompanyViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(AddCompanyViewModel), typeof(AddCompanyViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Post([FromBody]AddCompanyViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (await _companyRepo.IsExistNameAsync(model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            var company = new Company(model.Name);

            var affectedRows = await _companyRepo.AddAsync(company);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<CompanyViewModel>(company);

                return CreatedAtRoute("GetCompany", new { id = company.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CompanyViewModel), 200)]
        [SwaggerResponseExample(200, typeof(CompanyViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Put(long id, [FromBody]EditCompanyViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            var company = await _companyRepo.GetAsync(id);
            if (company == null)
            {
                return NotFound(Resources.Companies.CompanyResource.CompanyNotFound);
            }

            if (await _companyRepo.IsExistNameAsync(company.Id, model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            company.Name = model.Name;

            var affectedRows = await _companyRepo.EditAsync(company);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<CompanyViewModel>(company);

                return Ok(viewModel);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CompanyViewModel), 200)]
        [SwaggerResponseExample(200, typeof(CompanyViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Delete(long id)
        {
            var company = await _companyRepo.GetAsync(id);
            if (company == null)
            {
                return NotFound(Resources.Companies.CompanyResource.CompanyNotFound);
            }

            var affectedRows = await _companyRepo.DeleteAsync(company);
            if (affectedRows == -1)
            {
                return BadRequest(Resources.Companies.CompanyResource.CanNotDeleteCompany);
            }
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<CompanyViewModel>(company);
                return Ok(viewModel);
            }
            return BadRequest();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _companyRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
