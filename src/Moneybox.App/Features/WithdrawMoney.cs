using Moneybox.App.Domain.Services.Abstract;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private IAccountService accountService;
        private IBalanceService balanceService;
        private IUpdateService updateService;

        public WithdrawMoney(
            IAccountService accountService,
            IBalanceService balanceService,
            IUpdateService updateService)
        {
            this.balanceService = balanceService;
            this.accountService = accountService;
            this.updateService = updateService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            var from = this.accountService.GetAccountInformation(fromAccountId);

            this.accountService.FromBalance(amount, from);

            from.Balance = this.balanceService.Withdrawal(from.Balance, amount);
            from.Withdrawn = this.balanceService.Withdrawal(from.Withdrawn, amount);

            this.updateService.UpdateAccount(from);
        }
    }
}
