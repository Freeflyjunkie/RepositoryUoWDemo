using System.Data.Entity;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Repositories.EF.Helpers
{
    public static class ContextHelpers
    {
        public static void ApplyStateChanges(this DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<IModelWithState>())
            {
                IModelWithState modelWithState = entry.Entity;
                entry.State = StateHelpers.ConvertState(modelWithState.EntityStateForGraphsUpdates);
            }
        }
    }
}
