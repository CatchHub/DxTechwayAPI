namespace ProductAppAPI.Mappers
{
    public class Mapper
    {
        private readonly IServiceProvider services;

        public Mapper(IServiceProvider services)
        {
            this.services = services;
        }

        public K Map<T, K>(T entity)
        {
            var mapper = services.GetRequiredService<IMapper<T, K>>();
            return mapper.Map(entity);
        }
    }
}
