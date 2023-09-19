using ProductAppAPI.Entities;
using ProductListApp.Domain.Entities;
using ProductWebAPI.DTOs;
using ProductWebAPI.DTOs.UserDTOs;
using ProductWebAPI.Mappers.ProductsMappers;
using ProductWebAPI.Mappers.UsersMappers;

namespace ProductAppAPI.Mappers
{
    public static class MappersController
    {
        public static void RegisterMappers(IServiceCollection services)
        {
            RegisterProductMappers(services);
            RegisterUserMappers(services);
        }

        private static void RegisterProductMappers(IServiceCollection services)
        {
            services.AddScoped<
                IMapper<Product, ProductsDTO>,
                ProductsResponseMapper>();

            services.AddScoped<
                IMapper<ProductsDTO, Product>,
                ProductsRequestMapper>();
        }

        private static void RegisterUserMappers(IServiceCollection services)
        {
            services.AddScoped<
                IMapper<UserRegisterDTO, User >,
                UserRegisterRequestMapper>();
        }
    }
}
