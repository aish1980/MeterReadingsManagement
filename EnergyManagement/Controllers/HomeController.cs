using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EnergyManagement.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using MeterReadingsManagement.Models;
using System.IO;
using MeterReadingsManagement.ViewModels;
using Microsoft.AspNetCore.Http;

namespace EnergyManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
           
        }

        public IActionResult Upload()
        {
            return View();
        }

        private string BASE_URL = "https://localhost:44321";

        [HttpPost]
        [ActionName("Upload")]
        public IActionResult UploadFile(MeterReadingsUploadViewModel fileInfo)
        {

            var httpClient = new HttpClient();
            var multipartFormDataContent = new MultipartFormDataContent();
            httpClient.BaseAddress = new Uri(BASE_URL);
            var ms = new MemoryStream();
            fileInfo.CSVFile.CopyTo(ms);
            var fileBytes = ms.ToArray();
            var fileContent = new ByteArrayContent(fileBytes);
            multipartFormDataContent.Add(fileContent, "file", fileInfo.CSVFile.FileName);
            HttpResponseMessage response = httpClient.PostAsync("/api/meterreadings/upload", 
                    multipartFormDataContent).Result;
            ViewBag.Message = $"{ response.Content.ReadAsStringAsync().Result} records has been successfully loaded."; 
            return View();

        }

        [HttpGet]
        public IActionResult Index()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URL);
                MediaTypeWithQualityHeaderValue contentType =
        new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = client.GetAsync
        ("/api/meterreadings").Result;
                string stringData = response.Content.
        ReadAsStringAsync().Result;
                List<MeterReading> data = JsonConvert.DeserializeObject
        <List<MeterReading>>(stringData);
                return View(data);
            }

            
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(MeterReading meterReading)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URL);
                string stringData = JsonConvert.
                SerializeObject(meterReading);
                var contentData = new StringContent
            (stringData, System.Text.Encoding.UTF8,
            "application/json");
                HttpResponseMessage response = client.PostAsync
            ("/api/meterreadings", contentData).Result;
                ViewBag.Message = response.Content.
            ReadAsStringAsync().Result;
                return RedirectToAction(nameof(Index));
            }

        }


        [ActionName("Edit")]
        public ActionResult EditLoad(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri (BASE_URL);
                HttpResponseMessage response =
        client.GetAsync("/api/meterreadings/" + Id).Result;
                string stringData = response.Content.
            ReadAsStringAsync().Result;
                MeterReading data = JsonConvert.
            DeserializeObject<MeterReading>(stringData);
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult Edit(MeterReading meterReading)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URL);
                string stringData = JsonConvert.SerializeObject(meterReading);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync
            ("/api/meterreadings/" + meterReading.Id,
            contentData).Result;
                ViewBag.Message = response.Content.
            ReadAsStringAsync().Result;
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpGet]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URL);
                HttpResponseMessage response =
client.GetAsync("/api/meterreadings/" + Id).Result;
                string meterReadingData = response.
            Content.ReadAsStringAsync().Result;
                MeterReading data = JsonConvert.
DeserializeObject<MeterReading>(meterReadingData);

                return View(data);
            }
            
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Delete(int Id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URL);
                HttpResponseMessage response =
                client.DeleteAsync("/api/meterreadings/" + Id).Result;
                TempData["Message"] =
            response.Content.ReadAsStringAsync().Result;
                return RedirectToAction(nameof(Index));
            }
        }

       
    }
}
