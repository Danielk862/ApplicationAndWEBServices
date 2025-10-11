using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Shared.DTOs;
using Orders.Backend.Helpers;
using Orders.Backend.Repositories.Interfaces;
using Orders.Shared.Responses;

namespace Orders.Backend.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Internals
        private readonly DataContext _dataContext;
        private readonly DbSet<T> _entity;
        #endregion

        #region Constructor
        public GenericRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            _entity = dataContext.Set<T>();
        }
        #endregion

        #region Methods
        public virtual async Task<ActionResponse<T>> AddAsync(T entity)
        {
            _dataContext.Add(entity);

            try
            {
                await _dataContext.SaveChangesAsync();
                return new ActionResponse<T>
                {
                    WasSuccess = true,
                    Result = entity
                };
            }
            catch (DbUpdateException) 
            {
                return DbUpdateExceptionActionResponse();
            }
            catch (Exception ex)
            {
                return ExceptionActionResponse(ex);
            }
        }

        public virtual async Task<ActionResponse<T>> DeleteAsync(int id)
        {
            var row = await _entity.FindAsync(id);

            if (row == null)
            {
                return new ActionResponse<T>
                {
                    Messages = "Registro no encontrado"
                };
            }

            _entity.Remove(row);

            try
            {
                await _dataContext.SaveChangesAsync();
                return new ActionResponse<T>
                {
                    WasSuccess = true,
                };
            }
            catch
            {
                return new ActionResponse<T>
                {
                    Messages = "No se puede borrar, por que tiene registros relacionados."
                };
            }
        }

        public virtual async Task<ActionResponse<T>> GetAsync(int id)
        {
            var row = await _entity.FindAsync(id);

            if (row != null)
            {
                return new ActionResponse<T>
                {
                    WasSuccess = true,
                    Result = row
                };
            }

            return new ActionResponse<T>
            {
                Messages = "Registro no encontrado"
            };
        }

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync() =>
             new ActionResponse<IEnumerable<T>>
            {
                WasSuccess = true,
                Result = await _entity.ToListAsync()
            };
        
        public virtual async Task<ActionResponse<T>> UpdateAsync(T entity)
        {
            _dataContext.Update(entity);

            try
            {
                await _dataContext.SaveChangesAsync();
                return new ActionResponse<T>
                {
                    WasSuccess = true,
                    Result = entity
                };
            }
            catch (DbUpdateException)
            {
                return DbUpdateExceptionActionResponse();
            }
            catch (Exception ex)
            {
                return ExceptionActionResponse(ex);
            }
        }

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _entity.AsQueryable();

            return new ActionResponse<IEnumerable<T>>
            {
                WasSuccess = true,
                Result = await queryable
                .Paginate(pagination)
                .ToListAsync()
            };
        }

        public virtual async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagintaion)
        {
            var queryable = _entity.AsQueryable();
            double count = await queryable.CountAsync();

            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = (int)count
            };
        }
        #endregion

        #region Private Methods
        private ActionResponse<T> ExceptionActionResponse(Exception ex) =>       
             new ActionResponse<T> { Messages = ex.Message };
        
        private ActionResponse<T> DbUpdateExceptionActionResponse() => 
            new ActionResponse<T> { Messages = "Ya existe el registro que estas intentando crear" };
        #endregion
    }
}
