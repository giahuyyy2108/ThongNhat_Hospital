using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Tls;
using ThongNhat_Hospital.Interface;
using ThongNhat_Hospital.Models;
using ThongNhat_Hospital.Models.ViewModel;

namespace ThongNhat_Hospital.Controllers.Admin
{
    public class PhieuGiaoHangController : Controller
    {
        private readonly DataBaseContext _context;
        private readonly IReport _service;

        public PhieuGiaoHangController(DataBaseContext context,IReport service)
        {
            _context = context;
            _service = service;
        }

        // GET: PhieuGiaoHang
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.PhieuGiaoHang.Include(p => p.loaihang);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: PhieuGiaoHang/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuGiaoHang = await _context.ChiTietDonHang
                .Include(p=>p.user)
                .Include(p=>p.phieugiao)
                .Where(p=>p.Id_PhieuGiao == id) 
                .ToListAsync();
            string usergui = "";
            string usernhan = "";
            string chuky_gui = "";
            string chuky_nhan = "";
            string email_gui = "";
            string email_nhan = "";
            string ngay_nhan = "";
            string ngay_gui = "";


            foreach (var item in phieuGiaoHang)
            {
                if (item.Id_HinhThuc == "1")
                {
                    usergui = item.user.hoten;
                    chuky_gui = item.chuky;
                    email_gui = item.user.Email;
                    ngay_gui = item.Thoigian.ToString("dd/MM/yyyy H:mm", CultureInfo.GetCultureInfo("vi-VN"));

                }else if(item.Id_HinhThuc == "2")
                {
                    usernhan = item.user.hoten;
                    chuky_nhan = item.chuky;
                    email_nhan = item.user.Email;
                    ngay_nhan = item.Thoigian.ToString("dd/MM/yyyy H:mm", CultureInfo.GetCultureInfo("vi-VN"));
                }
            }


            PhieuViewModel phieuView = new PhieuViewModel()
            {
                CTphieu = phieuGiaoHang,
                nguoigui = usergui,
                nguoinhan = usernhan,
                chuky_usergui= chuky_gui,
                chuky_usernhan= chuky_nhan,
                email_usergui= email_gui,
                email_usernhan = email_nhan,
                ngay_usergui= ngay_gui,
                ngay_usernhan= ngay_nhan,
            };
            
            if (phieuGiaoHang == null)
            {
                return NotFound();
            }

            return View(phieuView);
        }

        // GET: PhieuGiaoHang/Create
        public IActionResult Create()
        {
            ViewData["Id_LoaiHang"] = new SelectList(_context.LoaiHang, "Id", "Name");
            return View();
        }

        // POST: PhieuGiaoHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ngaygiao,Note,Id_LoaiHang")] PhieuGiaoHang phieuGiaoHang)
        {
            phieuGiaoHang.Id = Guid.NewGuid().ToString();
            if (ModelState.IsValid)
            {
                _context.Add(phieuGiaoHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_LoaiHang"] = new SelectList(_context.LoaiHang, "Id", "Id", phieuGiaoHang.Id_LoaiHang);
            return View(phieuGiaoHang);
        }

        // GET: PhieuGiaoHang/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuGiaoHang = await _context.PhieuGiaoHang.FindAsync(id);
            if (phieuGiaoHang == null)
            {
                return NotFound();
            }
            ViewData["Id_LoaiHang"] = new SelectList(_context.LoaiHang, "Id", "Name", phieuGiaoHang.Id_LoaiHang);
            return View(phieuGiaoHang);
        }

        // POST: PhieuGiaoHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ngaygiao,Note,Id_LoaiHang")] PhieuGiaoHang phieuGiaoHang)
        {
            if (id != phieuGiaoHang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phieuGiaoHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhieuGiaoHangExists(phieuGiaoHang.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_LoaiHang"] = new SelectList(_context.LoaiHang, "Id", "Id", phieuGiaoHang.Id_LoaiHang);
            return View(phieuGiaoHang);
        }

        // GET: PhieuGiaoHang/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuGiaoHang = await _context.PhieuGiaoHang
                .Include(p => p.loaihang)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phieuGiaoHang == null)
            {
                return NotFound();
            }

            return View(phieuGiaoHang);
        }

        // POST: PhieuGiaoHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var phieuGiaoHang = await _context.PhieuGiaoHang.FindAsync(id);
            _context.PhieuGiaoHang.Remove(phieuGiaoHang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhieuGiaoHangExists(string id)
        {
            return _context.PhieuGiaoHang.Any(e => e.Id == id);
        }


        
    }
}
