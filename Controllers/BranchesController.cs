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
using ERPAPI.ViewModels.CostCenters;
using Microsoft.EntityFrameworkCore;
using ERPAPI.Extentions;
using Swashbuckle.AspNetCore.Examples;
using Newtonsoft.Json.Converters;
using ERPAPI.SwaggerExamples.CostCenters;
using Microsoft.AspNetCore.Authorization;
using ERPAPI.ViewModels.Branchs;
using ERPAPI.SwaggerExamples.Branchs;
using ERPAPI.ViewModels;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]")]
    public class BranchesController : BaseController
    {
        private IBranchRepository _branchRepo;

        public BranchesController(ILanguageManager languageManager, IBranchRepository branchRepo) : base(languageManager)
        {
            _branchRepo = branchRepo;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                _branchRepo.SetLoggedInUserId(GetUserId());

                _branchRepo.SetIsAdmin(GetIsUserAdmin());
            }
            return base.OnActionExecutionAsync(context, next);
        }

        [HttpGet(Name = "GetBranches")]
        [ProducesResponseType(typeof(IEnumerable<BranchViewModel>), 200)]
        [SwaggerResponseExample(200, typeof(BranchViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> GetAll()
        {
            var branchs = await _branchRepo.GetAllNoTracking().ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<BranchViewModel>>(branchs);
            return Ok(viewModels);
        }

        [HttpGet("search/{key?}", Name = "SearchBranches")]
        [ProducesResponseType(typeof(IEnumerable<SearchViewModel>), 200)]
        public async Task<IActionResult> Search(string key = "")
        {
            var branches = await _branchRepo.Search(key).ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<SearchViewModel>>(branches);
            return Ok(viewModels);
        }

        [HttpGet("generatecode/{id?}", Name = "GenerateBranchCode")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> NewCode(long? id)
        {
            Guid? parentId = null;
            if (id.HasValue)
            {
                var costCenter = await _branchRepo.GetAsync(id.Value);
                if (costCenter == null)
                {
                    return NotFound(Resources.Branchs.BranchResource.BranchNotFound);
                }
                parentId = costCenter.Id;
            }
            var newCode = await _branchRepo.GetNewCodeAsync(parentId);
            return Ok(newCode);
        }

        [HttpGet("{id}", Name = "GetBranch")]
        [ProducesResponseType(typeof(BranchViewModel), 200)]
        [SwaggerResponseExample(200, typeof(BranchViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Get(long id)
        {
            var branch = await _branchRepo.GetAsync(id);
            if (branch == null)
            {
                return NotFound(Resources.Branchs.BranchResource.BranchNotFound);
            }
            var viewModel = AutoMapper.Mapper.Map<BranchViewModel>(branch);
            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BranchViewModel), 200)]
        [SwaggerResponseExample(200, typeof(BranchViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(AddBranchViewModel), typeof(AddBranchViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Post([FromBody]AddBranchViewModel model)
        {
            Guid? parentId = null;

            if (model == null)
            {
                return BadRequest();
            }

            if (model.ParentBranchId.HasValue)
            {
                var parentBranch = await _branchRepo.GetAsync(model.ParentBranchId.Value);
                if (parentBranch == null)
                {
                    return NotFound(Resources.Branchs.BranchResource.ParentBranchNotFound);
                }
                parentId = parentBranch.Id;
            }

            if (await _branchRepo.IsExistCodeAsync(model.Code))
            {
                ModelState.AddModelError("Code", Resources.Global.Common.ThisCodeExist);
            }
            if (await _branchRepo.IsExistNameAsync(model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            var branch = new Branch(model.Name, model.Code,model.Note);
            if (parentId.HasValue)
            {
                branch.ParentId = parentId.Value;
            }

            var affectedRows = await _branchRepo.AddAsync(branch);
            if (affectedRows > 0)
            {
                _branchRepo.LoadReferences(branch);

                var viewModel = AutoMapper.Mapper.Map<BranchViewModel>(branch);

                return CreatedAtRoute("GetBranch", new { id = branch.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BranchViewModel), 200)]
        [SwaggerResponseExample(200, typeof(BranchViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Delete(long id)
        {
            var branch = await _branchRepo.GetAsync(id);
            if (branch == null)
            {
                return NotFound(Resources.Branchs.BranchResource.BranchNotFound);
            }

            var affectedRows = await _branchRepo.DeleteAsync(branch);
            if (affectedRows == -1)
            {
                return BadRequest(Resources.Branchs.BranchResource.CanNotDeleteBranch);
            }
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<BranchViewModel>(branch);
                return Ok(viewModel);
            }
            return BadRequest();
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _branchRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
