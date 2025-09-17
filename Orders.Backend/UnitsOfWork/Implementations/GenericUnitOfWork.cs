using Orders.Backend.DTOs;
using Orders.Backend.Repositories.Interfaces;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.Responses;

namespace Orders.Backend.UnitsOfWork.Implementations
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
        public virtual async Task<ActionResponse<T>> AddAsync(T model) => await _repository.AddAsync(model);

        public virtual async Task<ActionResponse<T>> DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync() => await _repository.GetAsync();

        public virtual async Task<ActionResponse<T>> GetAsync(int id) => await _repository.GetAsync(id);

        public virtual async Task<ActionResponse<T>> UpdateAsync(T model) => await _repository.UpdateAsync(model);

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination) => await _repository.GetAsync(pagination);

        public virtual async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) => await _repository.GetTotalRecordsAsync(pagination);
        #endregion
    }
}
