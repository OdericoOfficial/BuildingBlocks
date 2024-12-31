namespace BuildingBlocks.Aspects.Abstractions
{
    public interface ISyncAspect : IAspect
    {
        void OnNext(AspectContext context, SyncAspectDelegate next);
    }
}