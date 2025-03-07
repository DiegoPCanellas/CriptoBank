namespace Data.Common.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task AddAsync(T model);
        void Update(T model);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsNoTrackingAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(int id);
        Task<IEnumerable<T>> GetAllAsNoTrackingAsync(int id);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
