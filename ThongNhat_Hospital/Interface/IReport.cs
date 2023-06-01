using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThongNhat_Hospital.Models;
using ThongNhat_Hospital.Models.ViewModel;

namespace ThongNhat_Hospital.Interface
{
    public interface IReport
    {
        Task<List<ThongkeViewModel>> ThongkeLoaiHang(DateTime ngayBatDau, DateTime ngayKetThuc);

        List<ThongkeViewModel> ThongkeTinhTrangPhieu();
    }
}
