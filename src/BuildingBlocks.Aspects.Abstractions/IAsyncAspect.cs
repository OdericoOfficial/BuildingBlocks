namespace BuildingBlocks.Aspects.Abstractions
{
    public interface IAsyncAspect : IAspect
    {
        Task OnNextAsync(AspectContext context, AsyncAspectDelegate next);
    }
}
