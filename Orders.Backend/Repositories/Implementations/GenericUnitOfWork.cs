using Orders.Backend.Repositories.Interfaces;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.Responses;

namespace Orders.Backend.Repositories.Implementations
{
    public class GenericUnitOfWork<T> : IGenericUnitOfWork<T> where T : class
    {
        #region Internals
        private readonly IGenericRepository<T> _repository;
        #endregion

        #region Constructor
        public GenericUnitOfWork(IGenericRepository<T> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public async Task<ActionResponse<T>> AddAsync(T model) => await _repository.AddAsync(model);

        public async Task<ActionResponse<T>> DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async Task<ActionResponse<IEnumerable<T>>> GetAsync() => await _repository.GetAsync();

        public async Task<ActionResponse<T>> GetAsync(int id) => await _repository.GetAsync(id);

        public async Task<ActionResponse<T>> UpdateAsync(T model) => await _repository.UpdateAsync(model);
        #endregion
    }
}
