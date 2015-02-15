namespace AdminPureGold.Repositories.Interfaces.WeichertSL
{
    public interface IUnitOfWorkSalesListing : IUnitOfWork
    {
        IListRepository ListRepository { get; }
        ISaleRepository SaleRepository { get; }        
    }
}
