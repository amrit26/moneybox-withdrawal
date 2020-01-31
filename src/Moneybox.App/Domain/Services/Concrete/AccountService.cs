using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services.Abstract;
using System;

namespace Moneybox.App.Domain.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private IAccountRepository accountRepository;
        private IBalanceService balanceService;
        private INotificationService notificationService;

        public AccountService(
            IAccountRepository accountRepository,
            IBalanceService balanceService,
            INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.balanceService = balanceService;
            this.notificationService = notificationService;
        }

        public Account GetAccountInformation(Guid accountId)
        {
            return this.accountRepository.GetAccountById(accountId);
        }

        public void FromBalance(decimal amount, Account from)
        {
            var fromBalance = this.balanceService.Withdrawal(from.Balance, amount);
            if (fromBalance < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }

            if (fromBalance < 500m)
            {
                this.notificationService.NotifyFundsLow(from.User.Email);
            }
        }

        public void PaidInBalance(decimal amount, Account to)
        {
            var paidIn = this.balanceService.Deposit(to.PaidIn, amount);
            if (paidIn > Account.PayInLimit)
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }

            if (Account.PayInLimit - paidIn < 500m)
            {
                this.notificationService.NotifyApproachingPayInLimit(to.User.Email);
            }
        }
    }
}
