namespace Application.Services.Common.Interfaces
{
    public interface IApiService
    {
        Task<decimal> GetCurrentEthPrice();
    }
}
