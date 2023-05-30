using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThongNhat_Hospital.Interface;
using ThongNhat_Hospital.Models;
using ThongNhat_Hospital.Models.ViewModel;

namespace ThongNhat_Hospital.Controllers
{
    public class ReportController : Controller
    {
        private readonly DataBaseContext _context;
        private readonly IReport _service;
        public ReportController(DataBaseContext context, IReport service) 
        {
            _context = context;
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<ContentResult> GetLoaiHang(DateTime? ngayBatDau, DateTime? ngayKetThuc)
        {
            if (ngayBatDau == null && ngayKetThuc == null)
            {
                ngayBatDau = DateTime.Now.AddMonths(-4);
                ngayKetThuc = DateTime.Now;
            }
            var listmodel = await _service.ThongkeLoaiHang((DateTime)ngayBatDau, (DateTime)ngayKetThuc);

            List<DataPoint> dataPoints = new List<DataPoint>();

            foreach (var item in listmodel)
            {
                dataPoints.Add(new DataPoint(item.name, item.count));
            }
            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return Content(JsonConvert.SerializeObject(dataPoints, _jsonSetting), "application/json");
        }


        [HttpGet]
        public ContentResult GetTinhTrang()
        {
            var listmodel = _service.ThongkeTinhTrangPhieu();

            List<DataPoint> dataPoints = new List<DataPoint>();

            foreach (var item in listmodel)
            {
                dataPoints.Add(new DataPoint(item.name, item.count));
            }
            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            return Content(JsonConvert.SerializeObject(dataPoints, _jsonSetting), "application/json");
        }
    }
}
