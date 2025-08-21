using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
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
                    WasSuccess = true,
                    Messages = "Registro no encontrado"
                };
            }

            try
            {
                _entity.Remove(row);
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
                    WasSuccess = false,
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
                WasSuccess = false,
                Messages = "Registro no encontrado"
            };
        }

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync()
        {
            return new ActionResponse<IEnumerable<T>>
            {
                WasSuccess = true,
                Result = await _entity.ToListAsync()
            };
        }

        public virtual async Task<ActionResponse<T>> UpdateAsync(T entity)
        {
            try
            {
                _dataContext.Update(entity);
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
        #endregion

        #region Private Methods
        private ActionResponse<T> ExceptionActionResponse(Exception ex)
        {
            return new ActionResponse<T>
            {
                WasSuccess = false,
                Messages = ex.Message
            };
        }

        private ActionResponse<T> DbUpdateExceptionActionResponse()
        {
            return new ActionResponse<T>
            {
                WasSuccess = false,
                Messages = "Ya existe el registro que estas intentando crear"
            };
        }
        #endregion
    }
}
