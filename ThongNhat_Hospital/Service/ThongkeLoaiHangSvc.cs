using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThongNhat_Hospital.Interface;
using ThongNhat_Hospital.Models;
using ThongNhat_Hospital.Models.ViewModel;

namespace ThongNhat_Hospital.Service
{
    public class ThongkeLoaiHangSvc : IReport
    {
        private readonly DataBaseContext _context;
        public ThongkeLoaiHangSvc(DataBaseContext context)
        {
            _context = context;
        }


        public List<ThongkeViewModel> ThongkeTinhTrangPhieu()
        {
            List<ThongkeViewModel> result = _context.ThongkeViewModel.FromSqlRaw($"EXEC GETReportTinhTrang").ToList();

            return result;
        }

        public List<ThongkeViewModel> ThongkeLoaiHang()
        {
            List<SqlParameter> parameter = new List<SqlParameter>
            {
                new SqlParameter("dateto", DateTime.Now),
            };

            List<ThongkeViewModel> result =  _context.ThongkeViewModel.FromSqlRaw($"EXEC GetReportLoaiHangByDate '25/04/2023' , dateto").ToList();

            return result;
        }

    }
}
