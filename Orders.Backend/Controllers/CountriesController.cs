using Microsoft.AspNetCore.Mvc;
using Orders.Backend.DTOs;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.Entites;

namespace Orders.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : GenericController<Country>
    {
        private readonly ICountriesUnitOfWork _unitOfWork;

        public CountriesController(IGenericUnitOfWork<Country> unit, ICountriesUnitOfWork unitOfWork) : base(unit)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync()
        {
            var response = await _unitOfWork.GetAsync();

            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var response = await _unitOfWork.GetAsync(id);

            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Messages);
        }

        [HttpGet("paginated")]
        public override async Task<IActionResult> GetAsync(PaginationDTO pagination)
        {
            var response = await _unitOfWork.GetAsync(pagination);

            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }
    }
}
