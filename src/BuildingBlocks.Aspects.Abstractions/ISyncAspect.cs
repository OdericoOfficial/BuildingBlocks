namespace BuildingBlocks.Aspects.Abstractions
{
    public interface ISyncAspect
    {
        void OnNext(AspectContext context, SyncAspectDelegate next);
    }
}