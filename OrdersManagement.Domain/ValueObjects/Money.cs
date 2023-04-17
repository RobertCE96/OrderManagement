using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Domain.ValueObjects
{
    public class Money
    {
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }

        public Money(decimal amount, string currencyCode)
        {
            Amount = amount;
            CurrencyCode = currencyCode;
        }
    }
}
