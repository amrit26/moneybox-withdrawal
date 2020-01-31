using System;

namespace Moneybox.App.Domain.Services.Abstract
{
    public interface IAccountService
    {
        Account GetAccountInformation(Guid accountId);
        void FromBalance(decimal amount, Account from);
        void PaidInBalance(decimal amount, Account to);
    }
}
