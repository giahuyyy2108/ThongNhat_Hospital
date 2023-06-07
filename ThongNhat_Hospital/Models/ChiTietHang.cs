using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ThongNhat_Hospital.Models
{
    public class ChiTietHang
    {
        [Column(TypeName = "varchar")]
        [StringLength(400)]
        public string id { get; set; }

        public string Id_PhieuGiao { get; set; }

        public string filename { get; set; }

        public string file { get; set; }

        [ForeignKey("Id_PhieuGiao")]
        public PhieuGiaoHang phieugiao { get; set; }


    }
}
