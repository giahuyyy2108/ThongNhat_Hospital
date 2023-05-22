using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ThongNhat_Hospital.Models;

namespace ThongNhat_Hospital.Controllers.Admin
{
    public class LoaiHangController : Controller
    {

        private readonly DataBaseContext _context;
        public LoaiHangController(DataBaseContext context) {
            _context = context;
        }
        public IActionResult Index()
        {
            List<LoaiHang> loaiHangs = _context.LoaiHang
                .Include(l => l.Name)
                .ToList();
            return View(loaiHangs);
        }



        public IActionResult Create()
        {
            return View();
        }
    }
}
