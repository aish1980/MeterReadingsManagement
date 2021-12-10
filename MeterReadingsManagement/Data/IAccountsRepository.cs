using MeterReadingsManagement.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeterReadingsManagement.Data
{
    public interface IAccountsRepository
    {
        bool ValidateAccountId(int accountId);
    }

    public class AccountsRepository : IAccountsRepository
    {
        private readonly DataDbContext context;

        public AccountsRepository(DataDbContext context)
        {
            this.context = context;
        }

        //Check validity of account id
        public bool ValidateAccountId(int accountId)
        {
            bool isRecodeExist = context.MeterReadings.Any(record =>
                                        record.AccountId == accountId);
            return isRecodeExist;
        }
    }
}
