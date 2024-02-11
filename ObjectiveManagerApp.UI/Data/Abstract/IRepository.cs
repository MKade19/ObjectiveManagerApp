namespace ObjectiveManagerApp.UI.Data.Abstract
{
    public interface IRepository<T>
    {
        Task CreateOneAsync(T entity);
        IAsyncEnumerable<IEnumerable<T>> GetChunkAsync();
        Task UpdateByIdAsync(T entity);
        Task DeleteOneAsync(T entity);
    }
}


