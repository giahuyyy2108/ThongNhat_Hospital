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
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Collections.Generic;
using System.Globalization;

namespace ThongNhat_Hospital.Controllers.User1
{
    [Authorize(Roles = "admin,user")]

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
            ViewData["Id_User"] = new SelectList(_context.user.Where(p=>p.Id != (User.FindFirstValue(ClaimTypes.NameIdentifier))), "Id", "UserName");
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

            if(cTDH_Nhan.Id_User == cTDH_Giao.Id_User)
            {
                ModelState.AddModelError(string.Empty, "Không được gửi cho bản thân");
            }

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



        public async Task<IActionResult> Historylist()
        {
            var dataBaseContext = await _context.ChiTietDonHang
                .Include(p=> p.phieugiao)
                .Include(p=> p.user)
                .Where(p=> p.Id_HinhThuc == "2")
                .ToListAsync();
            

            return View(dataBaseContext);
        }


        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuGiaoHang = await _context.ChiTietDonHang
                .Include(p => p.user)
                .Include(p => p.phieugiao)
                .Where(p => p.Id_PhieuGiao == id)
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

                }
                else if (item.Id_HinhThuc == "2")
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
                chuky_usergui = chuky_gui,
                chuky_usernhan = chuky_nhan,
                email_usergui = email_gui,
                email_usernhan = email_nhan,
                ngay_usergui = ngay_gui,
                ngay_usernhan = ngay_nhan,
            };

            if (phieuGiaoHang == null)
            {
                return NotFound();
            }

            return View(phieuView);
        }
    }
}
