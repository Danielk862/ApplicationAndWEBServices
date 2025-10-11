using Microsoft.AspNetCore.Mvc;
using Orders.Backend.DTOs;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.Entites;

namespace Orders.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : GenericController<Category>
    {
        private readonly ICategoriesUnitOfWork _unitOfWork;

        public CategoriesController(IGenericUnitOfWork<Category> unit, ICategoriesUnitOfWork unitOfWork) : base(unit) 
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("paginated")]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO paginationDTO)
        {
            var response = await _unitOfWork.GetAsync(paginationDTO);

            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }


        [HttpGet("totalRecords")]
        public override async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO paginationDTO)
        {
            var response = await _unitOfWork.GetTotalRecordsAsync(paginationDTO);

            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }
    }
}
