﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace ThongNhat_Hospital.Models
{
    public class CTDH
    {
        [Column(TypeName = "varchar")]
        [StringLength(400)]
        public string Id { get; set; }
        public string Id_HinhThuc { get; set; }
        public string Id_PhieuGiao { get; set; }
        [Required(ErrorMessage = "Phải chọn người nhận")]
        public string Id_User { get; set; }
        public string chuky { set; get; }

        public DateTime Thoigian { set; get; }

        [ForeignKey("Id_HinhThuc")]
        public HinhThuc hinhthuc { get; set; }

        [ForeignKey("Id_PhieuGiao")]
        public PhieuGiaoHang phieugiao { get; set; }

        [ForeignKey("Id_User")]
        public User user { get; set; }
    }
}
