using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services.Abstract;

namespace Moneybox.App.Domain.Services.Concrete
{
    public class UpdateService : IUpdateService
    {
        private IAccountRepository accountRepository;

        public UpdateService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public void UpdateAccount(Account account)
        {
            this.accountRepository.Update(account);
        }
    }
}
