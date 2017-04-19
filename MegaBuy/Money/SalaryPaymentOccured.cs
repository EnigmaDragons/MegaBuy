using MegaBuy.Money.Amounts;

namespace MegaBuy.Money
{
    public class SalaryPaymentOccured
    {
        public IAmount Amount;

        public SalaryPaymentOccured(IAmount amount)
        {
            this.Amount = amount;
        }
    }
}