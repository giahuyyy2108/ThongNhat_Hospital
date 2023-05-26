using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        public List<ThongkeViewModel> ThongkheLoaiHang()
        {
            //List<SqlParameter> parameter = new List<SqlParameter>
            //{

            //};

            List<ThongkeViewModel> result =  _context.ThongkeViewModel.FromSqlRaw("EXEC GetReportLoaiHangByDate '25/04/2023' ,'25/05/2023'").ToList();

            return result;
        }
    }
}
