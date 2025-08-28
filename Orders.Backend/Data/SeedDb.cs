using Orders.Shared.Entites;

namespace Orders.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;

        public SeedDb(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
        }

        private async Task CheckCountriesAsync()
        {
            if (!_dataContext.Countries.Any())
            {
                _dataContext.Countries.Add(new Country { Name = "Colombia" });
                _dataContext.Countries.Add(new Country { Name = "Estados Unidos" });
            }

            await _dataContext.SaveChangesAsync();
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_dataContext.Categories.Any())
            {
                _dataContext.Categories.Add(new Category { Name = "Calzado" });
                _dataContext.Categories.Add(new Category { Name = "Tecnología" });
            }

            await _dataContext.SaveChangesAsync();
        }
    }
}
