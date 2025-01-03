namespace BuildingBlocks.Aspects.Abstractions
{
    public interface ISyncAspect
    {
        void OnNext(IAspectContext context, SyncAspectDelegate next);
    }
}