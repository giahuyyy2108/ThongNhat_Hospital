using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThongNhat_Hospital.Models;

namespace ThongNhat_Hospital.Controllers.Admin
{
    public class LoaiHangController : Controller
    {
        private readonly DataBaseContext _context;

        public LoaiHangController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: LoaiHang
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoaiHang.ToListAsync());
        }

        // GET: LoaiHang/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiHang = await _context.LoaiHang
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loaiHang == null)
            {
                return NotFound();
            }

            return View(loaiHang);
        }

        // GET: LoaiHang/Create
        public IActionResult Create()
        {
            return PartialView("Create", new LoaiHang());
        }

        // POST: LoaiHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] LoaiHang loaiHang)
        {
            loaiHang.Id = Guid.NewGuid().ToString();
            if (ModelState.IsValid)
            {
                _context.Add(loaiHang);
                await _context.SaveChangesAsync();
                return View(loaiHang);
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", loaiHang) });
        }

        // GET: LoaiHang/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiHang = await _context.LoaiHang.FindAsync(id);
            if (loaiHang == null)
            {
                return NotFound();
            }
            return View(loaiHang);
        }

        // POST: LoaiHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name")] LoaiHang loaiHang)
        {
            if (id != loaiHang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiHangExists(loaiHang.Id))
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
            return View(loaiHang);
        }

        // GET: LoaiHang/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiHang = await _context.LoaiHang
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loaiHang == null)
            {
                return NotFound();
            }

            return View(loaiHang);
        }

        // POST: LoaiHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var loaiHang = await _context.LoaiHang.FindAsync(id);
            _context.LoaiHang.Remove(loaiHang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiHangExists(string id)
        {
            return _context.LoaiHang.Any(e => e.Id == id);
        }
    }
}
