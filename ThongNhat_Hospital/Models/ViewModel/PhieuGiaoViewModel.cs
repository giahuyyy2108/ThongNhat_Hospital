using System;
using System.ComponentModel.DataAnnotations;

namespace ThongNhat_Hospital.Models.ViewModel
{
    public class PhieuGiaoViewModel
    {
        public string MaPhieuGiao { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập Email")]
        public string Email { get; set; }
       
        [Required(ErrorMessage = "Bạn cần chọn loại hàng")]
        public string TenHang { get; set; }
        public string MaNguoiNhan { get; set; }
        public string MaNguoiGui { get; set; }

    }
}
