namespace AdminPureGold.Repositories.Interfaces.CorpComm
{
    public interface IUnitOfWorkCorpComm : IUnitOfWork
    {
        IMcMessageRepository McMessageRepository { get; }
    }
}
