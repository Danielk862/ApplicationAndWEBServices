using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.DTOs;
using Orders.Backend.Helpers;
using Orders.Backend.Repositories.Interfaces;
using Orders.Shared.Entites;
using Orders.Shared.Responses;

namespace Orders.Backend.Repositories.Implementations
{
    public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {
        private readonly DataContext _context;

        public CategoriesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO paginationDTO)
        {
            var queryble = _context.Categories
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(paginationDTO.Filter))
            {
                queryble = queryble.Where(x => x.Name.ToLower().Contains(paginationDTO.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Category>>
            {
                WasSuccess = true,
                Result = await queryble
                    .OrderBy(x => x.Name)
                    .Paginate(paginationDTO)
                    .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO paginationDTO)
        {
            var queryable = _context.Categories.AsQueryable();

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
