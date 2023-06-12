using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ThongNhat_Hospital.Models
{
    public class LoaiHang
    {
        [Column(TypeName = "varchar")]
        [StringLength(400)]
        public string Id { get; set; }

        [Required(ErrorMessage ="Phải nhập thông tin")]
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string Name { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<PhieuGiaoHang> PhieuGiao { get; set; }

    }
}
