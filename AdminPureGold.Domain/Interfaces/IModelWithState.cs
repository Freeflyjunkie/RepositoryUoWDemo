namespace AdminPureGold.Domain.Interfaces
{
    public interface IModelWithState
    {
        State EntityStateForGraphsUpdates { get; set; }
    }

    public enum State
    {        
        Unchanged,
        Added,
        Modified,
        Deleted
    }
}
