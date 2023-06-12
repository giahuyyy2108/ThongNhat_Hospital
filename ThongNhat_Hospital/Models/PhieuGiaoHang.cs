using System.ComponentModel.DataAnnotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Runtime.Serialization;

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
        [Required(ErrorMessage ="Phải chọn loại hàng")]
        public string Id_LoaiHang { get; set; }


        [ForeignKey("Id_LoaiHang")]
        public LoaiHang loaihang { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<CTDH> CTDH { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<ChiTietHang> ChiTietHang { get; set; }

    }
}
