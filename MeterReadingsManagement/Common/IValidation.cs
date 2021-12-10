using MeterReadingsManagement.Data;
using MeterReadingsManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MeterReadingsManagement.Common
{
    public interface IValidation
    {
        public bool ValidateReadingValue(int ReadingValue);
        public bool ValidateDuplicates(MeterReading meterReading);
        public bool ValidateAccountId(int AccountId);
    }

    public class Validation : IValidation
    {
        private IMeterReadingsRepository _meterReadingsRepository { get; set; }
        private IAccountsRepository _accountsRepository { get; set; }
        public Validation(IMeterReadingsRepository meterReadingsRepository, IAccountsRepository accountsRepository)
        {
            this._meterReadingsRepository = meterReadingsRepository;
            this._accountsRepository = accountsRepository;
        }
        public bool ValidateReadingValue(int ReadingValue)
        {
            Regex valueNotIntPattern = new Regex(@"^\d{4}$");

            return valueNotIntPattern.IsMatch(ReadingValue.ToString());
        }

        public bool ValidateDuplicates(MeterReading meterReading)
        {
            bool result = _meterReadingsRepository.CheckDuplicates(meterReading);
            return result;
        }

        public bool ValidateAccountId(int AccountId)
        {
            bool result =_accountsRepository.ValidateAccountId(AccountId);
            return result;
        }
    }
}
