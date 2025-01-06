namespace BuildingBlocks.Aspects.Abstractions
{
    public interface IAspect
    {
        ValueTask OnEntryAsync<TContext>(TContext context)
            where TContext : struct, IAspectContext;

        ValueTask OnSuccessAsync<TContext>(TContext context)
            where TContext : struct, IAspectContext;

        ValueTask OnExceptionAsync<TContext>(TContext context)
            where TContext : struct, IAspectContext;

        ValueTask OnExitAsync<TContext>(TContext context)
            where TContext : struct, IAspectContext;
    }
}
