using System.ComponentModel.DataAnnotations;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ThongNhat_Hospital.Models
{
    public class PhieuGiaoHang
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime ngaygiao { get; set; }

        public string chuky { set; get; }

        public ICollection<CTDH> CTDH { get; set; }
    }
}
