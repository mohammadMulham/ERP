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
using ERPAPI.ViewModels.FinancialPeriods;
using ERPAPI.SwaggerExamples.FinancialPeriods;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]")]
    public class FinancialPeriodsController : BaseController
    {
        private IFinancialPeriodRepository _financialPeriodRepo;
        private ICompanyRepository _companyRepository;

        public FinancialPeriodsController(
            ILanguageManager languageManager,
            IFinancialPeriodRepository FinancialPeriodRepo,
            ICompanyRepository companyRepository) : base(languageManager)
        {
            _financialPeriodRepo = FinancialPeriodRepo;
            _companyRepository = companyRepository;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                _financialPeriodRepo.SetLoggedInUserId(GetUserId());
                _financialPeriodRepo.SetIsAdmin(GetIsUserAdmin());
                _financialPeriodRepo.SetLoggedInUserName(GetUserUserName());
            }
            return base.OnActionExecutionAsync(context, next);
        }

        [HttpGet(Name = "GetFinancialPeriods")]
        [ProducesResponseType(typeof(IEnumerable<FinancialPeriodViewModel>), 200)]
        [SwaggerResponseExample(200, typeof(FinancialPeriodViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> GetAll()
        {
            var financialPeriods = await _financialPeriodRepo.GetAllNoTracking().ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<FinancialPeriodViewModel>>(financialPeriods);
            return Ok(viewModels);
        }

        [HttpGet("{id}", Name = "GetFinancialPeriod")]
        [ProducesResponseType(typeof(FinancialPeriodViewModel), 200)]
        [SwaggerResponseExample(200, typeof(FinancialPeriodViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Get(long id)
        {
            var financialPeriod = await _financialPeriodRepo.GetAsync(id);
            if (financialPeriod == null)
            {
                return NotFound(Resources.FinancialPeriods.FinancialPeriodResource.FinancialPeriodNotFound);
            }
            var viewModel = AutoMapper.Mapper.Map<FinancialPeriodViewModel>(financialPeriod);
            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(FinancialPeriodViewModel), 200)]
        [SwaggerResponseExample(200, typeof(FinancialPeriodViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(AddFinancialPeriodViewModel), typeof(AddFinancialPeriodViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Post([FromBody]AddFinancialPeriodViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var company = await _companyRepository.GetAsync(model.CompanyId);
            if (company == null)
            {
                return NotFound(Resources.Companies.CompanyResource.CompanyNotFound);
            }

            if (await _financialPeriodRepo.IsExistNameAsync(model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            var financialPeriod = new FinancialPeriod(model.Name, company.Id, model.StartDate, model.EndtDate);

            var affectedRows = await _financialPeriodRepo.AddAsync(financialPeriod);
            if (affectedRows > 0)
            {
                _financialPeriodRepo.LoadReferences(financialPeriod);
                var viewModel = AutoMapper.Mapper.Map<FinancialPeriodViewModel>(financialPeriod);

                return CreatedAtRoute("GetFinancialPeriod", new { id = financialPeriod.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FinancialPeriodViewModel), 200)]
        [SwaggerResponseExample(200, typeof(FinancialPeriodViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Put(long id, [FromBody]EditFinancialPeriodViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            var financialPeriod = await _financialPeriodRepo.GetAsync(id);
            if (financialPeriod == null)
            {
                return NotFound(Resources.FinancialPeriods.FinancialPeriodResource.FinancialPeriodNotFound);
            }

            if (await _financialPeriodRepo.IsExistNameAsync(financialPeriod.Id, model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            financialPeriod.Name = model.Name;

            var affectedRows = await _financialPeriodRepo.EditAsync(financialPeriod);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<FinancialPeriodViewModel>(financialPeriod);

                return Ok(viewModel);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(FinancialPeriodViewModel), 200)]
        [SwaggerResponseExample(200, typeof(FinancialPeriodViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Delete(long id)
        {
            var financialPeriod = await _financialPeriodRepo.GetAsync(id);
            if (financialPeriod == null)
            {
                return NotFound(Resources.FinancialPeriods.FinancialPeriodResource.FinancialPeriodNotFound);
            }

            var affectedRows = await _financialPeriodRepo.DeleteAsync(financialPeriod);
            if (affectedRows == -1)
            {
                return BadRequest(Resources.FinancialPeriods.FinancialPeriodResource.CanNotDeleteFinancialPeriod);
            }
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<FinancialPeriodViewModel>(financialPeriod);
                return Ok(viewModel);
            }
            return BadRequest();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _financialPeriodRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
