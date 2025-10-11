using Orders.Shared.DTOs;
using Orders.Shared.Entites;
using Orders.Shared.Responses;

namespace Orders.Backend.Repositories.Interfaces
{
    public interface ICitiesRepository
    {
        Task<ActionResponse<IEnumerable<City>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);
    }
}
