using Moneybox.App.Domain.Services.Abstract;

namespace Moneybox.App.Domain.Services.Concrete
{
    public class BalanceService : IBalanceService
    {
        public BalanceService()
        {
        }

        public decimal Withdrawal(decimal from, decimal amount)
        {
            return from - amount;
        }

        public decimal Deposit(decimal to, decimal amount)
        {
            return to + amount;
        }
    }
}
