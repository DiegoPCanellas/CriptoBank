using Data.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Common
{
    //Repository base que deve ser herdado por todos os Repositories
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public DbSet<T> DbSet { get; set; }

        private readonly BankDbContext _dbContext;

        public BaseRepository(BankDbContext dbContext)
        {
            _dbContext = dbContext;

            DbSet = _dbContext.Set<T>();
        }

        public async Task AddAsync(T model)
        {
            await _dbContext.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }

        public void Update(T model)
        {
            _dbContext.Update(model);
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAllAsync(int id)
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsNoTrackingAsync(int id)
        {
            var list = await _dbContext.Set<T>().ToListAsync();

            if (list != null)
                _dbContext.Entry(list).State = EntityState.Detached;

            return list;
        }

        public async Task<T> GetByIdAsNoTrackingAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);

            if (entity != null)
                _dbContext.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        
        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void RollbackTransaction() 
        { 
            _dbContext.Database.RollbackTransaction();
        }

    }
}
