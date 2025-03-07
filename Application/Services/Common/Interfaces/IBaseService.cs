using Application.DTOs;
using Application.DTOs.Common;

namespace Application.Services.Common.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        void Commit();
        void Rollback();
    }
}
