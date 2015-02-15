using System;

namespace AdminPureGold.Domain.Models.WeichertSL
{
    public class SaleToBroker
    {
        public Int32 SaleToBrokerId { get; set; }
        public Int32 SaleId { get; set; }
        public Byte SaleBrokerSequenceNumber { get; set; }
        public String SaleBrokerNumber { get; set; }
    }
}
