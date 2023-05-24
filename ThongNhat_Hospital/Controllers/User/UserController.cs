using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using ThongNhat_Hospital.Models;
using ThongNhat_Hospital.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ThongNhat_Hospital.Controllers.User1
{
    public class UserController : Controller
    {

        private readonly DataBaseContext _context;
        private readonly UserManager<User> _userManager;
        
        public UserController(DataBaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> ListNhanhang()
        {
            var dataBaseContext = _context.ChiTietDonHang
                                            .Include(c => c.hinhthuc)
                                            .Include(c => c.phieugiao)
                                            .Include(c => c.user);
            return View(await dataBaseContext.ToListAsync());
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewData["Id_HinhThuc"] = new SelectList(_context.HinhThuc, "Id", "Name");
            ViewData["Id_PhieuGiao"] = new SelectList(_context.PhieuGiaoHang, "Id", "Id");
            ViewData["Id_User"] = new SelectList(_context.user, "Id", "UserName");
            ViewData["Id_LoaiHang"] = new SelectList(_context.LoaiHang, "Id", "Name");
            return View();
        }

        // POST: CTDH/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_User,chuky,id_LoaiHang,note")] CTDHViewModel cTDH_Nhan)
        {

            PhieuGiaoHang phieugiao = new PhieuGiaoHang();
            phieugiao.Id_LoaiHang = cTDH_Nhan.id_LoaiHang;
            phieugiao.Id = Guid.NewGuid().ToString();
            phieugiao.ngaygiao = DateTime.Now;
            phieugiao.Note = cTDH_Nhan.note;
            phieugiao.tinhtrang = 0;

            CTDH cTDH_Giao = new CTDH();
            cTDH_Giao.Id = Guid.NewGuid().ToString();
            cTDH_Giao.Id_PhieuGiao = phieugiao.Id;
            cTDH_Giao.Id_User = User.FindFirstValue(ClaimTypes.NameIdentifier);
            cTDH_Giao.Id_HinhThuc = "1";
            cTDH_Giao.chuky = cTDH_Nhan.chuky;
            cTDH_Giao.Thoigian = DateTime.Now;


            cTDH_Nhan.Id = Guid.NewGuid().ToString();
            cTDH_Nhan.Id_PhieuGiao = phieugiao.Id;
            cTDH_Nhan.Id_HinhThuc = "2";
            cTDH_Nhan.chuky = null;

            if (ModelState.IsValid)
            {
                _context.Add(phieugiao);
                _context.Add(cTDH_Giao);
                _context.Add(cTDH_Nhan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_HinhThuc"] = new SelectList(_context.HinhThuc, "Id", "Id", cTDH_Nhan.Id_HinhThuc);
            ViewData["Id_PhieuGiao"] = new SelectList(_context.PhieuGiaoHang, "Id", "Id", cTDH_Nhan.Id_PhieuGiao);
            ViewData["Id_User"] = new SelectList(_context.user, "Id", "Id", cTDH_Nhan.Id_User);
            return View(cTDH_Nhan);
        }
        public async Task<IActionResult> Confirm(string id)
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
            ViewData["Id_HinhThuc"] = new SelectList(_context.HinhThuc, "Id", "Name", cTDH.Id_HinhThuc);
            ViewData["Id_PhieuGiao"] = new SelectList(_context.PhieuGiaoHang, "Id", "Id", cTDH.Id_PhieuGiao);
            ViewData["Id_User"] = new SelectList(_context.user, "Id", "UserName", cTDH.Id_User);
            return View(cTDH);
        }

        // POST: CTDH/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(string id, [Bind("Id,Id_HinhThuc,Id_PhieuGiao,Id_User,chuky")] CTDH cTDH)
        {
            if (id != cTDH.Id)
            {
                return NotFound();
            }

            var phieu = _context.PhieuGiaoHang.Find(cTDH.Id_PhieuGiao);
            phieu.tinhtrang = 1;

            cTDH.Thoigian = DateTime.Now;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phieu);
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
                return RedirectToAction(nameof(ListNhanhang));
            }
            ViewData["Id_HinhThuc"] = new SelectList(_context.HinhThuc, "Id", "Id", cTDH.Id_HinhThuc);
            ViewData["Id_PhieuGiao"] = new SelectList(_context.PhieuGiaoHang, "Id", "Id", cTDH.Id_PhieuGiao);
            ViewData["Id_User"] = new SelectList(_context.user, "Id", "Id", cTDH.Id_User);
            return View(cTDH);
        }
        private bool CTDHExists(string id)
        {
            return _context.ChiTietDonHang.Any(e => e.Id == id);
        }
    }
}
