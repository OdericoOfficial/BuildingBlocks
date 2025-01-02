namespace BuildingBlocks.Aspects.Abstractions
{
    public interface IAsyncAspect
    {
        Task OnNextAsync(AspectContext context, AsyncAspectDelegate next);
    }
}
