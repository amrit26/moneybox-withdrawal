using Moneybox.App.Domain.Services.Abstract;
using System;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {
        private IAccountService accountService;
        private IBalanceService balanceService;
        private IUpdateService updateService;

        public TransferMoney(
            IAccountService accountService,
            IBalanceService balanceService,
            IUpdateService updateService)
        {
            this.accountService = accountService;
            this.balanceService = balanceService;
            this.updateService = updateService;
        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var from = this.accountService.GetAccountInformation(fromAccountId);
            var to = this.accountService.GetAccountInformation(toAccountId);

            this.accountService.FromBalance(amount, from);
            this.accountService.PaidInBalance(amount, to);

            from.Balance = this.balanceService.Withdrawal(from.Balance, amount);
            from.Withdrawn = this.balanceService.Withdrawal(from.Withdrawn, amount);
            to.Balance = this.balanceService.Deposit(to.Balance, amount);
            to.PaidIn = this.balanceService.Deposit(to.PaidIn, amount);

            this.updateService.UpdateAccount(from);
            this.updateService.UpdateAccount(to);
        }
    }
}
