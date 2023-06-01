using System.ComponentModel.DataAnnotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThongNhat_Hospital.Models
{
    public class PhieuGiaoHang
    {
        public string Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ngaygiao { get; set; }


        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string Note { get; set; }

        public int tinhtrang { get; set; }

        public string Id_LoaiHang { get; set; }


        [ForeignKey("Id_LoaiHang")]
        public LoaiHang loaihang { get; set; }

        public ICollection<CTDH> CTDH { get; set; }


        
    }
}
