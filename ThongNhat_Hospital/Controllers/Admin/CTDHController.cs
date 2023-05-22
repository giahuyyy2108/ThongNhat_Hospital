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
    public class CTDHController : Controller
    {
        private readonly DataBaseContext _context;

        public CTDHController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: CTDH
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.ChiTietDonHang.Include(c => c.hinhthuc).Include(c => c.phieugiao).Include(c => c.user);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: CTDH/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cTDH = await _context.ChiTietDonHang
                .Include(c => c.hinhthuc)
                .Include(c => c.phieugiao)
                .Include(c => c.user)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cTDH == null)
            {
                return NotFound();
            }

            return View(cTDH);
        }

        // GET: CTDH/Create
        public IActionResult Create()
        {
            ViewData["Id_HinhThuc"] = new SelectList(_context.HinhThuc, "Id", "Name");
            ViewData["Id_PhieuGiao"] = new SelectList(_context.PhieuGiaoHang, "Id", "Id");
            ViewData["Id_User"] = new SelectList(_context.user, "Id", "UserName");
            return View();
        }

        // POST: CTDH/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_HinhThuc,Id_PhieuGiao,Id_User,chuky")] CTDH cTDH)
        {
            cTDH.Id = Guid.NewGuid().ToString();
            if (ModelState.IsValid)
            {
                _context.Add(cTDH);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_HinhThuc"] = new SelectList(_context.HinhThuc, "Id", "Id", cTDH.Id_HinhThuc);
            ViewData["Id_PhieuGiao"] = new SelectList(_context.PhieuGiaoHang, "Id", "Id", cTDH.Id_PhieuGiao);
            ViewData["Id_User"] = new SelectList(_context.user, "Id", "Id", cTDH.Id_User);
            return View(cTDH);
        }

        // GET: CTDH/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cTDH = await _context.ChiTietDonHang.FindAsync(id);
            if (cTDH == null)
            {
                return NotFound();
            }
            ViewData["Id_HinhThuc"] = new SelectList(_context.HinhThuc, "Id", "Id", cTDH.Id_HinhThuc);
            ViewData["Id_PhieuGiao"] = new SelectList(_context.PhieuGiaoHang, "Id", "Id", cTDH.Id_PhieuGiao);
            ViewData["Id_User"] = new SelectList(_context.user, "Id", "Id", cTDH.Id_User);
            return View(cTDH);
        }

        // POST: CTDH/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Id_HinhThuc,Id_PhieuGiao,Id_User,chuky")] CTDH cTDH)
        {
            if (id != cTDH.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cTDH);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CTDHExists(cTDH.Id))
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
            ViewData["Id_HinhThuc"] = new SelectList(_context.HinhThuc, "Id", "Id", cTDH.Id_HinhThuc);
            ViewData["Id_PhieuGiao"] = new SelectList(_context.PhieuGiaoHang, "Id", "Id", cTDH.Id_PhieuGiao);
            ViewData["Id_User"] = new SelectList(_context.user, "Id", "Id", cTDH.Id_User);
            return View(cTDH);
        }

        // GET: CTDH/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cTDH = await _context.ChiTietDonHang
                .Include(c => c.hinhthuc)
                .Include(c => c.phieugiao)
                .Include(c => c.user)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cTDH == null)
            {
                return NotFound();
            }

            return View(cTDH);
        }

        // POST: CTDH/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cTDH = await _context.ChiTietDonHang.FindAsync(id);
            _context.ChiTietDonHang.Remove(cTDH);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CTDHExists(string id)
        {
            return _context.ChiTietDonHang.Any(e => e.Id == id);
        }
    }
}
