namespace WebApp.Domain.Interfaces
{
    public interface IService<T> where T : class
    {
        public Task Srv(T entity);
    }
}
