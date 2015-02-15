using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.Mrc;

namespace AdminPureGold.Repositories.Repositories.Mrc
{
    //class PrintJobToAppObjectToTransactionRepository 
    //{
    public class PrintJobToAppObjectToTransactionRepository : GenericRepository<PrintJobToAppObjectToTransaction>, IPrintJobToAppObjectToTransactionRepository
    {
        private readonly MrcContext _context;

        public PrintJobToAppObjectToTransactionRepository(MrcContext context)
            : base(context)
        {
            _context = context;
        }

    }
}
