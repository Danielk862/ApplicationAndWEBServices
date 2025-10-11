using Orders.Backend.DTOs;
using Orders.Shared.Entites;
using Orders.Shared.Responses;

namespace Orders.Backend.Repositories.Interfaces
{
    public interface ICategoriesRepository
    {
        Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO paginationDTO);
        Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO paginationDTO);
    }
}
