namespace AdminPureGold.Repositories.Interfaces.WeichertCore
{
    public interface IUnitOfWorkCore : IUnitOfWork
    {        
        IOfficeRepository OfficeRepository { get; }
        IPersonRepository PersonRepository { get; }
        IPersonToRelateRepository PersonToRelateRepository { get; }
        IRelateToEmailRepository RelateToEmailRepository { get; }
        IRelateToNameRepository RelateToNameRepository { get; }
        IRelateToPhoneRepository RelateToPhoneRepository { get; }
        IWeichertOneUserRepository WeichertOneUserRepository { get; }
    }
}
