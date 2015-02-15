using AdminPureGold.Domain.Interfaces;
using System.Data.Entity;

namespace AdminPureGold.Repositories.EF.Helpers
{
    class StateHelpers
    {
        public static EntityState ConvertState(State state)
        {
            switch (state)
            {
                case State.Added:
                    return EntityState.Added;
                case State.Modified:
                    return EntityState.Modified;
                case State.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Unchanged;
            }
        }
    }
}
