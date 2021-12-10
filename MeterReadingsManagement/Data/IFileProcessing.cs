using MeterReadingsManagement.Common;
using MeterReadingsManagement.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MeterReadingsManagement.Data
{
    public interface IFileProcessing
    {
        int processData(string filePath);
        string fileUpload(IFormFile file);
    }

    public class FileProcessing : IFileProcessing
    {
        private IMeterReadingsRepository _meterReadingsRepository { get; set; }
        private IValidation _validation { get; set; }
        public FileProcessing(IMeterReadingsRepository meterReadingsRepository, IValidation validation)
        {
            this._meterReadingsRepository = meterReadingsRepository;
            this._validation = validation;
        }

        //Validate and Save CSV file data in to database
        public int processData(string filePath)
        {
            int SuccessRecordCount = 0;
            int RecordCount = 0;
            int number;

            if (File.Exists(filePath))
            {
                StreamReader reader = new StreamReader(File.OpenRead(filePath));
               
                while (!reader.EndOfStream)
                {
                    
                    var line = reader.ReadLine();

                    if (RecordCount >= 1)
                    {
                        var values = line.Split(',');
                        MeterReading meterReading = new MeterReading
                        {
                            AccountId = Convert.ToInt32(values[0]),
                            MeterReadingDateTime = DateTime.ParseExact(values[1], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                            MeterReadValue = int.TryParse(values[2], out number) ? Convert.ToInt32(values[2]) : 0
                        };

                        if (!_validation.ValidateDuplicates(meterReading) && _validation.ValidateAccountId(meterReading.AccountId)
                            && _validation.ValidateReadingValue(meterReading.MeterReadValue))
                        {
                            _meterReadingsRepository.Add(meterReading);
                            SuccessRecordCount++;
                        }
                    }
                    RecordCount++;
                }

                
            }
           
            return SuccessRecordCount;

        }

        //Upload CSV file in to files folder 
        public string fileUpload(IFormFile file)
        {
            string filePath = string.Empty;
            if (file.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Files");
                filePath = Path.Combine(uploadsFolder, file.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }
            }

            return filePath;
        }
    }
}
