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
using ERPAPI.ViewModels;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]")]
    public class CostCentersController : BaseController
    {
        private ICostCenterRepository _costCenterRepo;

        public CostCentersController(ILanguageManager languageManager, ICostCenterRepository costCenterRepo) : base(languageManager)
        {
            _costCenterRepo = costCenterRepo;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                _costCenterRepo.SetLoggedInUserId(GetUserId());

                _costCenterRepo.SetIsAdmin(GetIsUserAdmin());
            }
            return base.OnActionExecutionAsync(context, next);
        }

        [HttpGet(Name = "GetCostCenters")]
        [ProducesResponseType(typeof(IEnumerable<CostCenterViewModel>), 200)]
        [SwaggerResponseExample(200, typeof(CostCenterViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> GetAll()
        {
            var costCenters = await _costCenterRepo.GetAllNoTracking().ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<CostCenterViewModel>>(costCenters);
            return Ok(viewModels);
        }

        [HttpGet("masternodes/", Name = "GetMasterCostCenters")]
        public async Task<IActionResult> MasterNodes()
        {
            var viewModels = await _costCenterRepo.GetAllNoTracking().Where(e => e.ParentId == null).Select(e => new
            {
                Id = e.Number,
                Name = e.CodeName,
                Children = e.CostCenters.Where(ee => ee.EntityStatus == EntityStatus.Active).OrderBy(ee => ee.Code).Select(ee => new
                {
                    Id = ee.Number,
                    Name = ee.CodeName,
                    Children = ee.CostCenters.Where(eee => eee.EntityStatus == EntityStatus.Active).OrderBy(eee => eee.Code).Select(eee => new
                    {
                        Id = eee.Number,
                        Name = eee.CodeName,
                        hasChildren = eee.CostCenters.LongCount(eeee => eeee.EntityStatus == EntityStatus.Active) > 0
                    })
                })
            }).ToListAsync();
            return Ok(viewModels);
        }

        [HttpGet("permissions/{id}", Name = "GetCostcenterPermissions")]
        public async Task<IActionResult> Permissions(long id)
        {
            var viewModel = new
            {
                Id = id,
                CanBeDeleted = new
                {
                    Value = await _costCenterRepo.CheckIfCanBeDeletedAsync(id),
                    Message = "لا يمكن حذفه لانه مرتبط بعنصر"
                },

            };
            return Ok(viewModel);
        }

        [HttpGet("nodes/{id}", Name = "GetNodeCostCenters")]
        public async Task<IActionResult> Nodes(long id)
        {
            var costCenter = await _costCenterRepo.GetAsync(id);
            if (costCenter == null)
            {
                return NotFound(Resources.CostCenters.CostCenterResource.CostCenterNotFound);
            }
            var viewModels = await _costCenterRepo.GetAllNoTracking().Where(e => e.ParentId == costCenter.Id)
                .Select(e => new
                {
                    Id = e.Number,
                    Name = e.CodeName,
                    hasChildren = e.CostCenters.LongCount(ee => ee.EntityStatus == EntityStatus.Active) > 0
                }).ToListAsync();
            return Ok(viewModels);
        }

        [HttpGet("search/{key?}", Name = "SearchCostCenters")]
        [ProducesResponseType(typeof(IEnumerable<SearchViewModel>), 200)]
        public async Task<IActionResult> Search(string key = "")
        {
            var stores = await _costCenterRepo.Search(key).ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<SearchViewModel>>(stores);
            return Ok(viewModels);
        }

        [HttpGet("generatecode/{id?}", Name = "GenerateCostCenterCode")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> NewCode(long? id)
        {
            Guid? parentId = null;
            if (id.HasValue)
            {
                var costCenter = await _costCenterRepo.GetAsync(id.Value);
                if (costCenter == null)
                {
                    return NotFound(Resources.CostCenters.CostCenterResource.CostCenterNotFound);
                }
                parentId = costCenter.Id;
            }
            var newCode = await _costCenterRepo.GetNewCodeAsync(parentId);
            return Ok(newCode);
        }

        [HttpGet("new/{id?}", Name = "NewCostCenter")]
        public async Task<IActionResult> NewCostCenter(long? id)
        {
            if (id.HasValue)
            {
                var costCenter = await _costCenterRepo.GetAsync(id.Value);
                if (costCenter == null)
                {
                    return NotFound(Resources.CostCenters.CostCenterResource.CostCenterNotFound);
                }
                var newCode = await _costCenterRepo.GetNewCodeAsync(costCenter.Id);
                var viewModel = new
                {
                    NewCode = newCode,
                    CostCenterCodeName = costCenter.CodeName,
                    CostCenterId = costCenter.Number
                };
                return Ok(viewModel);
            }
            else
            {
                var newCode = await _costCenterRepo.GetNewCodeAsync();
                var viewModel = new
                {
                    NewCode = newCode,
                };
                return Ok(viewModel);
            }
        }

        [HttpGet("{id}", Name = "GetCostCenter")]
        [ProducesResponseType(typeof(CostCenterViewModel), 200)]
        [SwaggerResponseExample(200, typeof(CostCenterViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Get(long id)
        {
            var costCenter = await _costCenterRepo.GetAsync(id);
            if (costCenter == null)
            {
                return NotFound(Resources.CostCenters.CostCenterResource.CostCenterNotFound);
            }
            var viewModel = AutoMapper.Mapper.Map<CostCenterViewModel>(costCenter);
            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CostCenterViewModel), 201)]
        [SwaggerResponseExample(201, typeof(CostCenterViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(AddCostCenterViewModel), typeof(AddCostCenterViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Post([FromBody]AddCostCenterViewModel model)
        {
            Guid? parentId = null;

            if (model == null)
            {
                return BadRequest();
            }

            if (model.ParentCostCenterId.HasValue)
            {
                var parentCostCenter = await _costCenterRepo.GetAsync(model.ParentCostCenterId.Value);
                if (parentCostCenter == null)
                {
                    return NotFound(Resources.CostCenters.CostCenterResource.ParentCostCenterNotFound);
                }
                parentId = parentCostCenter.Id;
            }

            if (await _costCenterRepo.IsExistCodeAsync(model.Code))
            {
                ModelState.AddModelError("Code", Resources.Global.Common.ThisCodeExist);
            }
            if (await _costCenterRepo.IsExistNameAsync(model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            var costCenter = new CostCenter(model.Code, model.Name, model.Note);
            if (parentId.HasValue)
            {
                costCenter.ParentId = parentId.Value;
            }

            var affectedRows = await _costCenterRepo.AddAsync(costCenter);
            if (affectedRows > 0)
            {
                _costCenterRepo.LoadReferences(costCenter);

                var viewModel = AutoMapper.Mapper.Map<CostCenterViewModel>(costCenter);

                return CreatedAtRoute("GetCostCenter", new { id = costCenter.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CostCenterViewModel), 200)]
        public async Task<IActionResult> Put(long id, [FromBody]EditCostCenterViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            var costcenter = await _costCenterRepo.GetAsync(id);
            if (costcenter == null)
            {
                return NotFound(Resources.CostCenters.CostCenterResource.CostCenterNotFound);
            }

            Guid? parentId = null;
            if (model.ParentCostCenterId.HasValue)
            {
                var parentCostCenter = await _costCenterRepo.GetAsync(model.ParentCostCenterId.Value);
                if (parentCostCenter == null)
                {
                    return NotFound(Resources.CostCenters.CostCenterResource.ParentCostCenterNotFound);
                }
                parentId = parentCostCenter.Id;
            }

            if (await _costCenterRepo.IsExistNameAsync(costcenter.Id, model.Name))
            {
                ModelState.AddModelError("Name", Resources.Global.Common.ThisNameExist);
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            costcenter.ParentId = parentId;
            costcenter.Code = model.Code;
            costcenter.Name = model.Name;
            costcenter.Note = model.Note;

            var affectedRows = await _costCenterRepo.EditAsync(costcenter);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<CostCenterViewModel>(costcenter);

                return Ok(viewModel);
            }
            return BadRequest();
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CostCenterViewModel), 200)]
        [SwaggerResponseExample(200, typeof(CostCenterViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Delete(long id)
        {
            var costcenter = await _costCenterRepo.GetAsync(id);
            if (costcenter == null)
            {
                return NotFound(Resources.CostCenters.CostCenterResource.CostCenterNotFound);
            }

            bool canBeDeleted = await _costCenterRepo.CheckIfCanBeDeletedAsync(id);
            if (!canBeDeleted)
            {
                return BadRequest(Resources.CostCenters.CostCenterResource.CanNotDeleteCostCenter);
            }

            var affectedRows = await _costCenterRepo.SetDeletedAsync(costcenter);

            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<CostCenterViewModel>(costcenter);
                return Ok(viewModel);
            }
            return BadRequest();
        }

        [HttpDelete("undelete/{id}")]
        [ProducesResponseType(typeof(CostCenterViewModel), 200)]
        [SwaggerResponseExample(200, typeof(CostCenterViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> UndDlete(long id)
        {
            var costcenter = await _costCenterRepo.GetEntityAsync(id);
            if (costcenter == null)
            {
                return NotFound(Resources.CostCenters.CostCenterResource.CostCenterNotFound);
            }

            var affectedRows = await _costCenterRepo.SetUnDeletedAsync(costcenter);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<CostCenterViewModel>(costcenter);
                return Ok(viewModel);
            }
            return BadRequest();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _costCenterRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
