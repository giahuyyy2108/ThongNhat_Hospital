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
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net.Http;

namespace ThongNhat_Hospital.Controllers.User1
{
    [Authorize(Roles = "admin,user")]

    public class UserController : Controller
    {
        
        private readonly DataBaseContext _context;
        private readonly UserManager<User> _userManager;
        private readonly HttpClient _httpClient;
        public UserController(DataBaseContext context, UserManager<User> userManager, HttpClient httpClient)
        {
            _context = context;
            _userManager = userManager;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> ListNhanhang()
        {
            var dataBaseContext = _context.ChiTietDonHang
                                            .Include(c => c.hinhthuc)
                                            .Include(c => c.phieugiao)
                                            .Include(c => c.user);
            return View(await dataBaseContext.ToListAsync());
        }


        public ActionResult GetListNhanhang()
        {
            var dataBaseContext = _context.ChiTietDonHang
                                            .Include(c => c.hinhthuc)
                                            .Include(c => c.phieugiao)
                                            .Where(c=>c.user.UserName == User.Identity.Name)
                                            .Where(c=>c.Id_HinhThuc == "2")
                                            .Where(c=>c.phieugiao.ngaygiao.Date == DateTime.Now.Date)
                                            .ToList();
            int stt = 1;
            List<DonNhanTrongNgayViewModel> list = new List<DonNhanTrongNgayViewModel>();
            foreach (var item in dataBaseContext)
            {
                DonNhanTrongNgayViewModel don = new DonNhanTrongNgayViewModel();
                don.Id = stt;
                stt++;
                don.Id_Phieu = item.phieugiao.Id;
                if(item.phieugiao.tinhtrang == 0)
                {
                    don.trangthai = "Chưa xác nhận";
                    don.id_CT = item.Id;
                }
                else
                {
                    don.trangthai = "Đã xác nhận";
                    don.capnhat = item.Thoigian.ToString("dd/MM/yyyy - HH:mm", CultureInfo.GetCultureInfo("vi-VN"));
                }
                list.Add(don);
            }

            return Json(new {data = list }, new Newtonsoft.Json.JsonSerializerSettings());
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
        public async Task<IActionResult> Create([Bind("Id_User,chuky,id_LoaiHang,note,files")] CTDHViewModel cTDH_Nhan)
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

            foreach (var item in cTDH_Nhan.files)
            {
                ChiTietHang chiTietHang = new ChiTietHang();
                if (item.file != null)
                {
                    chiTietHang.filename = item.filename;
                    chiTietHang.file = item.file;
                    chiTietHang.Id_PhieuGiao = phieugiao.Id;
                    chiTietHang.id = Guid.NewGuid().ToString();
                    _context.Add(chiTietHang);
                }
            }

            //for (int i = 0; i < cTDH_Nhan.files.Count; i++)
            //{
            //    ChiTietHang chiTietHang = new ChiTietHang();
            //    if (cTDH_Nhan.files[i] != null)
            //    {
            //        chiTietHang.file = cTDH_Nhan.files[i];
            //        chiTietHang.Id_PhieuGiao = phieugiao.Id;
            //        chiTietHang.id = Guid.NewGuid().ToString();
            //        _context.Add(chiTietHang);
            //    }
            //}


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
                return LocalRedirect("/user/ListNhanHang");
            }
            ViewData["Id_HinhThuc"] = new SelectList(_context.HinhThuc, "Id", "Id", cTDH_Nhan.Id_HinhThuc);
            ViewData["Id_PhieuGiao"] = new SelectList(_context.PhieuGiaoHang, "Id", "Id", cTDH_Nhan.Id_PhieuGiao);
            ViewData["Id_User"] = new SelectList(_context.user, "Id", "UserName", cTDH_Nhan.Id_User);
            ViewData["Id_LoaiHang"] = new SelectList(_context.LoaiHang, "Id", "Name");
            return View(cTDH_Nhan);
        }
        public async Task<IActionResult> Confirm(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cTDH = await _context.ChiTietDonHang
                .FindAsync(id);
            if (cTDH == null)
            {
                return NotFound();
            }
            ViewData["Id_HinhThuc"] = new SelectList(_context.HinhThuc, "Id", "Name", cTDH.Id_HinhThuc);
            ViewData["Id_PhieuGiao"] = new SelectList(_context.PhieuGiaoHang, "Id", "Id", cTDH.Id_PhieuGiao);
            ViewData["Id_User"] = new SelectList(_context.user, "Id", "UserName", cTDH.Id_User);
            List<ChiTietHang> listcth = await _context.ChiTietHang
                                                .Where(p => p.Id_PhieuGiao == cTDH.Id_PhieuGiao)
                                                .ToListAsync();
            ViewData["File"] = listcth;
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



        public ActionResult Historylist()
        {
            return View();
        }
        public async Task<IActionResult> GetHistorylist()
        {
            var dataBaseContext = await _context.ChiTietDonHang
                .Include(p => p.phieugiao)
                .Include(p => p.user)
                .Include(p => p.hinhthuc)
                .Where(p => p.Id_User == User.FindFirstValue(ClaimTypes.NameIdentifier) )
                .OrderBy(p=>p.phieugiao.ngaygiao)
                .ToListAsync();

            int stt = 1;
            List<HistoryViewModel> history = new List<HistoryViewModel>();
            foreach (var item in dataBaseContext)
            {
                HistoryViewModel model = new HistoryViewModel();
                model.stt = stt;
                stt++;
                model.id_phieu = item.Id_PhieuGiao;
                if (item.hinhthuc.Id == "1")
                {
                    model.hinhthuc = "Giao Hàng";
                } else
                {
                    model.hinhthuc = "Nhận hàng";
                }
                if (item.phieugiao.tinhtrang == 0)
                {
                    model.trangthai = "0";
                }
                else
                {
                    model.trangthai = "1";
                    model.capnhat = item.Thoigian.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                    model.id_CT = item.Id_PhieuGiao;
                }

                foreach (var item1 in await _context.ChiTietDonHang.Include(p => p.user).Where(p => p.phieugiao.Id == item.Id_PhieuGiao).Where(p => p.Id_User != item.Id_User).ToListAsync())
                {
                    if(item1.Id_HinhThuc == "1")
                    {
                        model.nguoinhan = item.user.hoten;
                        model.nguoigui = item1.user.hoten;
                    }
                    if(item1.Id_HinhThuc == "2")
                    {
                        model.nguoinhan = item1.user.hoten;
                        model.nguoigui = item.user.hoten;
                    }
                }
                history.Add(model);
            }
            return Json(new {data = history }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public async Task<IActionResult> FileDetail(string id)
        {
            var dataBaseContext = await _context.ChiTietHang
                .Include(p => p.phieugiao)
                .Include(p => p.phieugiao.ChiTietHang)
                .Where(p => p.Id_PhieuGiao == id)
                .ToListAsync();
            return View(dataBaseContext);
        }


        
        public async Task<IActionResult> getBNbyId(string mabn)
        {
            string apiUrl = $"https://hsoftapi.bvtn.org.vn/api/upd_hsoft_benhnhan/?ip=192.168.0.75&idbv=79025&mabn={mabn}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode(); // Throw an exception if the request wasn't successful

                string responseBody = await response.Content.ReadAsStringAsync();
                return Ok(responseBody);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }


        public ActionResult BenhNhan()
        {
            return View();
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
                .Include(p=>p.phieugiao.loaihang)
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
