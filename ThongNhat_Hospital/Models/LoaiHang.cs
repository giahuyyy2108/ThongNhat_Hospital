using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;

namespace ThongNhat_Hospital.Models
{
    public class LoaiHang
    {
        [Column(TypeName = "varchar")]
        [StringLength(400)]
        public string Id { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<CTDH> CTDH { get; set; }

    }
}
