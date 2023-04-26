using ShoppingWebAPI.DAL.Entities;

namespace ShoppingWebAPI.DAL
{
    public class SeederDb
    {
        private readonly DataBaseContext _context;
        public SeederDb(DataBaseContext context)
        {
            _context = context;
        }

        public async Task SeederAsync()
        {
            await _context.Database.EnsureCreatedAsync(); //Esta línea me ayuda a crear mi BD de forma automática
            await PopulateCategoriesAsync();
            await PopulateCountriesAsync();

            await _context.SaveChangesAsync();
        }

        private async Task PopulateCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    CreatedDate = DateTime.Now,
                    Name = "Colombia",
                    States = new List<State>()
                    {
                        new State
                        {
                            CreatedDate = DateTime.Now,
                            Name = "Antioquia",
                            Cities = new List<City>()
                            {
                                new City { Name = "Medellín", CreatedDate = DateTime.Now },
                                new City { Name = "Envigado", CreatedDate = DateTime.Now },
                                new City { Name = "Bello", CreatedDate = DateTime.Now },
                                new City { Name = "Itagüí", CreatedDate = DateTime.Now },
                                new City { Name = "Barbosa", CreatedDate = DateTime.Now },
                                new City { Name = "Copacabana", CreatedDate = DateTime.Now },
                                new City { Name = "Girardota", CreatedDate = DateTime.Now },
                                new City { Name = "Sabaneta", CreatedDate = DateTime.Now },
                            }
                        },

                        new State
                        {
                            CreatedDate = DateTime.Now,
                            Name = "Cundinamarca",
                            Cities = new List<City>()
                            {
                                new City { Name = "Bogotá", CreatedDate = DateTime.Now },
                                new City { Name = "Engativá", CreatedDate = DateTime.Now },
                                new City { Name = "Fusagasugá", CreatedDate = DateTime.Now },
                                new City { Name = "Villeta", CreatedDate = DateTime.Now },
                            }
                        }
                    }
                });

                _context.Countries.Add(new Country
                {
                    CreatedDate = DateTime.Now,
                    Name = "Argentina",
                    States = new List<State>()
                    {
                        new State
                        {
                            CreatedDate = DateTime.Now,
                            Name = "Buenos Aires",
                            Cities = new List<City>()
                            {
                                new City { Name = "Alberti", CreatedDate = DateTime.Now },
                                new City { Name = "Avellaneda", CreatedDate = DateTime.Now },
                                new City { Name = "Bahía Blanca", CreatedDate = DateTime.Now },
                                new City { Name = "Ezeiza", CreatedDate = DateTime.Now },
                            }
                        },

                        new State
                        {
                            CreatedDate = DateTime.Now,
                            Name = "La Pampa",
                            Cities = new List<City>()
                            {
                                new City { Name = "Parera", CreatedDate = DateTime.Now },
                                new City { Name = "Santa Isabel", CreatedDate = DateTime.Now },
                                new City { Name = "Puelches", CreatedDate = DateTime.Now },
                                new City { Name = "La Adela", CreatedDate = DateTime.Now },
                            }
                        }
                    }
                });
            }
        }

        private async Task PopulateCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Tecnología", Description = "Elementos de tech: laptops, celulares, tablets.", CreatedDate = DateTime.Now });
                _context.Categories.Add(new Category { Name = "Indumentaria", Description = "Ropa y calzado", CreatedDate = DateTime.Now });
                _context.Categories.Add(new Category { Name = "Cosméticos", Description = "Todo para la belleza de la mujer", CreatedDate = DateTime.Now });
                _context.Categories.Add(new Category { Name = "Gamers", Description = "Consolas: PS5, XBOX", CreatedDate = DateTime.Now });
                _context.Categories.Add(new Category { Name = "Alimentos", Description = "Grano, legumbre, carne, etc", CreatedDate = DateTime.Now });
            }
        }
    }
}
