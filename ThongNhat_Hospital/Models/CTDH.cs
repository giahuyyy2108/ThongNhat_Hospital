using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThongNhat_Hospital.Models
{
    public class CTDH
    {
        [Column(TypeName = "varchar")]
        [StringLength(400)]
        public string Id { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string Note { get; set; }

        public string Id_LoaiHang { get; set; }

        public string Id_HinhThuc { get; set; }

        public int Id_PhieuGiao { get; set; }

        public string Id_User { get; set; }

        [ForeignKey("Id_LoaiHang")]
        public LoaiHang loaihang { get; set; }

        [ForeignKey("Id_HinhThuc")]
        public HinhThuc hinhthuc { get; set; }

        [ForeignKey("Id_PhieuGiao")]
        public PhieuGiaoHang phieugiao { get; set; }

        [ForeignKey("Id_User")]
        public User user { get; set; }
    }
}
