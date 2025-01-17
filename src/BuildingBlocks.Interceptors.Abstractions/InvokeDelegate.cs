namespace BuildingBlocks.Interceptors.Abstractions
{
    public delegate ValueTask InvokeDelegate<TContext>(TContext context, InvokeDelegate<TContext> next)
        where TContext : struct, IInvokeContext;
}
