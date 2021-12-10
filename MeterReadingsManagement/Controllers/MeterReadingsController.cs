using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MeterReadingsManagement.Common;
using MeterReadingsManagement.Data;
using MeterReadingsManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeterReadingsManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingsController : ControllerBase
    {

        private IMeterReadingsRepository _meterReadingsRepository { get; set; }
        private IFileProcessing _fileProcessing { get; set; }
        
        public MeterReadingsController(IMeterReadingsRepository meterReadingsRepository, IFileProcessing fileProcessing
           )
        {
            this._meterReadingsRepository = meterReadingsRepository;
            this._fileProcessing= fileProcessing;
        }

        //Upload Files
        [HttpPost("upload")]
        public int Upload([FromForm] IFormFile file)
        {
            string filePath = _fileProcessing.fileUpload(file);
            int SuccessRecordCount = _fileProcessing.processData(filePath);
            return SuccessRecordCount;
        }

        // Get all MeterReadings Records
        [HttpGet]
        public IEnumerable<MeterReading> Get()
        {
            var meterReadingData = _meterReadingsRepository.GetAllMeterReadingValues();
            return meterReadingData;
        }

        // Get MeterReadings Records by id
        [HttpGet("{id}")]
        public MeterReading Get(int id)
        {
            var meterReadingData = _meterReadingsRepository.GetMeterReadingValuesById(id);
            return meterReadingData;
        }

        // Insert MeterReadings Records
        [HttpPost]
        public void Post(MeterReading meterReading)
        {
            _meterReadingsRepository.Add(meterReading);
        }

        // Update MeterReadings Records
        [HttpPut("{id}")]
        public void Put(MeterReading meterReading)
        {
            _meterReadingsRepository.Update(meterReading);
        }

        // DELETE MeterReadings Records
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _meterReadingsRepository.Delete(id);
        }
    }
}
