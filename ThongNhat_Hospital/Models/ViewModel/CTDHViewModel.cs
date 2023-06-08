using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ThongNhat_Hospital.Models.ViewModel
{
    public class CTDHViewModel : CTDH 
    {
        [Required(ErrorMessage ="Phải chọn loại hàng")]
        public string id_LoaiHang { get; set; }

        public string note { get; set; }

        public string id_file { get; set; }

        public List<ChiTietHang> files { get; set; }
    }
}
