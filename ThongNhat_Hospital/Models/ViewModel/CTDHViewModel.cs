using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThongNhat_Hospital.Models.ViewModel
{
    public class CTDHViewModel : CTDH 
    {
        public string id_LoaiHang { get; set; }

        public string note { get; set; }
    }
}
