using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.DTOs;
using Orders.Backend.Helpers;
using Orders.Backend.Repositories.Interfaces;
using Orders.Shared.Entites;
using Orders.Shared.Responses;

namespace Orders.Backend.Repositories.Implementations
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly DataContext _dataContext;

        public CountriesRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync()
        {
            var countries = await _dataContext.Countries
                .OrderBy(x => x.Name)
                .ToListAsync();

            return new ActionResponse<IEnumerable<Country>>
            {
                WasSuccess = true,
                Result = countries
            };
        }

        public override async Task<ActionResponse<Country>> GetAsync(int id)
        {
            var country = await _dataContext.Countries
                .Include(x => x.States!)
                .ThenInclude(x => x.Cities)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (country == null)
            {
                return new ActionResponse<Country>
                {
                    Messages = "País no existe"
                };
            }

            return new ActionResponse<Country>
            {
                WasSuccess = true,
                Result = country
            };
        }
    
        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination)
        {
            var queryble = _dataContext.Countries
                .Include(x => x.States)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryble = queryble.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Country>>
            {
                WasSuccess = true,
                Result = await queryble
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO paginationDTO)
        {
            var queryable = _dataContext.Countries.AsQueryable();

            if (!string.IsNullOrWhiteSpace(paginationDTO.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(paginationDTO.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();

            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = (int)count
            };
        }
    }
}
