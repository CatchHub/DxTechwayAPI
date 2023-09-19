namespace ProductAppAPI.Mappers
{
    public interface IMapper<in T, out K>
    {
        K Map(T entity);
    }
}
