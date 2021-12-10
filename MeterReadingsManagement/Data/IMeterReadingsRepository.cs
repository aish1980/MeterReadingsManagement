using MeterReadingsManagement.Common;
using MeterReadingsManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeterReadingsManagement.Data
{
    public interface IMeterReadingsRepository
    {
        MeterReading GetMeterReadingValuesById(int Id);
        IEnumerable<MeterReading> GetAllMeterReadingValues();
        MeterReading Add(MeterReading meterReadingData);
        MeterReading Update(MeterReading meterReadingData);
        MeterReading Delete(int Id);
        bool CheckDuplicates(MeterReading meterReadingData);
    }

    public class MeterReaderRepository : IMeterReadingsRepository
    {
        private readonly DataDbContext context;

        public MeterReaderRepository(DataDbContext context)
        {
            this.context = context;
        }

        public MeterReading Add(MeterReading meterReadingDetails)
        {
            context.MeterReadings.Add(meterReadingDetails);
            context.SaveChanges();
            return meterReadingDetails;
        }

        public MeterReading Delete(int Id)
        {
            MeterReading meterReadingDetails = context.MeterReadings.Find(Id);
            if (meterReadingDetails != null)
            {
                context.MeterReadings.Remove(meterReadingDetails);
                context.SaveChanges();
            }
            return meterReadingDetails;
        }

        public IEnumerable<MeterReading> GetAllMeterReadingValues()
        {
            return context.MeterReadings;
        }

        public MeterReading GetMeterReadingValuesById(int Id)
        {
            return context.MeterReadings.Find(Id);
        }

        public MeterReading Update(MeterReading meterReadingDetails)
        {
            var meterReadingData = context.MeterReadings.Attach(meterReadingDetails);
            meterReadingData.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return meterReadingDetails;
        }

        //Check duplicate records
        bool IMeterReadingsRepository.CheckDuplicates(MeterReading meterReadingData)
        {
            bool isRecodeExist = context.MeterReadings.Any(record =>
                                        record.AccountId == meterReadingData.AccountId &&
                                        record.MeterReadingDateTime != meterReadingData.MeterReadingDateTime);
            return isRecodeExist;
        }
    }
}
