using System.Threading.Tasks;

namespace BuildingBlocks.Interceptors.Abstractions
{
    public interface IInterceptor
    {
        ValueTask OnNextAsync<TContext>(TContext context, InvokeDelegate<TContext> next)
            where TContext : struct, IInvokeContext;
    }
}
