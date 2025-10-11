using Orders.Shared.DTOs;
using Orders.Shared.Entites;
using Orders.Shared.Responses;

namespace Orders.Backend.UnitsOfWork.Interfaces
{
    public interface ICategoriesUnitOfWork
    {
        Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO paginationDTO);
        Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO paginationDTO);
    }
}
