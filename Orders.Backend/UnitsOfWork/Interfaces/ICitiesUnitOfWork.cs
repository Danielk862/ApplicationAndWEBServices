using Orders.Shared.DTOs;
using Orders.Shared.Entites;
using Orders.Shared.Responses;

namespace Orders.Backend.UnitsOfWork.Interfaces
{
    public interface ICitiesUnitOfWork
    {
        Task<ActionResponse<IEnumerable<City>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);
    }
}
