using Orders.Shared.DTOs;
using Orders.Shared.Entites;
using Orders.Shared.Responses;

namespace Orders.Backend.Repositories.Interfaces
{
    public interface IStatesRepository
    {
        Task<ActionResponse<State>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<State>>> GetAsync();
        Task<ActionResponse<IEnumerable<State>>> GetAsync(PaginationDTO pagination);
        Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);
    }
}
