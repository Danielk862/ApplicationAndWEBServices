﻿using Orders.Backend.DTOs;
using Orders.Backend.Repositories.Interfaces;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.Entites;
using Orders.Shared.Responses;

namespace Orders.Backend.UnitsOfWork.Implementations
{
    public class CategoriesUnitOfWork : GenericUnitOfWork<Category>, ICategoriesUnitOfWork
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesUnitOfWork(IGenericRepository<Category> repository, ICategoriesRepository categoriesRepository) : base(repository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public override async Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO paginationDTO) =>
            await _categoriesRepository.GetAsync(paginationDTO);
        public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO paginationDTO) =>
            await _categoriesRepository.GetTotalRecordsAsync(paginationDTO);
    }
}
