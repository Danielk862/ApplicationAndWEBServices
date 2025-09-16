using Microsoft.EntityFrameworkCore;
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
            await CheckCountriesFullAsync();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
        }

        private async Task CheckCountriesFullAsync()
        {
            if (!_dataContext.Countries.Any())
            {
                var countriesSqlScript = File.ReadAllText("Data\\CountriesStatesCities.sql");
                await _dataContext.Database.ExecuteSqlRawAsync(countriesSqlScript);
            }
        }

        private async Task CheckCountriesAsync()
        {
            if (!_dataContext.Countries.Any())
            {
                _dataContext.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States = [
                        new State()
                        {
                            Name = "Antioquia",
                            Cities = [
                                new City() { Name = "Medellín"},
                                new City() { Name = "Itagui"},
                                new City() { Name = "Envigado"},
                                new City() { Name = "Bello"},
                                new City() { Name = "Rionegro"}
                            ]
                        },
                        new State()
                        {
                            Name = "Bogotá",
                            Cities = [
                                new City() { Name = "Usaquen"},
                                new City() { Name = "Chapinero"},
                                new City() { Name = "Santa fe"},
                                new City() { Name = "Usme"},
                                new City() { Name = "Bosa"}
                            ]
                        }
                    ],
                });
                _dataContext.Countries.Add(new Country
                {
                    Name = "Estados Unidos",
                    States = [
                        new State()
                        {
                            Name = "Florida",
                            Cities = [
                                new City() { Name = "Orlando" },
                                new City() { Name = "Miami" },
                                new City() { Name = "Tampa" },
                                new City() { Name = "Fort Lauderdale" },
                                new City() { Name = "Key Wets" }
                            ]
                        },
                        new State() {
                            Name = "Texas",
                            Cities = [
                                new City() { Name = "Houston" },
                                new City() { Name = "San antonio" },
                                new City() { Name = "Dallas" },
                                new City() { Name = "Austin" },
                                new City() { Name = "El paso" },
                            ]
                        }
                    ]
                });
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
