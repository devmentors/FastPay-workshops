using System.Threading.Tasks;

namespace FastPay.Application.Abstractions
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}