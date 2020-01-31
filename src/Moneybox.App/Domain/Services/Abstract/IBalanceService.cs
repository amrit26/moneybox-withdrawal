namespace Moneybox.App.Domain.Services.Abstract
{
    public interface IBalanceService
    {
        decimal Withdrawal(decimal from, decimal amount);
        decimal Deposit(decimal to, decimal amount);
    }
}
