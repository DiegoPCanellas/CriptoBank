using Application.Services.Common.Interfaces;
using Data.Common.Interfaces;

namespace Application.Services.Common
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public void OpenTransaction()
        {
            _repository.BeginTransaction();
        }

        public void Commit()
        {
            _repository.CommitTransaction();
        }

        public void Rollback()
        {
            _repository.RollbackTransaction();
        }
    }
}
