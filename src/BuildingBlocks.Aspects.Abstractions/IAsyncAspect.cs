namespace BuildingBlocks.Aspects.Abstractions
{
    public interface IAsyncAspect
    {
        Task OnNextAsync(IAspectContext context, AsyncAspectDelegate next);
    }
}
