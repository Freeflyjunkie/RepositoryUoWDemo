namespace AdminPureGold.Repositories.Interfaces.AtlasX
{
    public interface IUnitOfWorkAtlasX : IUnitOfWork
    {
        IPropertyRepository PropertyRepository { get; }
        IPropertyAlternateRepository PropertyAlternateRepository { get; }
        IWAtlasXRepository WAtlasXRepository { get; }
        IWAtlasXToAppRepository WAtlasXToAppRepository { get; }
        IWAtlasXToAppWPersonRepository WAtlasXToAppWPersonRepository { get; }
    }
}
