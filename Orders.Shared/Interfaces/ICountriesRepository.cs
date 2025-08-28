using Orders.Shared.Entites;
using Orders.Shared.Responses;

namespace Orders.Shared.Interfaces
{
    public interface ICountriesRepository
    {
        Task<ActionResponse<Country>> GetAsync(int id);
        Task<ActionResponse<IEnumerable<Country>>> GetAsync();
    }
}
